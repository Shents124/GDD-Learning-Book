using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ArrangeCakeStep : BaseStep
{
    public GameObject cakeClick;

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
        UIService.PlayFadeIn(()=> 
        {
            UIService.PlayFadeOut();
            NextStep(); 
        });
    }

    public void DoneStep()
    {
        cakeClick.SetActive(true);
    }
}
