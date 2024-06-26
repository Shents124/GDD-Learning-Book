﻿using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Button;

public class ItemClick : MonoBehaviour
{
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    public ButtonClickedEvent onClick
    {
        get { return m_OnClick; }
        set { m_OnClick = value; }
    }

    public bool isMoveWithMouse = false;

    private bool isMoveBackStart = false;

    public TypeCallAction type = TypeCallAction.Click;

    public float threshold = 100f;

    private Vector2 startPos;

    private Vector2 startMousePosition;

    private Vector2 lastMousePosition;


    private void Start()
    {
        startPos = transform.position;
    }

    public void AddListener(UnityAction action)
    {
        m_OnClick.AddListener(action);
    }
    
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

    private void OnMouseUp()
    {
        if(isMoveBackStart)
        {
            transform.position = startPos;
        }
    }

    private void OnMouseDrag()
    {
        lastMousePosition = Input.mousePosition;
        if(isMoveWithMouse && type != TypeCallAction.Click)
        {
            transform.position = (Vector2) (Camera.main.ScreenToWorldPoint(lastMousePosition));
        }

        if (type == TypeCallAction.Click)
            return;

        switch (type)
        {
            case TypeCallAction.Up:
                if (startMousePosition.y - lastMousePosition.y < -threshold)
                {
                    m_OnClick?.Invoke();
                    isMoveBackStart = false;
                }
                else
                {
                    isMoveBackStart = true;
                }
                break;
            case TypeCallAction.Down:
                if (startMousePosition.y - lastMousePosition.y > threshold)
                {
                    m_OnClick?.Invoke();
                    isMoveBackStart = false;
                }
                else
                {
                    isMoveBackStart = true;
                }
                break;
            case TypeCallAction.Left:
                if (startMousePosition.x - lastMousePosition.x < -threshold)
                {
                    m_OnClick?.Invoke();
                    isMoveBackStart = false;
                }
                else
                {
                    isMoveBackStart = true;
                }
                break;
            case TypeCallAction.Right:
                if (startMousePosition.x - lastMousePosition.x > threshold)
                {
                    m_OnClick?.Invoke();
                    isMoveBackStart = false;
                }
                else
                {
                    isMoveBackStart = true;
                }
                break;
            
            case TypeCallAction.Drag:
                if (Vector3.Distance(startMousePosition, lastMousePosition) >= threshold)
                    m_OnClick?.Invoke();
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
    Left,
    Drag
}
