using Cysharp.Threading.Tasks;
using ZBase.UnityScreenNavigator.Core.Activities;
using ZBase.UnityScreenNavigator.Core.Views;

namespace UI
{
    public static class UIService
    {
        private  static  ActivityType s_currentActivityType = ActivityType.None;
    
        public static async UniTask OpenActivityAsync(ActivityType activityType, bool playAnimation = true, 
            OnViewLoadedCallback onLoadedCallBack = null,
            params object[] args)
        {
            var activityOption = new ActivityOptions(activityType.ToString(), playAnimation, onLoadedCallBack);
            var activityContainer = ActivityContainer.Find(UIConstant.ACTIVITY);

            await activityContainer.ShowAsync(activityOption, args);

            if (s_currentActivityType != ActivityType.None)
            {
                await CloseActivityAsync(s_currentActivityType, false);
            }

            s_currentActivityType = activityType;
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
    }
}