using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Spine.Unity;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace Step7
{
    public class Step7Activity : BaseActivity
    {
        [SpineAnimation]
        public string animTalk, animIdle;

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
            ShowTalk();
            EventManager.Connect(Events.FillColorDone, CheckNextStep);
            return base.Initialize(args);
        }
        private async void ShowTalk()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            var track = animPlayer.AnimationState.SetAnimation(0, animTalk, false);
            track.Complete += Entry => {
                animPlayer.AnimationState.SetAnimation(0, animIdle, true);
                animPlayer.transform.DOLocalMoveY(posFall.localPosition.y, 2f).OnComplete(() => {
                    bgPlayer.SetActive(false);
                });
            };
        }

        protected override void OnDisable()
        {
            EventManager.Disconnect(Events.FillColorDone, CheckNextStep);
        }

        public async void CheckNextStep()
        {
            if(currentStep >= stepToDone)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                screenAnim.gameObject.SetActive(true);
                screenAnim.DOFade(1, 0.25f).OnComplete(async () => {
                    screenShoot.SetActive(true);
                    screenAnim.DOFade(0, 0.25f).OnComplete(() => {
                        screenAnim.gameObject.SetActive(false);
                    });
                    await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
                    DoneAll.SetActive(true);
                    await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
                    AdsManager.Instance.ShowInterstitial(() => {
                        UIService.OpenActivityWithFadeIn(nextActivity, screenAnim);
                    });
                });
            }
        }
    }
}
