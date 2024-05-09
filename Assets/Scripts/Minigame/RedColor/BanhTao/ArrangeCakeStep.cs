using DG.Tweening;
using Sound.Service;
using UnityEngine;
using Utility;

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
        await AsyncService.Delay(1, this);
        cakeClickBtn.SetActive(false);
        base.NextStep();
    }
    public void ChangeToBakeStep()
    {
        AudioUtility.PlaySFX(AudioClipName.Clearstep);
        cakeDoneEndStep.DOFade(1, 0.75f).OnComplete(NextStep);
    }

    public void DoneStep()
    {
        cakeClick.SetActive(true);
    }
}
