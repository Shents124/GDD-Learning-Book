using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStep : MonoBehaviour
{
    public Action actionNext {  get; set; }
    public Action actionPrev { get; set; }

    public BaseStep previousStep;

    public BaseStep nextStep;

    public virtual void InActive()
    {
        ResetStep();
        gameObject.SetActive(true);
    }

    public virtual void ResetStep()
    {

    }

    public virtual void NextStep()
    {
        actionNext?.Invoke();
        if (nextStep == null)
            return;
        ResetStep();
        this.gameObject.SetActive(false);
        nextStep.InActive();
    }

    public virtual void BackStep()
    {
        actionPrev?.Invoke();
        if (previousStep == null)
        {
            return;
        }
        ResetStep();
        this.gameObject.SetActive(false);
        previousStep.InActive();
    }

    public void ConnectStep(BaseStep stepNext, BaseStep stepPrevious)
    {
        previousStep = stepPrevious;
        nextStep = stepNext;
    }
}
