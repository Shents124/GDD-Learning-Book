using System;
using System.Collections.Generic;
using Constant;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace MenuScreen
{
    public class MenuScreenActivity : Activity
    {
        [SerializeField] private List<MenuItem> menuItems;

        public override UniTask Initialize(Memory<object> args)
        {
            UIService.PlayFadeOut();
            
            foreach (var item in menuItems)
            {
                item.Initialize(OnChoseColor);
            }
            return base.Initialize(args);
        }

        private void OnChoseColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Red:
                    UIService.OpenActivityWithFadeIn(ActivityType.Step345RedScreen, args: colorType);
                    break;
                
                case ColorType.Yellow:
                    UIService.OpenActivityWithFadeIn(ActivityType.MiniGameYellowScreen, args: colorType);
                    break;
                
                case ColorType.Green:
                    UIService.OpenActivityWithFadeIn(ActivityType.Step345GreenScreen, args: colorType);
                    break;
                
                case ColorType.Blue:
                    UIService.OpenActivityWithFadeIn(ActivityType.Step345BlueScreen, args: colorType);
                    break;
            }
        }
    }
}