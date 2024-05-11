using Firebase.Analytics;
using Tracking.Constant;

namespace Tracking
{
    public enum AdLocation
    {
        memu, start, end, back
    }
    
    public static class AdTracker
    {
        public static void LogAdEvent(string eventName)
        {
            FirebaseUserTracker.SetUserProperties();
            FirebaseAnalytics.LogEvent(eventName);
        }

        public static void LodAdInterSuccess(AdLocation adLocation, string levelName, string miniGameSession, bool isWoaAds)
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
        
        public static void LodAdInterFailed(AdLocation adLocation, string levelName, string miniGameSession, bool isWoaAds)
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
    }
}