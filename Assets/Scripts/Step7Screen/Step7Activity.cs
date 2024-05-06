using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Spine.Unity;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace Step7
{
    public class Step7Activity : BaseActivity
    {
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
            ShowTalk();
            EventManager.Connect(Events.FillColorDone, CheckNextStep);
            return base.Initialize(args);
        }
        private async void ShowTalk()
        {
            await AsyncService.Delay(1f, this);
            var track = animPlayer.AnimationState.SetAnimation(0, animTalk, false);
            track.Complete += Entry => {
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
            };
        }

        protected override void OnDisable()
        {
            EventManager.Disconnect(Events.FillColorDone, CheckNextStep);
        }

        private async void CheckNextStep()
        {
            if(currentStep >= stepToDone)
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
}
