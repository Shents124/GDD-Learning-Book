using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Minigame.BlueColor
{
    public class Clothes : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler,
        IInitializePotentialDragHandler
    {
        [SerializeField] private BlueClothesType blueClothesType;
        [SerializeField] private CanvasGroup canvasGroup;

        public BlueClothesType BlueClothesType => blueClothesType;
        
        private RectTransform _rectTransform;

        private bool _canDrag;
        private Action _callback;
        private Vector3 _originPos;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originPos = _rectTransform.anchoredPosition;
            _canDrag = true;
        }

        public void Initialize(Action callback)
        {
            _callback = callback;
        }
        
        public void SetCanDrag(bool value)
        {
            _canDrag = value;
            if (_canDrag == false)
                _callback?.Invoke();
        }

        public void SetPosition(Vector3 position)
        {
            GetComponent<RectTransform>().anchoredPosition = position;
        }
        
        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            eventData.useDragThreshold = false;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_canDrag)
            {
                return;
            }

            EventManager.CallBeginDragBlueClothes(blueClothesType);
            canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EventManager.CallEndDragBlueClothes();
            canvasGroup.blocksRaycasts = true;
            _rectTransform.anchoredPosition = _originPos;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_canDrag)
                _rectTransform.transform.position = eventData.position;
        }
    }
}