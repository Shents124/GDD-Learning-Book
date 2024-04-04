using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
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
        allStep[currentStep].gameObject.SetActive(false);
        currentStep++;
        if(currentStep >= numberStep)
        {
            applePeelDone.SetActive(true);
            await UniTask.Delay(System.TimeSpan.FromSeconds(0.75f));
            knife.type = TypeKnife.Cut;
            NextStep();
            //foreach (var peel in allStep)
            //{
            //    peel.gameObject.SetActive(false);
            //    peel.ResetStep();
            //}

        }
        else
        {
            knife.gameObject.SetActive(true);
            allStep[currentStep].gameObject.SetActive(true);
        }
    }
}
