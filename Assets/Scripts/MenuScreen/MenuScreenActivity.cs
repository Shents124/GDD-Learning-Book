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
                    UIService.OpenActivityAsync(ActivityType.Step345RedScreen, args: colorType).Forget();
                    break;
                
                case ColorType.Yellow:
                    UIService.OpenActivityAsync(ActivityType.Step345YellowScreen, args: colorType).Forget();
                    break;
                
                case ColorType.Green:
                    break;
                
                case ColorType.Blue:
                    break;
            }
        }
    }
}