using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStep : MonoBehaviour
{
    public BaseStep previousStep;

    public BaseStep nextStep;

    public virtual void ResetStep()
    {

    }

    public virtual void NextStep()
    {
        if (nextStep == null)
            return;
        ResetStep();
        this.gameObject.SetActive(false);
        nextStep.gameObject.SetActive(true);
        nextStep.ResetStep();
    }

    public virtual void BackStep()
    {
        if (previousStep == null)
        {
            return;
        }
        ResetStep();
        this.gameObject.SetActive(false);
        previousStep.gameObject.SetActive(true);
        previousStep.ResetStep();
    }

    public void ConnectStep(BaseStep stepNext, BaseStep stepPrevious)
    {
        previousStep = stepPrevious;
        nextStep = stepNext;
    }
}
