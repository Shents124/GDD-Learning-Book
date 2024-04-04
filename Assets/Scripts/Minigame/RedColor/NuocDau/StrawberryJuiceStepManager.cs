using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStepManager : MonoBehaviour
    {
        [SerializeField] private List<BaseStep> steps;

        private int _currentStep;
        private int _stepCount;
        
        private void Start()
        {
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
            UIService.PlayFadeIn(() => {
                Destroy(this);
                UIService.OpenActivityAsync(ActivityType.Step7, closeLastActivity: false).Forget();
            });
        }
    }
}