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

        public Step8Activity stepFillColor;

        private int currentStep = 0;

        public void AddStep() => currentStep++;

        public override UniTask Initialize(Memory<object> args)
        {
            EventManager.Connect(Events.FillColorDone, CheckNextStep);
            return base.Initialize(args);
        }

        protected override void OnDisable()
        {
            EventManager.Disconnect(Events.FillColorDone, CheckNextStep);
        }

        public void CheckNextStep()
        {
            if(currentStep >= stepToDone)
            {
                //TODO: Next step capture
                Debug.Log("Next step capture");
            }
        }
    }
}
