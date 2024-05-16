using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;
using static System.Net.WebRequestMethods;

namespace IAP
{
    public class RemoveAdsActivity : Activity
    {
        public Button btnClose;
        public Button btnPrivacy;
        public Button btnTerms;

        private const string linkPrivacy =  "https://wolfoogames.com/privacy-policy.html";
        private const string linkTerms = "https://wolfoogames.com/terms-conditions.html";

        public override UniTask Initialize(Memory<object> args)
        {
            btnPrivacy.onClick.AddListener(()=> { OpenUrl(linkPrivacy); });
            btnTerms.onClick.AddListener(()=> { OpenUrl(linkTerms); });
            btnClose.onClick.AddListener(OnClose);
            IapTracker.LogIapUI(FirebaseEventName.IAP_POPUP_SHOW);
            return base.Initialize(args);
        }

        public override void DidExit(Memory<object> args)
        {
            IapTracker.LogIapUI(FirebaseEventName.IAP_POPUP_SHOW);
            base.DidExit(args);
        }

        private void OpenUrl(string url)
        {
            Application.OpenURL(url);
        }

        private void OnClose()
        {
            UIService.OpenActivityAsync(ActivityType.HomeScreen).Forget();
        }
    }
}