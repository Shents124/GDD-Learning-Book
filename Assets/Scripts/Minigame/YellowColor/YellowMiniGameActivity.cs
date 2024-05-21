using System;
using DG.Tweening;
using Sound.Service;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class YellowMiniGameActivity : BaseActivity
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform step2Position;
        
        [SerializeField] private float durationMove = 1f;
        [SerializeField] private MomChicken momChicken;
        [SerializeField] private BasketEgg basketEgg;

        public override void DidEnter(Memory<object> args)
        {
            ProductTracking.miniGameSession = 1;
            ProductTracking.miniGameStep = 1;
            ProductTracking.step = 4;
            ProductTracking.LogMiniGameStart();
            
            MomChickenLayEgg();
            base.DidEnter(args);
        }

        private void MomChickenLayEgg()
        {
            momChicken.LayEgg(TransitionToStep2);
        }

        private void TransitionToStep2()
        {
            content.DOAnchorPos(step2Position.anchoredPosition, durationMove).OnComplete(() => {
                AudioUtility.PlaySFX(AudioClipName.Yellow_pickup_egg);
                basketEgg.Initialize(OnCollectEggsFinish, GetComponent<RectTransform>());
            });
        }

        private void OnCollectEggsFinish()
        {
            SetDataTrackingAd();
            UIService.OpenActivityWithFadeIn(ActivityType.MiniGameYellow2Screen, trackingAdInter: trackingAdInter);
        }

        protected override void SetDataTrackingAd()
        {
            trackingAdInter = new TrackingAdInter {
                hasData = true,
                levelName = LevelName.yellow,
                adLocation = AdLocation.start, 
                miniGameSession = "2", 
                isWoaAd = false
            };
        }
    }
}