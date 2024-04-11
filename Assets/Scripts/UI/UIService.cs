using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
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
        
        public static void OpenActivityWithFadeIn(ActivityType activityType, bool playAnimation = true, 
            OnViewLoadedCallback onLoadedCallBack = null, bool closeLastActivity = true,
            params object[] args)
        {
            PlayFadeIn(() => 
                OpenActivityAsync(activityType, playAnimation, onLoadedCallBack, closeLastActivity, args).Forget());
        }

        // public static void OpenActivity(ActivityType activityType, bool playAnimation = true, 
        //     OnViewLoadedCallback onLoadedCallBack = null,
        //     params object[] args)
        // {
        //     var activityOption = new ActivityOptions(activityType.ToString(), playAnimation, onLoadedCallBack);
        //     var activityContainer = ActivityContainer.Find(UIConstant.ACTIVITY);
        //
        //     activityContainer.Show(activityOption, args);
        // }

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
        
        public static void PlayFadeIn(Action callback)
        {
            var activityContainer = ActivityContainer.Find(UIConstant.FADE_ACTIVITY);
            bool popupExist = activityContainer.TryGet(ActivityType.FadeScreen.ToString(), out var toast);

            if (popupExist == false)
                return;
            
            if (toast.View is FadeScreenActivity fadeScreenActivity)
            {
                fadeScreenActivity.FadeIn(callback);
            }
        }
        
        public static void PlayFadeOut(Action callback = null)
        {
            var activityContainer = ActivityContainer.Find(UIConstant.FADE_ACTIVITY);
            bool popupExist = activityContainer.TryGet(ActivityType.FadeScreen.ToString(), out var toast);

            if (popupExist == false)
                return;
            
            if (toast.View is FadeScreenActivity fadeScreenActivity)
            {
                fadeScreenActivity.FadeOut(callback);
            }
        }
    }
}