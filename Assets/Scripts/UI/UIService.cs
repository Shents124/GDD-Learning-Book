using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;
using ZBase.UnityScreenNavigator.Core.Views;

namespace UI
{
    public static class UIService
    {
        private static ActivityType s_currentActivityType = ActivityType.None;
    
        public static async UniTask OpenActivityAsync(ActivityType activityType, bool playAnimation = true, 
            OnViewLoadedCallback onLoadedCallBack = null, bool closeLastActivity = true,
            params object[] args)
        {
            
            var activityOption = new ActivityOptions(activityType.ToString(), playAnimation, onLoadedCallBack);
            var activityContainer = ActivityContainer.Find(UIConstant.ACTIVITY);

            await activityContainer.ShowAsync(activityOption, args);

            if (s_currentActivityType != ActivityType.None)
            {
                if (closeLastActivity)
                    await CloseActivityAsync(s_currentActivityType, false);
            }

            s_currentActivityType = activityType;
        }
        
        public static void OpenActivityWithFadeIn(ActivityType activityType, bool playAnimation = false, 
            OnViewLoadedCallback onLoadedCallBack = null, bool closeLastActivity = true, bool playAd = true,
            params object[] args)
        {
            if (playAd)
            {
                AdsManager.Instance.ShowInterstitial(() => {
                    PlayFadeIn(() => 
                        OpenActivityAsync(activityType, playAnimation, onLoadedCallBack, closeLastActivity, args).Forget());
                });
            }
            else
            {
                PlayFadeIn(() => 
                    OpenActivityAsync(activityType, playAnimation, onLoadedCallBack, closeLastActivity, args).Forget());
            }
        }
        
        public static async UniTask OpenActivityAsyncNoClose(ActivityType activityType, bool playAnimation = true,
            OnViewLoadedCallback onLoadedCallBack = null,
            params object[] args)
        {
            var activityOption = new ActivityOptions(activityType.ToString(), playAnimation, onLoadedCallBack);
            var activityContainer = ActivityContainer.Find(UIConstant.ACTIVITY);

            await activityContainer.ShowAsync(activityOption, args);

            s_currentActivityType = activityType;
        }

        public static void CloseActivity(ActivityType activityType, bool playAnimation)
        {
            var activityContainer = ActivityContainer.Find(UIConstant.ACTIVITY);
            activityContainer.Hide(activityType.ToString(), playAnimation);
        }
        
        public static async UniTask CloseActivityAsync(ActivityType activityType, bool playAnimation)
        {
            var activityContainer = ActivityContainer.Find(UIConstant.ACTIVITY);
            await activityContainer.HideAsync(activityType.ToString(), playAnimation);
        }

        public static async UniTask InitializeFadeScreen()
        {
            var activityContainer = ActivityContainer.Find(UIConstant.FADE_ACTIVITY);
            await activityContainer.ShowAsync(ActivityType.FadeScreen.ToString(), false);
        }
        
        public static void PlayFadeIn(Action callback, float timeDuration = 0.5f, float alphaDone = 1)
        {
            var activityContainer = ActivityContainer.Find(UIConstant.FADE_ACTIVITY);
            bool popupExist = activityContainer.TryGet(ActivityType.FadeScreen.ToString(), out var toast);

            if (popupExist == false)
                return;
            
            if (toast.View is FadeScreenActivity fadeScreenActivity)
            {
                fadeScreenActivity.FadeIn(callback, timeDuration, alphaDone);
            }
        }
        
        public static void PlayFadeOut(Action callback = null, float timeDuration = 0.5f)
        {
            var activityContainer = ActivityContainer.Find(UIConstant.FADE_ACTIVITY);
            bool popupExist = activityContainer.TryGet(ActivityType.FadeScreen.ToString(), out var toast);

            if (popupExist == false)
                return;
            
            if (toast.View is FadeScreenActivity fadeScreenActivity)
            {
                fadeScreenActivity.FadeOut(callback, timeDuration);
            }
        }
    }
}