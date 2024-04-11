using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace DefaultNamespace
{
    public class FadeScreenActivity : Activity
    {
        [SerializeField] private float animDuration = 0.5f;
        [SerializeField] private CanvasGroup canvasGroup;

        public override UniTask Initialize(Memory<object> args)
        {
            canvasGroup.alpha = 0f;
            SetBlockRaycast(false);
            return base.Initialize(args);
        }
        
        public void FadeIn(Action callback)
        {
            canvasGroup.alpha = 0f;
            SetBlockRaycast(true);
            canvasGroup.DOFade(1, animDuration).OnComplete(() => {
                SetBlockRaycast(false);
                callback?.Invoke();
            });
        }

        public void FadeOut(Action callback)
        {
            canvasGroup.alpha = 1f;
            SetBlockRaycast(true);
            canvasGroup.DOFade(0, animDuration).OnComplete(() => {
                SetBlockRaycast(false);
                callback?.Invoke();
            });
        }
        
        private void SetBlockRaycast(bool value)
        {
            canvasGroup.blocksRaycasts = value;
        }
    }
}