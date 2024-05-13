using Firebase.Analytics;
using Tracking.Constant;

namespace Tracking
{
    public enum AdLocation
    {
        memu, start, end, back
    }

    public enum AdStatus
    {
        request, impress, click
    }

    public static class AdTracker
    {
        public static void LogAdInter(TrackingAdInter trackingAdInter, bool isSuccess)
        {
            if (trackingAdInter.hasData == false)
                return;
            
            if (isSuccess)
            {
                LogAdInterSuccess(trackingAdInter.adLocation, trackingAdInter.levelName.ToString(),
                    trackingAdInter.miniGameSession, trackingAdInter.isWoaAd);
            }
            else
            {
                LogAdInterFailed(trackingAdInter.adLocation, trackingAdInter.levelName.ToString(),
                    trackingAdInter.miniGameSession, trackingAdInter.isWoaAd);
            }
        }
        
        private static void LogAdInterSuccess(AdLocation adLocation, string levelName, string miniGameSession, bool isWoaAds)
        {
            Parameter[] firebaseParams = {
                new(FirebaseParam.LOCATION, adLocation.ToString()),
                new(FirebaseParam.LEVEL_NAME, levelName),
                new(FirebaseParam.MINI_GAME_SESSION, miniGameSession),
                new (FirebaseParam.IS_WOA_ADS, isWoaAds.ToString())
            };

            FirebaseUserTracker.SetUserProperties();
            FirebaseAnalytics.LogEvent(FirebaseEventName.AD_INTER_SUCCESS, firebaseParams);
        }
        
        private static void LogAdInterFailed(AdLocation adLocation, string levelName, string miniGameSession, bool isWoaAds)
        {
            Parameter[] firebaseParams = {
                new(FirebaseParam.LOCATION, adLocation.ToString()),
                new(FirebaseParam.LEVEL_NAME, levelName),
                new(FirebaseParam.MINI_GAME_SESSION, miniGameSession),
                new (FirebaseParam.IS_WOA_ADS, isWoaAds.ToString())
            };

            FirebaseUserTracker.SetUserProperties();
            FirebaseAnalytics.LogEvent(FirebaseEventName.AD_INTER_FAILED, firebaseParams);
        }

        public static void LogAdInterStatus(AdStatus status)
        {
            FirebaseUserTracker.SetUserProperties();
            switch (status)
            {
                case AdStatus.request:
                    FirebaseAnalytics.LogEvent(FirebaseEventName.AD_INTER_REQUEST);
                    break;
                case AdStatus.click:
                    FirebaseAnalytics.LogEvent(FirebaseEventName.AD_INTER_CLICK);
                    break;
                case AdStatus.impress:
                    FirebaseAnalytics.LogEvent(FirebaseEventName.AD_INTER_IMPRESS);
                    break;
            }
        }

        public static void LogAdBannerStatus(AdStatus status)
        {
            FirebaseUserTracker.SetUserProperties();
            switch (status)
            {
                case AdStatus.request:
                    FirebaseAnalytics.LogEvent(FirebaseEventName.AD_BANNER_REQUEST);
                    break;
                case AdStatus.click:
                    FirebaseAnalytics.LogEvent(FirebaseEventName.AD_BANNER_CLICK);
                    break;
                case AdStatus.impress:
                    FirebaseAnalytics.LogEvent(FirebaseEventName.AD_BANNER_IMPRESS);
                    break;
            }
        }
    }
}