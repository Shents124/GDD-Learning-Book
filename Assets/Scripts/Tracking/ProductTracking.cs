using Firebase.Analytics;
using Tracking.Constant;
using UnityEngine;

namespace Tracking
{
    public enum ProductLocation
    {
        menu, next_color, auto
    }

    public enum ResultType
    {
        win, back, quit
    }
    
    public static class ProductTracking
    {
        public static int step;
        public static int miniGameSession;
        public static int miniGameStep;
        
        private static float s_startTimePlay;
        private static ProductLocation s_startLocation;
        private static LevelName s_levelName;
        private static bool s_inMiniGame;
        
        public static void LogLevelStart(ProductLocation location, LevelName levelName)
        {
            s_startTimePlay = Time.time;
            s_startLocation = location;
            s_levelName = levelName;
            
            FirebaseUserTracker.SetUserProperties();
            
            Parameter[] firebaseParams = {
                new(FirebaseParam.LOCATION, location.ToString()),
                new(FirebaseParam.LEVEL_NAME, levelName.ToString())
            };
            
            FirebaseAnalytics.LogEvent(FirebaseEventName.LEVEL_START, firebaseParams);
        }

        public static void LogLevelEnd(ResultType resultType)
        {
            FirebaseUserTracker.SetUserProperties();
            
            Parameter[] firebaseParams = {
                new(FirebaseParam.LOCATION, s_startLocation.ToString()),
                new(FirebaseParam.LEVEL_NAME, s_levelName.ToString()),
                new (FirebaseParam.LAST_STEP, step),
                new (FirebaseParam.DURATION, (long) (Time.time - s_startTimePlay)),
                new (FirebaseParam.RESULT, resultType.ToString())
            };
            
            FirebaseAnalytics.LogEvent(FirebaseEventName.LEVEL_END, firebaseParams);
        }

        public static void LogMiniGameStart()
        {
            s_inMiniGame = true;
            FirebaseUserTracker.SetUserProperties();
            
            Parameter[] firebaseParams = {
                new(FirebaseParam.LOCATION, s_startLocation.ToString()),
                new(FirebaseParam.LEVEL_NAME, s_levelName.ToString())
            };
            
            FirebaseAnalytics.LogEvent(FirebaseEventName.MINI_GAME_START, firebaseParams);
        }

        public static void LogMiniGameEnd(ResultType resultType)
        {
            if (s_inMiniGame == false)
                return;
            
            FirebaseUserTracker.SetUserProperties();
            
            Parameter[] firebaseParams = {
                new(FirebaseParam.LOCATION, s_startLocation.ToString()),
                new(FirebaseParam.LEVEL_NAME, s_levelName.ToString()),
                new (FirebaseParam.LAST_MINI_GAME_STEP, miniGameSession),
                new (FirebaseParam.LAST_MINI_GAME_STEP, miniGameStep),
                new (FirebaseParam.DURATION, (long) (Time.time - s_startTimePlay)),
                new (FirebaseParam.RESULT, resultType.ToString())
            };
            
            FirebaseAnalytics.LogEvent(FirebaseEventName.MINI_GAME_END, firebaseParams);
            s_inMiniGame = true;
        }
    }
}