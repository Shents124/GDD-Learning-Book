using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    [SerializeField] private bool isCurrentLand;

    private void OnMouseEnter()
    {
        if (!isCurrentLand)
        {
            EventManager.SendSimpleEvent(Events.ErrorWay);
        }
        else
        {
            EventManager.SendSimpleEvent(Events.CurrentWay);
        }
    }
}
