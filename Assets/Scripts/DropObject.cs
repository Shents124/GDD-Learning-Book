using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour, IDropHandler, IDragHandler
{
    private Action<PointerEventData> _onDrop;
    
    public void Initialize(Action<PointerEventData> onDrop)
    {
        _onDrop = onDrop;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            _onDrop?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}