using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Spine.Unity;
using UI;
using UnityEngine;
using UnityEngine.UI;

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
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            var track = animPlayer.AnimationState.SetAnimation(0, animTalk, false);
            track.Complete += Entry => {
                animPlayer.AnimationState.SetAnimation(0, animIdle, true);
                animPlayer.transform.DOLocalMoveY(posFall.localPosition.y, 2f).OnComplete(() => {
                    bgPlayer.SetActive(false);
                });
            };
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

        private IEnumerator MoveToNextStep()
        {
            yield return new WaitForSeconds(1f);
            //UIService.OpenActivityWithFadeIn(ActivityType.MiniGameYellow5Screen);
        }

        public async void CheckNextStep()
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
                UIService.OpenActivityWithFadeIn(nextActivity, screenAnim);
            });
        }
    }
}