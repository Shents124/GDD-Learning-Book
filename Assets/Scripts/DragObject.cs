using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(CanvasGroup), typeof(GraphicRaycaster))]
public class DragObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler,
    IInitializePotentialDragHandler, IPointerUpHandler
{
    [SerializeField] private float threshold = 10f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    [SerializeField] private int maxSortingOrder = 1002; 
    
    private RectTransform _rectTransform;
    private Vector3 _originPos;
    
    private Action _callback;
    private bool _isDrag;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originPos = _rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponent<Canvas>();
        canvas.overrideSorting = false;
    }
    
    public void Initialize(Action callback)
    {
        _callback = callback;
    }
    
    public void Initialize(Action callback, RectTransform rectTransform)
    {
        _callback = callback;
        _originPos = rectTransform.anchoredPosition;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;
        canvasGroup.blocksRaycasts = false;
        canvas.overrideSorting = true;
        canvas.sortingOrder = maxSortingOrder;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;
        canvasGroup.blocksRaycasts = true;
        
        var distance = Vector3.Distance(_rectTransform.anchoredPosition, _originPos);

        if (distance <= threshold)
        {
            _callback?.Invoke();
        }
        else
        {
            _rectTransform.anchoredPosition = _originPos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"Pointer up, is drag: {_isDrag}");
        if (_isDrag == false)
            _callback?.Invoke();
    }

    public void DisableOverrideSorting()
    {
        canvas.overrideSorting = false;
    }
}