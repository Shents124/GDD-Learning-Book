using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CutAppleStep : BaseStep
{
    public List<CutApple> allStep;

    public GameObject applePeelDone;

    public GameObject knife;

    public Transform knifeStart;

    public int numberStep;

    private int currentStep;

    private void Start()
    {
        allStep[0].gameObject.SetActive(true);
        EventManager.Connect(Events.CutAppleDone, CompleteMiniStep);
        knife.transform.position = knifeStart.position;
        knife.transform.rotation = knifeStart.rotation;
        knife.SetActive(true);
    }

    private void OnDisable()
    {
        EventManager.Disconnect(Events.CutAppleDone, CompleteMiniStep);
    }

    public async void CompleteMiniStep()
    {
        await allStep[currentStep].OnDonePeel();
        allStep[currentStep].gameObject.SetActive(false);
        currentStep++;
        if (currentStep >= numberStep)
        {
            UIService.PlayFadeIn(() => {
                knife.SetActive(false);
                applePeelDone.SetActive(true);
                foreach (var peel in allStep)
                {
                    peel.gameObject.SetActive(false);
                    //peel.ResetStep();
                }
                NextStep();
                UIService.PlayFadeOut();
            });
        }
        else
        {
            allStep[currentStep].gameObject.SetActive(true);
        }
    }
}
