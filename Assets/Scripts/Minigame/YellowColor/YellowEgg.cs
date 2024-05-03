using System;
using DG.Tweening;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class YellowEgg : MonoBehaviour
    {
        [SerializeField] private GameObject trungObj;
        [SerializeField] private GameObject shadow;
        [SerializeField] private BabyChicken babyChicken;
        [SerializeField] private GameObject vfx;

        private DragObject _dragObject;
        private RectTransform _rectTransform;
        private RectTransform _parent;
        private float _moveDuration;
        private Action _callback;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            babyChicken.gameObject.SetActive(false);
            _dragObject = GetComponent<DragObject>();
            _dragObject.enabled = false;
        }

        public void Initialize(RectTransform parent, float moveDuration, Action callback)
        {
            _parent = parent;
            _moveDuration = moveDuration;
            _dragObject.Initialize(OnClick, _rectTransform);
            _dragObject.enabled = true;
            _callback = callback;
        }
        
        private void SetOnClick(bool value)
        {
            _dragObject.enabled = value;
        }
        
        public void DoMove(RectTransform endPosition, float duration, Action callback = null)
        {
            shadow.SetActive(false);
            _rectTransform.DOJump(endPosition.position, 400f, 1, duration).OnComplete(() => {
                callback?.Invoke();
                shadow.SetActive(true);
                SetOnClick(true);
            });
        }

        public void Move()
        {
            shadow.SetActive(false);
            _rectTransform.SetParent(_parent);
            _rectTransform.anchoredPosition = Vector2.one;
            SetOnClick(false);
            _dragObject.DisableOverrideSorting();
            _callback?.Invoke();
            vfx.SetActive(true);
        }
        
        private void OnClick()
        {
            _rectTransform.SetParent(_parent);
            _rectTransform.DOJump(_parent.position, 400f, 1, _moveDuration).OnComplete(() => {
                _callback?.Invoke();
                SetOnClick(false);
                vfx.SetActive(true);
                _dragObject.DisableOverrideSorting();
            });
        }

        public void BirthChicken()
        {
            trungObj.SetActive(false);
            shadow.SetActive(false);
            babyChicken.gameObject.SetActive(true);
            babyChicken.PlayAnimRun();
        }
    }
}