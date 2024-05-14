using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Security;
using Utility;

namespace IAP
{
    public class IAPService : MonoBehaviour, IDetailedStoreListener
    {
        private static IAPService _instance;

        public static IAPService Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = (new GameObject("AdsManager")).AddComponent<IAPService>();
                return _instance;
            }
        }

        void Start()
        {
            InitializeUnityGamingService().Forget();
            InitUnityIAP();
        }

        private const string ENVIRONMENT = "production";
        private static IStoreController s_storeController;
        private static IExtensionProvider s_extensionProvider;
        private static CrossPlatformValidator s_validator;

        private static IAPConfirmService s_iapConfirmService;

        #region Initialize

        public async UniTask InitializeUnityGamingService()
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName(ENVIRONMENT);
                await UnityServices.InitializeAsync(options).AsUniTask();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void InitUnityIAP()
        {
            if (IsInitialized())
                return;

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            var iapProductData = LoadResourceService.LoadCsv<IAPProductsData>();
            var allProducts = iapProductData.GetAllProducts();

            foreach (IAPProductCsv product in allProducts)
            {
                IDs storeSpecificIds = new IDs {
                    { product.googlePlayId, GooglePlay.Name }, { product.appleAppStoreId, AppleAppStore.Name }
                };

                builder.AddProduct(product.gameProductId, product.productType, storeSpecificIds);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        private void InitializeValidator()
        {
            if (IsCurrentStoreSupportedByValidator())
            {
                //s_validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);

#if !UNITY_EDITOR
             s_validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
#endif
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            s_storeController = controller;
            s_extensionProvider = extensions;

            s_iapConfirmService = new IAPConfirmService();

            InitializeValidator();

            var iapProductData = LoadResourceService.LoadCsv<IAPProductsData>();
            var allProducts = iapProductData.GetAllProducts();

            foreach (Product product in controller.products.all)
            {
                if (!string.IsNullOrEmpty(product.transactionID))
                    controller.ConfirmPendingPurchase(product);
            }

            Debug.Log($"[IAP] Initialize Success!");

            foreach (IAPProductCsv product in allProducts)
            {
                if (product.productType == ProductType.NonConsumable)
                {
                    Product pro = controller.products.WithID(product.gameProductId);
                    if (CheckPurchaseBuyAllPack(pro))
                    {
                        AdsManager.Instance.SetRemovedAds();
                        Debug.Log($"[IAP] Check buy full pack {product.gameProductId}");
                        return;
                    }
                }

                if (product.productType == ProductType.Subscription)
                {
                    Product pro = controller.products.WithID(product.gameProductId);
                    if (IsSubscribedTo(pro))
                    {
                        AdsManager.Instance.SetRemovedAds();
                        Debug.Log($"[IAP] Check Subscribed Pack {product.gameProductId}");
                        return;
                    }
                    else
                    {
                        AdsManager.Instance.SetUnRemovedAds();
                        Debug.Log($"[IAP] Check Expired Pack {product.gameProductId}");
                    }
                }
            }
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogWarning($"[IAP]: OnInitializeFailed error={error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogWarning($"[IAP]: OnInitializeFailed error={error}, message={message}");
        }

        private bool IsInitialized()
        {
            return s_storeController != null && s_extensionProvider != null;
        }

        private bool IsCurrentStoreSupportedByValidator()
        {
            var currentStore = StandardPurchasingModule.Instance().appStore;
            return currentStore == AppStore.GooglePlay ||
                   currentStore == AppStore.AppleAppStore ||
                   currentStore == AppStore.MacAppStore;
        }

        private bool CheckPurchaseBuyAllPack(Product packAll)
        {
            return packAll.hasReceipt;
        }

        private bool IsSubscribedTo(Product subscription)
        {
            // If the product doesn't have a receipt, then it wasn't purchased and the user is therefore not subscribed.
            if (subscription.receipt == null)
            {
                return false;
            }

            //The intro_json parameter is optional and is only used for the App Store to get introductory information.
            var subscriptionManager = new SubscriptionManager(subscription, null);

            // The SubscriptionInfo contains all of the information about the subscription.
            var info = subscriptionManager.getSubscriptionInfo();

            return info.isSubscribed() == Result.True;
        }

        #endregion

        #region Purchase Process

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Product product = purchaseEvent.purchasedProduct;

            var isPurchaseValid = IsPurchaseValid(product);

            if (isPurchaseValid)
            {
                //UIService.CloseLoadingActivity();
                s_iapConfirmService.OnPurchaseSuccess(product);
            }

            return PurchaseProcessingResult.Complete;
        }

        private bool IsPurchaseValid(Product product)
        {
            if (IsCurrentStoreSupportedByValidator())
            {
                try
                {
                    var result = s_validator.Validate(product.receipt);

                    foreach (IPurchaseReceipt receipt in result)
                    {
                        LogReceipt(receipt);
                    }
                }
                catch (IAPSecurityException reason)
                {
                    Debug.LogWarning($"Invalid receipt: {reason}");
                    return false;
                }
            }

            return true;
        }

        private void LogReceipt(IPurchaseReceipt receipt)
        {
            Debug.Log($"Product ID: {receipt.productID}\n" +
                      $"Purchase Date: {receipt.purchaseDate}\n" +
                      $"Transaction ID: {receipt.transactionID}");

            if (receipt is GooglePlayReceipt googleReceipt)
            {
                Debug.Log($"Purchase State: {googleReceipt.purchaseState}\n" +
                          $"Purchase Token: {googleReceipt.purchaseToken}");
            }

            if (receipt is AppleInAppPurchaseReceipt appleReceipt)
            {
                Debug.Log($"Original Transaction ID: {appleReceipt.originalTransactionIdentifier}\n" +
                          $"Subscription Expiration Date: {appleReceipt.subscriptionExpirationDate}\n" +
                          $"Cancellation Date: {appleReceipt.cancellationDate}\n" +
                          $"Quantity: {appleReceipt.quantity}");
            }
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            s_iapConfirmService.OnPurchaseFailed(product, failureReason);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            s_iapConfirmService.OnPurchaseFailed(product, failureDescription);
        }

        #endregion

        #region API

        public void PurchasePack(string productId, int id)
        {
            if (IsInitialized())
            {

#if UNITY_IOS
            //UIService.OpenLoadingActivity();
#endif

                Product product = s_storeController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    s_storeController.InitiatePurchase(product);
                }
                else
                {
                    Debug.LogWarning($"UNITY_IAP: FAILED BuyProductID '{productId}'. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.LogWarning($"UNITY_IAP: FAILED BuyProductID '{productId}'. Not initialized.");
                InitUnityIAP();
            }
        }

        public float GetBasePriceFromCsv(string gameProductId)
        {
            var csvData = LoadResourceService.LoadCsv<IAPProductsData>();
            if (csvData.TryGetProductData(gameProductId, out IAPProductCsv data))
            {
                float priceByStore = data.GetStorePrice();
                return priceByStore;
            }

            return -1;
        }

        public string GetPriceStringById(string gameProductId)
        {
            if (!IsInitialized())
                return GetPriceFromCsv(gameProductId);

            if (s_storeController.products.all.Length > 0)
            {
                foreach (Product product in s_storeController.products.all)
                {
                    if (product.availableToPurchase && product.definition.id.Equals(gameProductId))
                        return product.metadata.localizedPriceString;
                }
            }

            return GetPriceFromCsv(gameProductId);
        }

        private string GetPriceFromCsv(string gameProductId)
        {
            var csvData = LoadResourceService.LoadCsv<IAPProductsData>();
            if (csvData.TryGetProductData(gameProductId, out IAPProductCsv data))
            {
                float priceByStore = data.GetStorePrice();
                return $"${priceByStore}";
            }

            return string.Empty;
        }

        #endregion
    }
}
