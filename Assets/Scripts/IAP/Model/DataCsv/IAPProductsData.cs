using System.Collections.Generic;
using UnityEngine;

namespace IAP
{
    [CreateAssetMenu(menuName = "Config", fileName = "IAPData", order = 1)]
    public class IAPProductsData : ScriptableObject
    {
        public Dictionary<string, IAPProductCsv> products
        {
            get
            {
                if (products == null)
                {
                    ConvertData();
                }
                return products;
            }
        }

        public Dictionary<string, IAPProductCsv> _products;
        #region Initialize

        public IAPProductCsv[] _dataIAP;

        public void ConvertData()
        {
            _products = new Dictionary<string, IAPProductCsv>();
            foreach (IAPProductCsv entry in _dataIAP)
            {
                if (_products.ContainsKey(entry.gameProductId))
                {
                    Debug.LogError($"[CSV IAP] Duplicate Game Product Id={entry.gameProductId}");
                    continue;
                }
                _products.Add(entry.gameProductId, entry);
            }
        }

        #endregion

        #region API

        public List<IAPProductCsv> GetAllProducts()
        {
            return new List<IAPProductCsv>(products.Values);
        }

        public bool TryGetProductData(string gameProductId, out IAPProductCsv data)
        {
            if (products.TryGetValue(gameProductId, out IAPProductCsv value))
            {
                data = value;
                return true;
            }

            data = new IAPProductCsv();
            return false;
        }

        #endregion
    }
}