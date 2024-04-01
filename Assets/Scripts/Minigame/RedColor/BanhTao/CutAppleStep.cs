using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutAppleStep : BaseStep
{
    public List<CutApple> allStep;

    public GameObject applePeelDone;

    public GameObject knife;

    public int numberStep;

    private int currentStep;

    private void Start()
    {
        allStep[0].gameObject.SetActive(true);
        EventManager.Connect(Events.CutAppleDone, CompleteMiniStep);
    }

    private void OnDisable()
    {
        EventManager.Disconnect(Events.CutAppleDone, CompleteMiniStep);
    }

    public async void CompleteMiniStep()
    {
        await allStep[currentStep].OnDonePeel();
        currentStep++;
        if (currentStep >= numberStep)
        {
            knife.SetActive(false);
            applePeelDone.SetActive(true);
            foreach (var peel in allStep)
            {
                peel.gameObject.SetActive(false);
                //peel.ResetStep();
            }
            NextStep();
        }
        else
        {
            allStep[currentStep].gameObject.SetActive(true);
        }
    }
}
