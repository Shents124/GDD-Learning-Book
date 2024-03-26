using System;
using ButtonUI;
using Constant;
using UnityEngine;

namespace Step345Screen
{
    public class Card : BaseButtonUI
    {
        [SerializeField] private ColorType colorType;

        private Action<ColorType> _onClick;

        private void Start()
        {
            AddListener(OnClicked);
        }

        public void Initialize(Action<ColorType> onClick)
        {
            _onClick = onClick;
        }

        private void OnClicked()
        {
            _onClick?.Invoke(colorType);
        }
    }
}