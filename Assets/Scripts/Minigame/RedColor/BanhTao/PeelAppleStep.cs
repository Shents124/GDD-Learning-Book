using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeelAppleStep : BaseStep
{
    public Knife knife;

    public List<PeelApple> allStep;

    public GameObject applePeelDone;

    public int numberStep;
    private int currentStep;

    private void Start()
    {
        allStep[0].gameObject.SetActive(true);
        EventManager.Connect(Events.PeelAppleDone, CompleteMiniStep);
    }

    private void OnDisable()
    {
        EventManager.Disconnect(Events.PeelAppleDone, CompleteMiniStep);
    }

    public async void CompleteMiniStep()
    {
        knife.DisActiveKnife();
        await allStep[currentStep].OnDonePeel();
        knife.gameObject.SetActive(true);
        allStep[currentStep].gameObject.SetActive(false);
        currentStep++;
        if(currentStep >= numberStep)
        {
            //Lên step
            knife.type = TypeKnife.Cut;
            applePeelDone.SetActive(true);
            NextStep();
            foreach(var peel in allStep)
            {
                peel.gameObject.SetActive(false);
                peel.ResetStep();
            }
        }
        else
        {
            allStep[currentStep].gameObject.SetActive(true);
        }
    }
}
