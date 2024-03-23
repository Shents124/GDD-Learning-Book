using System;
using ButtonUI;
using Constant;
using TMPro;
using UnityEngine;
using Utility;

namespace MenuScreen
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] private ColorType colorType;
        [SerializeField] private BaseButtonUI baseButtonUI;
        [SerializeField] private TextMeshProUGUI colorName;

        private Action<ColorType> _onClick;

        private void Start()
        {
            baseButtonUI.AddListener(OnClicked);
        }

        public void Initialize(Action<ColorType> onClick)
        {
            _onClick = onClick;
            colorName.text = ColorHelper.GetColorName(colorType);
            colorName.color = ColorHelper.GetColor(colorType);
        }

        private void OnClicked()
        {
            _onClick?.Invoke(colorType);
        }
    }
}