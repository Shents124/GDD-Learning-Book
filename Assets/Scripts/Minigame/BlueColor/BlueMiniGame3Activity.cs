using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.BlueColor
{
    public class BlueMiniGame3Activity : BaseActivity
    {
        [SerializeField] private Button pandoBtn;
        [SerializeField] private Button ballBtn;
        [SerializeField] private GameObject pandoDone;
        [SerializeField] private GameObject ballDone;
        [SerializeField] private Step8Activity step8Activity;

        private int _currentStep;
        
        protected override void Start()
        {
            pandoBtn.onClick.AddListener(OnClickedPando);
            ballBtn.onClick.AddListener(OnClickedBall);
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
                StartCoroutine(MoveToNextStep());
            }
        }

        private IEnumerator MoveToNextStep()
        {
            yield return new WaitForSeconds(1f);
            //UIService.OpenActivityWithFadeIn(ActivityType.MiniGameYellow5Screen);
        }
    }
}