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
        [SerializeField] private DOTweenAnimation anim1;
        [SerializeField] private DOTweenAnimation anim2;

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
            OnInBasketEgg();
        }
        
        private void OnClick()
        {
            _rectTransform.SetParent(_parent);
            _rectTransform.DOJump(_parent.position, 400f, 1, _moveDuration).OnComplete(OnInBasketEgg);
        }

        private void OnInBasketEgg()
        {
            _callback?.Invoke();
            SetOnClick(false);
            vfx.SetActive(true);
            _dragObject.DisableOverrideSorting();
            anim1.DOPlay();
            //anim2.DOPlay();
        }
        
        public void BirthChicken()
        {
            trungObj.SetActive(false);
            shadow.SetActive(false);
            babyChicken.gameObject.SetActive(true);
            babyChicken.PlayAnimRun();
            anim1.DOKill();
            //anim2.DOKill();
        }
    }
}