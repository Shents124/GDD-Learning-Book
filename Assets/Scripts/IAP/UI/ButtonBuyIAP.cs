using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IAP
{
    public class ButtonBuyIAP : MonoBehaviour
    {
        private Button btnBuy;

        [SerializeField] private string productId;
        [SerializeField] private int packId;

        void Start ()
        {
            btnBuy = GetComponent<Button>();
            btnBuy.onClick.AddListener(OnBtnBuyClicked);
        }

        private void OnBtnBuyClicked()
        {
            IAPService.Instance.PurchasePack(productId, packId);
        }
    }
}