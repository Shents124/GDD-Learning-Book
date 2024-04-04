using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI;
using UnityEngine;

public class ArrangeCakeStep : BaseStep
{
    public GameObject cakeClick;

    public GameObject cakeClickBtn;

    public SpriteRenderer cakeDoneEndStep;

    private void Start()
    {
        EventManager.Connect(Events.ArrangeCakeDone, DoneStep);
    }

    private void OnDisable()
    {
        EventManager.Disconnect(Events.ArrangeCakeDone, DoneStep);
    }

    public void ChangeToBakeStep()
    {
        cakeDoneEndStep.DOFade(1, 0.75f).OnComplete(() => {
            cakeClickBtn.SetActive(false);
            UIService.PlayFadeIn(() =>
            {
                UIService.PlayFadeOut();
                NextStep();
            });
        });
    }

    public void DoneStep()
    {
        cakeClick.SetActive(true);
    }
}
