using Firebase.Analytics;
using SaveLoad;
using Tracking.Constant;

namespace Tracking
{
    public static class FirebaseUserTracker
    {
        public static void SetUserProperties()
        {
            var userType = SaveLoadService.LoadUserType();
            
            FirebaseAnalytics.SetUserProperty(FirebaseUserProperty.USER_TYPE, userType.ToString());
        }
    }
}