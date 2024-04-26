using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI;
using UnityEngine;

public class CutAppleStep : BaseStep
{
    public List<CutApple> allStep;

    public GameObject knife;

    public Transform knifeStart, knifeDone;

    public int numberStep;

    private int currentStep;

    public ParticleSystem vfxDone;

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

    private void ShowDoneStep()
    {
        knife.GetComponent<Knife>().enabled = false;
        knife.transform.position = knifeStart.position;
        transform.DOScale(1.2f, 1f);
        knife.transform.DOMove(knifeDone.position, 1f).OnComplete(async () => {
            vfxDone.gameObject.SetActive(true);
            knife.gameObject.SetActive(false);
            await UniTask.Delay(System.TimeSpan.FromSeconds(1.25f));
            UIService.PlayFadeIn(() => {
                NextStep();
                UIService.PlayFadeOut();
            });
        });
    }

    public async void CompleteMiniStep()
    {
        await allStep[currentStep].OnDonePeel();
        currentStep++;
        if (currentStep >= numberStep)
        {
            ShowDoneStep();
        }
        else
        {
            allStep[currentStep - 1].gameObject.SetActive(false);
            allStep[currentStep].gameObject.SetActive(true);
        }
    }
}
