using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnMouseEnter()
    {
        EventManager.SendSimpleEvent(Events.ErrorWay);
    }
}
