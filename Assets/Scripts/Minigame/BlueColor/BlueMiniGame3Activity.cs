using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Minigame.BlueColor
{
    public class BlueMiniGame3Activity : BaseActivity
    {
        [SpineAnimation(dataField: "animPlayer")]
        public string animTalk, animIdle;

        public SkeletonGraphic animPlayer;

        public Transform posFall;

        public GameObject bgPlayer;

        [SerializeField] private Button pandoBtn;
        [SerializeField] private Button ballBtn;
        [SerializeField] private GameObject pandoDone;
        [SerializeField] private GameObject ballDone;
        [SerializeField] private Step8Activity step8Activity;

        public Image screenAnim;

        public GameObject screenShoot;

        public GameObject DoneAll;

        private int _currentStep;
        
        protected override void Start()
        {
            pandoBtn.onClick.AddListener(OnClickedPando);
            ballBtn.onClick.AddListener(OnClickedBall);
            ShowTalk();
        }

        private async void ShowTalk()
        {
            await AsyncService.Delay(1, this);
            animPlayer.AnimationState.SetAnimation(0, animTalk, true);
            AudioUtility.PlaySFX(this, AudioClipName.Voice_coloring, 0, () => {
                animPlayer.AnimationState.SetAnimation(0, animIdle, true);
                animPlayer.transform.DOLocalMoveY(posFall.localPosition.y, 1).OnComplete(() => {
                    bgPlayer.SetActive(false);
                });
            });
        }

        protected override void InitializeData(Memory<object> args)
        {
            _currentStep = 0;
            base.InitializeData(args);
        }

        private void OnClickedPando()
        {
            _currentStep++;
            step8Activity.InitData(TypeObject.Pando, CheckNextStep);
            step8Activity.gameObject.SetActive(true);
        }

        private void OnClickedBall()
        {
            _currentStep++;
            step8Activity.InitData(TypeObject.Ball, CheckNextStep);
            step8Activity.gameObject.SetActive(true);
        }
        
        private void CheckNextStep(TypeObject typeObject)
        {
            if (typeObject == TypeObject.Pando)
            {
                pandoBtn.gameObject.SetActive(false);
                pandoDone.SetActive(true);
            }
            else if (typeObject == TypeObject.Ball)
            {
                ballBtn.gameObject.SetActive(false);
                ballDone.SetActive(true);
            }

            if (_currentStep >= 2)
            {
                CheckNextStep();
            }
        }


        private async void CheckNextStep()
        {
            await AsyncService.Delay(1f, this);
            screenAnim.gameObject.SetActive(true);
            screenAnim.DOFade(1, 0.25f).OnComplete(Action);
        }

        private async void Action()
        {
            screenShoot.SetActive(true);
            screenAnim.DOFade(0, 0.25f).OnComplete(() => { screenAnim.gameObject.SetActive(false); });
            AudioUtility.StopSFX();
            AudioUtility.PlaySFX(AudioClipName.Photo);
            await AsyncService.Delay(2.5f, this);
            AudioUtility.PlaySFX(AudioClipName.Congratulation_end);
            DoneAll.SetActive(true);
            await AsyncService.Delay(2.5f, this);
            UIService.OpenActivityWithFadeIn(nextActivity, screenAnim);
        }
    }
}