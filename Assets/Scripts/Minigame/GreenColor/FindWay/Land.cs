using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public Action<bool> actionDone;

    [SerializeField] private bool isCurrentLand;

    private void OnMouseEnter()
    {
        actionDone?.Invoke(isCurrentLand);
    }
}
