﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class ItemClick : MonoBehaviour
{
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    public TypeCallAction type = TypeCallAction.Click;

    public float threshold = 100f;

    private Vector2 startMousePosition;

    private Vector2 lastMousePosition;

    private void OnMouseDown()
    {
        if(type == TypeCallAction.Click)
        {
            m_OnClick?.Invoke();
        }
        else
        {
            startMousePosition = Input.mousePosition;
        }
    }

    private void OnMouseDrag()
    {
        lastMousePosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        if (type == TypeCallAction.Click)
            return;

        switch(type)
        {
            case TypeCallAction.Up:
                if(startMousePosition.y - lastMousePosition.y < -threshold)
                {
                    m_OnClick?.Invoke();
                }
                break;
            case TypeCallAction.Down:
                if (startMousePosition.y - lastMousePosition.y > threshold)
                {
                    m_OnClick?.Invoke();
                }
                break;
            case TypeCallAction.Left:
                if (startMousePosition.x - lastMousePosition.x < -threshold)
                {
                    m_OnClick?.Invoke();
                }
                break;
            case TypeCallAction.Right:
                if (startMousePosition.x - lastMousePosition.x  > threshold)
                {
                    m_OnClick?.Invoke();
                }
                break;

        }
    }
}

public enum TypeCallAction
{
    Click,
    Up,
    Down,
    Right,
    Left
}
