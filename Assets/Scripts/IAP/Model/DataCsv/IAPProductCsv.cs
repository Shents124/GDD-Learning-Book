using System;

namespace IAP
{
    [Serializable]
    public struct IAPProductCsv
    {
        public string gameProductId;

        public string googlePlayId;

        public float googlePlayPrice;

        public string appleAppStoreId;

        public float appleAppStorePrice;

        public IAPProductType gameProductType;

        public int idPack;
        public string GetStoreId()
        {
#if UNITY_ANDROID
            return googlePlayId;
#elif UNITY_IOS
                return appleAppStoreId;
#else
                return googlePlayId;
#endif
        }

        public float GetStorePrice()
        {
#if UNITY_ANDROID
            return googlePlayPrice;
#elif UNITY_IOS
                return appleAppStorePrice;
#else
                return googlePlayPrice;
#endif
        }
    }
}