using System;
using ButtonUI;
using Cysharp.Threading.Tasks;
using Spine.Unity;
using UI;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace HomeScreen
{
    public class HomeScreenActivity : Activity
    {
        [SerializeField] private BaseButtonUI button;
        [SerializeField] private SkeletonGraphic skeletonGraphic;
        
        public override UniTask Initialize(Memory<object> args)
        {
            button.AddListener( OnClickedPlay);
            return base.Initialize(args);
        }
        
        private static void OnClickedPlay()
        {
            UIService.OpenActivityAsync(ActivityType.MenuScreen).Forget();
        }
    }
}