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
        Action _callbackFadeIn;
        Action _callbackFadeOut;

        public override UniTask Initialize(Memory<object> args)
        {
            canvasGroup.alpha = 0f;
            SetBlockRaycast(false);
            return base.Initialize(args);
        }
        
        public void FadeIn(Action callback, float timeDuration = 0.5f, float alphaDone = 0)
        {
            canvasGroup.alpha = 0;
            _callbackFadeIn = callback;
            SetBlockRaycast(true);
            canvasGroup.DOFade(alphaDone, timeDuration).OnComplete(() => {
                SetBlockRaycast(false);
                _callbackFadeIn?.Invoke();
                _callbackFadeIn = null;
            });
        }

        public void FadeOut(Action callback, float timeDuration = 0.5f)
        {
            canvasGroup.alpha = 1f;
            _callbackFadeOut = callback;
            SetBlockRaycast(true);
            canvasGroup.DOFade(0, timeDuration).OnComplete(() => {
                SetBlockRaycast(false);
                _callbackFadeOut?.Invoke();
                _callbackFadeOut = null;
            });
        }
        
        private void SetBlockRaycast(bool value)
        {
            canvasGroup.blocksRaycasts = value;
        }
    }
}