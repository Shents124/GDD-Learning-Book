using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Minigame.BlueColor
{
    public class ToyContainer : MonoBehaviour
    {
        [SerializeField] private float shakeDuration = 0.5f;
        [SerializeField] private RectTransform showRect;
        [SerializeField] private List<RectTransform> toyParent;

        private Vector3 _hidePosition;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _hidePosition = _rectTransform.anchoredPosition;
        }
        
        public void DoShow(float duration, Action onFinish)
        {
            _rectTransform.DOAnchorPos(showRect.anchoredPosition, duration).OnComplete(() => {
                onFinish?.Invoke();
            });
        }

        public void DoHide(float duration, Action onFinish)
        {
            _rectTransform.DOAnchorPos(_hidePosition, duration).OnComplete(() => {
                onFinish?.Invoke();
            });
        }
        
        public void Shake(Action onFinish)
        {
            _rectTransform.DOShakeScale(shakeDuration, 0.1f, 1, 0).OnComplete(() => {
                onFinish?.Invoke();
            });
        }

        public RectTransform GetToyParent(int index)
        {
            return toyParent[index];
        }
    }
}