using System.Collections;
using System.Collections.Generic;
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
        NextStep();
    }

    public void DoneStep()
    {
        cakeClick.SetActive(true);
    }
}
