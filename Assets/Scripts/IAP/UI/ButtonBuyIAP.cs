using System.Collections;
using TMPro;
using Tracking;
using UnityEngine;
using UnityEngine.UI;

namespace IAP
{
    public class ButtonBuyIAP : MonoBehaviour
    {
        private Button btnBuy;

        [SerializeField] private TMP_Text textContent;

        [SerializeField] private string productId;
        [SerializeField] private int packId;

        void Start ()
        {
            btnBuy = GetComponent<Button>();
            btnBuy.onClick.AddListener(OnBtnBuyClicked);
            btnBuy.interactable = !IAPService.Instance.CheckBuyPack(productId);
            FillData();
        }

        private void OnBtnBuyClicked()
        {
            IapTracker.LogIapClick(IAPService.GetPack(productId));
            IAPService.Instance.PurchasePack(productId, packId);
        }
        private void FillData()
        {
            var pack = IAPService.GetPack(productId);
            textContent.text = $"{productId} - {pack.metadata.localizedPriceString}/MONTH";
        }
    }
}