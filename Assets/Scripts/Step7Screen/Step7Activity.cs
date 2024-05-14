using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using Tracking.Constant;
using Tracking;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Step7
{
    public class Step7Activity : BaseActivity
    {
        public LevelName levelName;

        [SpineAnimation]
        public string animTalk, animIdle, animRun;

        public SkeletonGraphic animPlayer;

        public Transform posFall;

        public GameObject bgPlayer;

        public int stepToDone = 2;

        public Step8Activity stepFillColor;

        private int currentStep = 0;

        public void AddStep() => currentStep++;

        public Image screenAnim;

        public GameObject screenShoot;

        public GameObject DoneAll;

        public override UniTask Initialize(Memory<object> args)
        {
            ProductTracking.step = 5;
            ShowTalk();
            EventManager.Connect(Events.FillColorDone, CheckNextStep);
            return base.Initialize(args);
        }
        private async void ShowTalk()
        {
            await AsyncService.Delay(1f, this);
            animPlayer.AnimationState.SetAnimation(0, animTalk, true);
            AudioUtility.PlaySFX(this, AudioClipName.Voice_coloring, 0, () => {
                animPlayer.AnimationState.SetAnimation(0, animRun, true);
                if (posFall.localPosition.x - animPlayer.transform.localPosition.x > 0)
                {
                    animPlayer.transform.localScale = new Vector2(-Math.Abs(animPlayer.transform.localScale.x), animPlayer.transform.localScale.y);
                }
                else
                {
                    animPlayer.transform.localScale = new Vector2(Math.Abs(animPlayer.transform.localScale.x), animPlayer.transform.localScale.y);
                }
                animPlayer.transform.DOLocalMove(posFall.localPosition, 1f).OnComplete(() => {
                    bgPlayer.SetActive(false);
                });
            });
        }

        protected override void OnDisable()
        {
            EventManager.Disconnect(Events.FillColorDone, CheckNextStep);
        }

        private async void CheckNextStep()
        {
            if(currentStep >= stepToDone)
            {
                ProductTracking.step = 6;
                await AsyncService.Delay(1f, this);
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
                    ProductTracking.LogLevelStart(ProductLocation.auto, LevelName.yellow);
                    UIService.OpenActivityWithFadeIn(nextActivity, screenAnim, trackingAdInter: trackingAdInter);
                });
            }
        }
        
        protected override void OnClickedNextBtn()
        {
            ProductTracking.LogLevelStart(ProductLocation.next_color, levelName);
            base.OnClickedNextBtn();
        }
        
        protected override void SetDataTrackingAd()
        {
            trackingAdInter = new TrackingAdInter {
                hasData = true,
                levelName = this.levelName,
                adLocation = AdLocation.end,
                miniGameSession = null,
                isWoaAd = false
            };
        }
    }
}
