﻿using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class TableFood : MonoBehaviour
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private float duration = 1f;
        [SerializeField] private RectTransform hidePosition;
        [SerializeField] private RectTransform showPosition;
        [SerializeField] private List<TableFoodItem> items;
        
        private Action<TableFoodItem> _onSelect;

        private void Start()
        {
            container.anchoredPosition = hidePosition.anchoredPosition;
        }

        public void Show(List<YellowFood> sets, Action<TableFoodItem> onSelect)
        {
            _onSelect = onSelect;
            
            for (int i = 0, n = sets.Count; i < n; i++)
            {
                items[i].Initialize(sets[i], OnSelectFood, container);
            }
            
            container.DOAnchorPos(showPosition.anchoredPosition, duration);
        }

        public void Hide()
        {
            container.DOAnchorPos(hidePosition.anchoredPosition, duration);
        }

        private void OnSelectFood(TableFoodItem tableFoodItem)
        {
            _onSelect?.Invoke(tableFoodItem);
        }
    }
}