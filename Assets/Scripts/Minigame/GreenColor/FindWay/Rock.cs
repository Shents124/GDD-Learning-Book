using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Action moveToRock;

    private void OnMouseEnter()
    {
        moveToRock?.Invoke();
    }
}
