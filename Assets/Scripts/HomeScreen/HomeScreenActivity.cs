using System;
using ButtonUI;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace HomeScreen
{
    public class HomeScreenActivity : Activity
    {
        [SerializeField] private BaseButtonUI button;

        public override UniTask Initialize(Memory<object> args)
        {
            button.AddListener(() =>  OnClickedPlay().Forget());
            return base.Initialize(args);
        }

        private static async UniTask OnClickedPlay()
        {
            await UIService.OpenActivityAsync(ActivityType.MenuScreen);
            await UIService.CloseActivityAsync(ActivityType.HomeScreen, false);
        }
    }
}