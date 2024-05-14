using UnityEngine;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing;
using Utility;

namespace IAP
{
    public class IAPConfirmService
    {
        public int IAPCount
        {
            get => PlayerPrefs.GetInt(IAPTag.IAP_COUNT, 0);
            set => PlayerPrefs.SetInt(IAPTag.IAP_COUNT, value);
        }
        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            Debug.LogWarning(
                $"UNITY_IAP: OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {reason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogWarning(
                $"UNITY_IAP: OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', " +
                $"PurchaseFailureReason: {failureDescription.reason}, Message : {failureDescription.message}");
        }

        public void OnPurchaseSuccess(Product product)
        {
            var gameProductId = product.definition.id;

            var iapData = LoadResourceService.LoadCsv<IAPProductsData>();
            if (iapData.TryGetProductData(gameProductId, out IAPProductCsv data))
            {
                OnPurchaseSuccessAction(data);
                Debug.Log($"[IAP PROCESS] PURCHASE SUCCESS of GAME PRODUCT ID={gameProductId}");
            }
            else
            {
                Debug.LogError($"[IAP PROCESS] Not have data of GAME PRODUCT ID={gameProductId}");
            }
        }

        public void OnPurchaseSuccessAction(IAPProductCsv productData)
        {
            IAPCount++;
            AdsManager.Instance.SetRemovedAds();
        }
    }
}
