using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Minigame.YellowColor
{
    public class TableFoodItem : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image foodImage;

        public YellowFood YellowFood => _yellowFood;
        
        private YellowFood _yellowFood;
        private Action<TableFoodItem> _onSelect;

        private RectTransform _rectTransform;
        private Vector3 _originPosition;
        
        private void Start()
        {
            button.onClick.AddListener(OnClicked);
            _rectTransform = GetComponent<RectTransform>();
            _originPosition = _rectTransform.anchoredPosition;
        }
        
        public void Initialize(YellowFood food, Action<TableFoodItem> onSelect, RectTransform parent)
        {
            _yellowFood = food;
            _onSelect = onSelect;
            _rectTransform.SetParent(parent);
            _rectTransform.localScale = Vector3.one;
            _rectTransform.anchoredPosition = _originPosition;
            LoadFoodImage();
        }
        
        private void LoadFoodImage()
        {
            foodImage.sprite = LoadSpriteService.LoadYellowFood(YellowFood);
        }
        
        public void OnSelectRight(RectTransform parent, float duration, Action onFinish)
        {
            _rectTransform.SetParent(parent);
            _rectTransform.DOAnchorPos(Vector2.zero, duration).OnComplete(() => {
                onFinish?.Invoke();
            });
        }

        public void DoScaleMinimum(float duration)
        {
            _rectTransform.DOScale(Vector3.zero, duration);
        }
        
        private void OnClicked()
        {
            _onSelect?.Invoke(this);
        }
    }
}