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
        public override UniTask Initialize(Memory<object> args)
        {
            AudioUtility.PlayMusic(AudioClipName.HomeMenu);
            button.AddListener(OnClickedPlay);
            return base.Initialize(args);
        }
        
        private static void OnClickedPlay()
        {
            AudioUtility.PlayUISfx(AudioClipName.Button);
            UIService.OpenActivityWithFadeIn(ActivityType.MenuScreen, args: true);
        }
    }
}