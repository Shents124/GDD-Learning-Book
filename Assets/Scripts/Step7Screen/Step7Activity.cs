using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace Step7
{
    public class Step7Activity : Activity
    {
        public int stepToDone = 2;

        private int currentStep = 0;

        public void AddStep() => currentStep++;

        public GameObject[] _UIStepColors;

        public override UniTask Initialize(Memory<object> args)
        {
            foreach (var item in _UIStepColors)
            {
                item.SetActive(false);
            }

            return base.Initialize(args);
        }

        public bool CheckDoneStep => currentStep >= stepToDone;
    }
}
