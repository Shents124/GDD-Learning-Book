using System;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Minigame.YellowColor
{
    public class YellowMiniGame4Activity : BaseActivity
    {
        [SpineAnimation(dataField: "animPlayer")]
        public string animTalk, animIdle;

        public SkeletonGraphic animPlayer;

        public Transform posFall, posStart;

        public GameObject bgPlayer;

        [SerializeField] private Button momChickenBtn;
        [SerializeField] private Button babyChickenBtn;
        [SerializeField] private GameObject momChickenDone;
        [SerializeField] private GameObject babyChickenDone;
        [SerializeField] private Step8Activity step8Activity;

        public Image screenAnim;

        public GameObject screenShoot;

        public GameObject DoneAll, BgReadyToScreenShot, BgDisableToScreenShot;

        private int _currentStep;
        
        protected override void Start()
        {
            ProductTracking.step = 5;
            momChickenBtn.onClick.AddListener(OnClickedMomChicken);
            babyChickenBtn.onClick.AddListener(OnClickedBabyChicken);
            ShowTalk();
        }

        private void ShowTalk()
        {
            animPlayer.transform.DOLocalMoveY(posStart.localPosition.y, 1).OnComplete(() => {
                animPlayer.AnimationState.SetAnimation(0, animTalk, true);
                AudioUtility.PlaySFX(this, AudioClipName.Voice_coloring, 0, () => {
                    animPlayer.AnimationState.SetAnimation(0, animIdle, true);
                    animPlayer.transform.DOLocalMoveY(posFall.localPosition.y, 1).OnComplete(() => {
                        bgPlayer.SetActive(false);
                    });
                });
            });
        }

        protected override void InitializeData(Memory<object> args)
        {
            _currentStep = 0;
            base.InitializeData(args);
        }

        protected override void OnClickedNextBtn()
        {
            ProductTracking.LogLevelStart(ProductLocation.next_color, LevelName.green);
            base.OnClickedNextBtn();
        }

        private void OnClickedMomChicken()
        {
            _currentStep++;
            step8Activity.InitData(TypeObject.MomChicken, CheckNextStep);
            step8Activity.gameObject.SetActive(true);
        }

        private void OnClickedBabyChicken()
        {
            _currentStep++;
            step8Activity.InitData(TypeObject.BabyChicken, CheckNextStep);
            step8Activity.gameObject.SetActive(true);
        }
        
        private void CheckNextStep(TypeObject typeObject)
        {
            if (typeObject == TypeObject.MomChicken)
            {
                momChickenBtn.gameObject.SetActive(false);
                momChickenDone.SetActive(true);
            }
            else if (typeObject == TypeObject.BabyChicken)
            {
                babyChickenBtn.gameObject.SetActive(false);
                babyChickenDone.SetActive(true);
            }

            if (_currentStep >= 2)
            {
                CheckNextStep();
            }
        }
        
        private async void CheckNextStep()
        {
            AudioUtility.StopSFX();
            AudioUtility.PlaySFX(AudioClipName.Hooray_girl, true);
            ProductTracking.step = 6;
            BgReadyToScreenShot.SetActive(true);
            BgDisableToScreenShot.SetActive(false);
            await AsyncService.Delay(3f, this);
            screenAnim.gameObject.SetActive(true);
            AudioUtility.StopSFX();
            AudioUtility.PlaySFX(AudioClipName.Photo);
            screenAnim.DOFade(1, 0.25f).OnComplete(async () => {
                screenShoot.SetActive(true);
                screenAnim.DOFade(0, 0.25f).OnComplete(() => {
                    screenAnim.gameObject.SetActive(false);
                });
                await AsyncService.Delay(2.5f, this);
                AudioUtility.PlaySFX(AudioClipName.Congratulation_end);
                DoneAll.SetActive(true);
                await AsyncService.Delay(2.5f, this);
                SetDataTrackingAd();
                
                ProductTracking.LogLevelEnd(ResultType.win);
                ProductTracking.LogLevelStart(ProductLocation.auto, LevelName.green);
                UIService.OpenActivityWithFadeIn(nextActivity, screenAnim, trackingAdInter: trackingAdInter);
            });
        }

        protected override void SetDataTrackingAd()
        {
            trackingAdInter = new TrackingAdInter {
                hasData = true,
                levelName = LevelName.yellow,
                adLocation = AdLocation.end, 
                miniGameSession = null, 
                isWoaAd = false
            };
        }
    }
}