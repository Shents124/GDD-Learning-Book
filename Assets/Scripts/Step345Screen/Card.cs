using System;
using ButtonUI;
using Constant;
using DG.Tweening;
using UnityEngine;

namespace Step345Screen
{
    public class Card : BaseButtonUI
    {
        [SerializeField] private ColorType colorType;

        private Action<ColorType, Card> _onClick;

        private void Start()
        {
            AddListener(OnClicked);
        }

        public void Initialize(Action<ColorType, Card> onClick)
        {
            _onClick = onClick;
        }

        private void OnClicked()
        {
            _onClick?.Invoke(colorType, this);
        }

        public void DoShow(Vector2 position, float duration)
        {
            Button.interactable = false;
            var rect = GetComponent<RectTransform>();
            rect.DOAnchorPos(position, duration);
            rect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), duration);
        }
    }
}