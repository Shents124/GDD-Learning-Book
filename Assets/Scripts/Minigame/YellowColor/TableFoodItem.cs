using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.YellowColor
{
    public class TableFoodItem : MonoBehaviour
    {
        [SerializeField] private Button button;

        public YellowFood YellowFood => _yellowFood;
        
        private YellowFood _yellowFood;
        private Action<TableFoodItem> _onSelect;

        private RectTransform _rectTransform;
        
        private void Start()
        {
            button.onClick.AddListener(OnClicked);
        }

        public void Initialize(YellowFood food, Action<TableFoodItem> onSelect, RectTransform parent)
        {
            _yellowFood = food;
            _onSelect = onSelect;
            _rectTransform.SetParent(parent);
            _rectTransform.localScale = Vector3.one;
        }

        public void OnSelectRight(RectTransform parent, float duration, Action onFinish)
        {
            _rectTransform.SetParent(parent);
            _rectTransform.DOAnchorPos(Vector2.zero, duration).OnComplete(() => {
                onFinish?.Invoke();
            });
        }
        
        private void OnClicked()
        {
            _onSelect?.Invoke(this);
        }
    }
}