using System;
using System.Collections;
using Sound.Service;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class YellowMiniGame3Activity : BaseActivity
    {
        [SerializeField] private float duration = 2f;
        
        public override void DidEnter(Memory<object> args)
        {
            AudioUtility.PlaySFX(AudioClipName.Chicken_all);
            StartCoroutine(NextMiniGame());
            base.DidEnter(args);
        }

        private IEnumerator NextMiniGame()
        {
            yield return new WaitForSeconds(duration);
            SetDataTrackingAd();
            UIService.OpenActivityWithFadeIn(ActivityType.MiniGameYellow4Screen, trackingAdInter: trackingAdInter);
        }
        
        protected override void SetDataTrackingAd()
        {
            trackingAdInter = new TrackingAdInter {
                hasData = true,
                levelName = LevelName.yellow,
                adLocation = AdLocation.end, 
                miniGameSession = "2", 
                isWoaAd = false
            };
        }
    }
}