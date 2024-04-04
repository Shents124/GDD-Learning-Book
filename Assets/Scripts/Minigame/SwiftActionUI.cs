using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Button;

public class SwiftActionUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    public ButtonClickedEvent onClick
    {
        get { return m_OnClick; }
        set { m_OnClick = value; }
    }

    public TypeCallAction type = TypeCallAction.Click;

    public float threshold = 100f;

    private Vector2 startMousePosition;

    private Vector2 lastMousePosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startMousePosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        lastMousePosition = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (type == TypeCallAction.Click)
            return;

        switch (type)
        {
            case TypeCallAction.Up:
                if (startMousePosition.y - lastMousePosition.y < -threshold)
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
                if (startMousePosition.x - lastMousePosition.x > threshold)
                {
                    m_OnClick?.Invoke();
                }
                break;

        }
    }
}