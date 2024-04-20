using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ButtonUI
{
    [RequireComponent(typeof(Button))]
    public class BaseButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Vector3 MIN_SCALE = new(0.95f, 0.95f, 0.95f);
        [SerializeField] private Vector3 MAX_SCALE = new(1.05f,1.05f,1.05f);
        [SerializeField] private float duration = 0.15f;

        private float startScale = 1f;
            
        private Tween _tweenDown;
        private Tween _tweenUp;

        private Action _onClick;
        private Button _button;

        protected Button Button => _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            startScale = transform.localScale.x;
        }

        private void OnDisable()
        {
            _tweenDown?.Kill();
            _tweenUp?.Kill();
            transform.localScale = Vector3.one;
        }
        
        public void AddListener(Action onClick)
        {
            _onClick += onClick;
        }
        
        private void OnClick()
        {
            if (IsInteractable() == false)
                return;
            
            _onClick?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable() || eventData.button != PointerEventData.InputButton.Left)
                return;
            
            _tweenDown = transform.DOScale(MIN_SCALE, duration / 2 );
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable() || eventData.button != PointerEventData.InputButton.Left)
                return;
            
            _tweenUp = transform.DOScale(MAX_SCALE, duration / 2).OnComplete(() => {
                if (transform == null)
                    return;
                transform.localScale = Vector3.one * startScale;
            });
        }
        
        private bool IsInteractable() => _button.interactable;
    }
}