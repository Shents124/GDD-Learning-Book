using System;
using System.Collections;
using Constant;
using Firebase.Analytics;
using Tracking.Constant;
using Unity.VisualScripting;
using UnityEngine;

namespace Tracking
{
    public static class FirebaseLevelTracker
    {
        public static void LogLevelStart(ColorType color, string location)
        {
            try
            {
                FirebaseUserTracker.SetUserProperties();

                var firebaseParams = new[] {
                    new Parameter(FirebaseParam.LOCATION, location),
                    new Parameter(FirebaseParam.NAME, color.ToString()),
                };

                FirebaseAnalytics.LogEvent(FirebaseEventName.LEVEL_START, firebaseParams);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void LogLevelEnd(ColorType color, string location, float duration, string result, string last_step)
        {
            try
            {
                FirebaseUserTracker.SetUserProperties();

                var firebaseParams = new[] {
                    new Parameter(FirebaseParam.LOCATION, location),
                    new Parameter(FirebaseParam.NAME, color.ToString()),
                    new Parameter(FirebaseParam.LAST_STEP, last_step),
                    new Parameter(FirebaseParam.DURATION, duration.ToString()),
                    new Parameter(FirebaseParam.RESULT, result),
                };

                FirebaseAnalytics.LogEvent(FirebaseEventName.LEVEL_START, firebaseParams);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void LogMinigameStart(string name, string location)
        {
            try
            {
                FirebaseUserTracker.SetUserProperties();

                var firebaseParams = new[] {
                    new Parameter(FirebaseParam.LOCATION, location),
                    new Parameter(FirebaseParam.NAME, name),
                };

                FirebaseAnalytics.LogEvent(FirebaseEventName.LEVEL_START, firebaseParams);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void LogMinigameEnd(string location, string name, string last_mini_game_session, string last_mini_game_step, float duration, string result)
        {
            try
            {
                FirebaseUserTracker.SetUserProperties();

                var firebaseParams = new[] {
                    new Parameter(FirebaseParam.LOCATION, location),
                    new Parameter(FirebaseParam.NAME, name),
                    new Parameter(FirebaseParam.LAST_MINI_GAME_SESSION, last_mini_game_session),
                    new Parameter(FirebaseParam.LAST_MINI_GAME_STEP, last_mini_game_step),
                    new Parameter(FirebaseParam.DURATION, duration.ToString()),
                    new Parameter(FirebaseParam.RESULT, result),
                };

                FirebaseAnalytics.LogEvent(FirebaseEventName.LEVEL_START, firebaseParams);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}