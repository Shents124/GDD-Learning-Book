using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    public override async void NextStep()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        cakeClickBtn.SetActive(false);
        base.NextStep();
    }
    public void ChangeToBakeStep()
    {
        cakeDoneEndStep.DOFade(1, 0.75f).OnComplete(() => {
            NextStep();
        });
    }

    public void DoneStep()
    {
        cakeClick.SetActive(true);
    }
}
