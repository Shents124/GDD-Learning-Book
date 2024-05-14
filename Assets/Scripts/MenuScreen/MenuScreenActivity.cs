using System;
using System.Collections.Generic;
using Constant;
using Cysharp.Threading.Tasks;
using Sound.Service;
using Tracking;
using Tracking.Constant;
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
            if (args.IsEmpty)
            {
                AudioUtility.PlayMusic(AudioClipName.HomeMenu);
            }
            
            UIService.PlayFadeOut();
            
            foreach (var item in menuItems)
            {
                item.Initialize(OnChoseColor);
            }
            return base.Initialize(args);
        }

        private void OnChoseColor(ColorType colorType)
        {
            AudioUtility.PlayUISfx(AudioClipName.Button);
            
            var trackingAdInter = new TrackingAdInter {
                hasData = true, adLocation = AdLocation.memu, miniGameSession = null, isWoaAd = false
            };

            ProductTracking.step = 1;
            
            switch (colorType)
            {
                case ColorType.Red:
                    trackingAdInter.levelName = LevelName.red;
                    ProductTracking.LogLevelStart(ProductLocation.menu, LevelName.red);
                    UIService.OpenActivityWithFadeIn(ActivityType.Step345RedScreen, trackingAdInter: trackingAdInter, args: colorType);
                    break;
                
                case ColorType.Yellow:
                    trackingAdInter.levelName = LevelName.yellow;
                    ProductTracking.LogLevelStart(ProductLocation.menu, LevelName.yellow);
                    UIService.OpenActivityWithFadeIn(ActivityType.Step345YellowScreen, trackingAdInter: trackingAdInter, args: colorType);
                    break;
                
                case ColorType.Green:
                    trackingAdInter.levelName = LevelName.green;
                    ProductTracking.LogLevelStart(ProductLocation.menu, LevelName.green);
                    UIService.OpenActivityWithFadeIn(ActivityType.Step345GreenScreen, trackingAdInter: trackingAdInter, args: colorType);
                    break;
                
                case ColorType.Blue:
                    trackingAdInter.levelName = LevelName.blue;
                    ProductTracking.LogLevelStart(ProductLocation.menu, LevelName.blue);
                    UIService.OpenActivityWithFadeIn(ActivityType.Step345BlueScreen, trackingAdInter: trackingAdInter,args: colorType);
                    break;
            }
        }
    }
}