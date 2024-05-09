using System;
using System.Collections;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
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

        public Transform posFall;

        public GameObject bgPlayer;

        [SerializeField] private Button momChickenBtn;
        [SerializeField] private Button babyChickenBtn;
        [SerializeField] private GameObject momChickenDone;
        [SerializeField] private GameObject babyChickenDone;
        [SerializeField] private Step8Activity step8Activity;

        public Image screenAnim;

        public GameObject screenShoot;

        public GameObject DoneAll;

        private int _currentStep;
        
        protected override void Start()
        {
            momChickenBtn.onClick.AddListener(OnClickedMomChicken);
            babyChickenBtn.onClick.AddListener(OnClickedBabyChicken);
            ShowTalk();
        }

        private async void ShowTalk()
        {
            await AsyncService.Delay(1f, this);
            var track = animPlayer.AnimationState.SetAnimation(0, animTalk, false);
            track.Complete += Entry => {
                animPlayer.AnimationState.SetAnimation(0, animIdle, true);
                animPlayer.transform.DOLocalMoveY(posFall.localPosition.y, 1).OnComplete(() => {
                    bgPlayer.SetActive(false);
                });
            };
        }

        protected override void InitializeData(Memory<object> args)
        {
            _currentStep = 0;
            base.InitializeData(args);
        }

        protected override void OnClickedNextBtn()
        {
            AudioUtility.PlayUISfx(AudioClipName.Button);
            UIService.OpenActivityWithFadeIn(nextActivity, playAd: false);
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

        private IEnumerator MoveToNextStep()
        {
            yield return new WaitForSeconds(1f);
            //UIService.OpenActivityWithFadeIn(ActivityType.MiniGameYellow5Screen);
        }

        private async void CheckNextStep()
        {
            await AsyncService.Delay(1f, this);
            screenAnim.gameObject.SetActive(true);
            screenAnim.DOFade(1, 0.25f).OnComplete(async () => {
                screenShoot.SetActive(true);
                screenAnim.DOFade(0, 0.25f).OnComplete(() => {
                    screenAnim.gameObject.SetActive(false);
                });
                await AsyncService.Delay(2.5f, this);
                DoneAll.SetActive(true);
                await AsyncService.Delay(2.5f, this);
                UIService.OpenActivityWithFadeIn(nextActivity, screenAnim);
            });
        }
    }
}