using System;
using DG.Tweening;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace Step345Screen
{
    public class Step345RedScreen : Activity
    {
        [SerializeField] private RectTransform characterTransform;
        [SerializeField] private Step345Gift gift;
        
        [SerializeField] private RectTransform characterEndPosition;
        [SerializeField] private RectTransform giftEndPosition;
        
        public override void DidEnter(Memory<object> args)
        {
            var giftTransform = gift.GetComponent<RectTransform>();
            characterTransform.DOAnchorPos(characterEndPosition.anchoredPosition, 2f).OnComplete(() => {
                giftTransform.DOAnchorPos(giftEndPosition.anchoredPosition, 0.3f).SetEase(Ease.OutQuad);
            });
            base.DidEnter(args);
        }
    }
}