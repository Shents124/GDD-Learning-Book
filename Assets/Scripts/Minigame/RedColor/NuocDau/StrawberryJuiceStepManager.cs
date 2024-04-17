﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStepManager : MonoBehaviour
    {
        public Button btnNext, btnBack;

        [SerializeField] private List<BaseStep> steps;

        private int _currentStep;
        private int _stepCount;
        
        private void Start()
        {
            btnBack.onClick.AddListener(OnClickedBackBtn);
            btnNext.onClick.AddListener(OnClickedNextBtn);
            foreach (var step in steps)
            {
                _stepCount++;
                step.Initialize(OnFinishStep);
            }

            SetFirstStep();
        }

        private void SetFirstStep()
        {
            _currentStep = 0;
            steps[0].InActive();
        }

        private void ChangeStep()
        {
            steps[_currentStep].gameObject.SetActive(false);
            _currentStep++;
            steps[_currentStep].InActive();
            
            UIService.PlayFadeOut();
        }
        
        private void OnFinishStep()
        {
            if (_currentStep >= _stepCount - 1)
            {
                OnFinishAllStep();
            }
            else
            {
                UIService.PlayFadeIn(ChangeStep);
            }
        }
        
        
        private void OnFinishAllStep()
        {
            AdsManager.Instance.ShowInterstitial(() => {
                
                UIService.PlayFadeIn(() => {
                    Destroy(this.gameObject);
                    UIService.OpenActivityAsync(ActivityType.Step7Red, closeLastActivity: false).Forget();
                });
            });
            
        }

        private async void OnClickedBackBtn()
        {
            await UIService.OpenActivityAsync(ActivityType.MenuScreen);
            UIService.PlayFadeOut();
            Destroy(this.gameObject);
        }

        private void OnClickedNextBtn()
        {
            UIService.PlayFadeIn(() => {
                UIService.PlayFadeOut();
                ChangeStep();
            });

        }
    }
}