using System;
using ButtonUI;
using Cysharp.Threading.Tasks;
using Sound.Service;
using Tracking;
using UI;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace HomeScreen
{
    public class HomeScreenActivity : Activity
    {
        [SerializeField] private BaseButtonUI button;
        [SerializeField] private BaseButtonUI buttonRemoveAds;
        public override UniTask Initialize(Memory<object> args)
        {
            AudioUtility.PlayMusic(AudioClipName.HomeMenu);
            button.AddListener(OnClickedPlay);
            buttonRemoveAds.AddListener(OnClickedRemoveAds);
            return base.Initialize(args);
        }
        
        private static void OnClickedPlay()
        {
            AudioUtility.PlayUISfx(AudioClipName.Button);
            UIService.OpenActivityWithFadeIn(ActivityType.MenuScreen, args: true);
        }

        private void OnClickedRemoveAds()
        {
            IapTracker.LogIapButton();
            AudioUtility.PlayUISfx(AudioClipName.Button);
            UIService.OpenActivityAsync(ActivityType.IapActivity).Forget();
        }
    }
}