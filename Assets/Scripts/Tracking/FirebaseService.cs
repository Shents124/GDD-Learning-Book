using Cysharp.Threading.Tasks;
using Firebase;
using UnityEngine;

namespace Tracking
{
    public class FirebaseService
    { 
        private static FirebaseApp s_firebaseApp;
        
        public async UniTask Initialize()
        {
            var status = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();

            if (status != DependencyStatus.Available)
            {
                Debug.LogError($"[FIREBASE CORE] Could not resolve all Firebase dependencies: {status}");
                // Firebase Unity SDK is not safe to use
                return;
            }

            Debug.Log("[FIREBASE CORE] Initialize done");
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
#if UNITY_EDITOR
            var options = new AppOptions { ApiKey = "", AppId = "", ProjectId = "" };

            // use device name as dummy app name
            var appName = SystemInfo.deviceName;

            // create a dummy instance for testing-purpose only
            s_firebaseApp = FirebaseApp.Create(options, appName);
#else
            s_firebaseApp = FirebaseApp.DefaultInstance;
#endif
        }
    }
}