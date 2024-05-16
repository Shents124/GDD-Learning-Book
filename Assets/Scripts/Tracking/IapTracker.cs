using Firebase.Analytics;
using Tracking.Constant;
using UnityEngine.Purchasing;

namespace Tracking
{
    public static class IapTracker
    {
        public static void LogIapButton()
        {
            FirebaseUserTracker.SetUserProperties();

            FirebaseAnalytics.LogEvent(FirebaseEventName.IAP_BUTTON_CLICK);
        }

        public static void LogIapUI(string eventName)
        {
            FirebaseUserTracker.SetUserProperties();

            FirebaseAnalytics.LogEvent(eventName);
        }

        public static void LogIapClick(Product product)
        {
            var gameProductId = product.definition.id;

            Parameter[] firebaseParams = {
                new(FirebaseParam.PACKAGE_ID, gameProductId),
                new(FirebaseParam.VALUE, product.metadata.localizedPriceString),
                new(FirebaseParam.CURRENCY, "USD")
            };

            FirebaseUserTracker.SetUserProperties();

            FirebaseAnalytics.LogEvent(FirebaseEventName.IAP_CLICK);
        }

        public static void LogIapSuccess(Product product)
        {
            var gameProductId = product.definition.id;

            Parameter[] firebaseParams = {
                new(FirebaseParam.PACKAGE_ID, gameProductId),
                new(FirebaseParam.VALUE, product.metadata.localizedPriceString),
                new(FirebaseParam.CURRENCY, "USD")
            };
            
            FirebaseUserTracker.SetUserProperties();
            
            FirebaseAnalytics.LogEvent(FirebaseEventName.IAP_SUCCESS, firebaseParams);

        }

        public static void LogIapConfirmSuccess(Product product)
        {
            var gameProductId = product.definition.id;

            Parameter[] firebaseParams = {
                new(FirebaseParam.PACKAGE_ID, gameProductId),
                new(FirebaseParam.VALUE, product.metadata.localizedPriceString),
                new(FirebaseParam.CURRENCY, "USD")
            };

            FirebaseUserTracker.SetUserProperties();

            FirebaseAnalytics.LogEvent(FirebaseEventName.IAP_CONFIRM_SUCCESS, firebaseParams);

        }

        public static void LogIapFailed(Product product, string errorName)
        {
            var gameProductId = product.definition.id;

            Parameter[] firebaseParams = {
                new(FirebaseParam.PACKAGE_ID, gameProductId),
                new (FirebaseParam.ERROR_NAME, errorName),
                new(FirebaseParam.VALUE, product.metadata.localizedPriceString),
                new(FirebaseParam.CURRENCY, "USD")
            };
            
            FirebaseUserTracker.SetUserProperties();
            
            FirebaseAnalytics.LogEvent(FirebaseEventName.IAP_FAILED, firebaseParams);
        }
    }
}