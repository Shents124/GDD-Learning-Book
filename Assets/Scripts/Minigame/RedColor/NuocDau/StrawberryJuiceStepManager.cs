using System.Collections.Generic;
using Constant;
using Sound.Service;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStepManager : MonoBehaviour
    {
        public Button btnNext, btnBack;

        [SerializeField] private List<BaseStep> steps;

        private int _currentStep;
        private int _stepCount;

        private bool _isChangeStep;
        
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
            if (_isChangeStep)
                return;
            
            AdsManager.Instance.ShowInterstitial((result) => {
                
                AdTracker.LogAdInter(GetTrackingAdInter(), result);
                
                _isChangeStep = true;
                UIService.PlayFadeIn(() => {
                    Destroy(this.gameObject);
                    var step = LoadResourceService.LoadStep<MakeCakeManager>(PathConstants.MAKE_CAKE);
                });
            });
        }

        private async void OnClickedBackBtn()
        {
            AudioUtility.StopSFX();
            AudioUtility.PlayUISfx(AudioClipName.Button);
            await UIService.OpenActivityAsyncNoClose(ActivityType.MenuScreen);
            UIService.PlayFadeOut();
            Destroy(this.gameObject);
        }

        private void OnClickedNextBtn()
        {
            AudioUtility.StopSFX();
            AudioUtility.PlayUISfx(AudioClipName.Button);
            OnFinishAllStep();
        }
        
        private static TrackingAdInter GetTrackingAdInter()
        {
            return new TrackingAdInter {
                hasData = true,
                adLocation = AdLocation.start,
                levelName = LevelName.red,
                miniGameSession = "3",
                isWoaAd = false
            };
        }
    }
}