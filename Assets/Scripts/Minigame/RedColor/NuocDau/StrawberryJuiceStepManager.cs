using System.Collections.Generic;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStepManager : MonoBehaviour
    {
        [SerializeField] private List<BaseStep> steps;

        private int _currentStep = 0;

        private void OnFinishStep()
        {
            
        }
    }
}