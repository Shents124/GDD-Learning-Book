using System;
using Constant;
using DG.Tweening;
using UnityEngine;

namespace Step345Screen
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private float moveDuration = 1.5f;
        [SerializeField] private Card[] cards;

        [SerializeField] private RectTransform endPosition;
        
        public void Initialize(Action<ColorType, Card> onClick)
        {
            foreach (var card in cards)
            {
                card.Initialize(onClick);
            }
        }

        public void DoMove(Action onFinish)
        {
            GetComponent<RectTransform>().DOAnchorPos(endPosition.anchoredPosition, moveDuration)
                .OnComplete(() => onFinish?.Invoke());
        }
    }
}