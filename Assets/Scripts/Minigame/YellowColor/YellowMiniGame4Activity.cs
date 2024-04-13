using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.YellowColor
{
    public class YellowMiniGame4Activity : BaseActivity
    {
        [SerializeField] private Button momChickenBtn;
        [SerializeField] private Button babyChickenBtn;
        [SerializeField] private GameObject momChickenDone;
        [SerializeField] private GameObject babyChickenDone;
        [SerializeField] private Step8Activity step8Activity;

        private int _currentStep;
        
        protected override void Start()
        {
            momChickenBtn.onClick.AddListener(OnClickedMomChicken);
            babyChickenBtn.onClick.AddListener(OnClickedBabyChicken);
        }
        
        protected override void InitializeData(Memory<object> args)
        {
            _currentStep = 0;
            base.InitializeData(args);
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