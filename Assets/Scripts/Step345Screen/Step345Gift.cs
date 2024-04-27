using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Step345Screen
{
    public class Step345Gift : MonoBehaviour
    {
        [SerializeField] private float delay = 1f;
        [SerializeField] private float balloonFlyDuration = 0.5f;
        [SerializeField] private float balloonFadeDuration = 0.5f;
        [SerializeField] private RectTransform balloonState2EndPos;
        [SerializeField] private GameObject balloonState1;
        [SerializeField] private GameObject balloonState2;
        [SerializeField] private RectTransform giftRectTransform;

        [SerializeField] private Button button;

        private Action _onClick;

        private void Start()
        {
            button.onClick.AddListener(OnClicked);
        }
        
        public async UniTask Initialize(Action onClick)
        {
            _onClick = onClick;
            giftRectTransform.DOShakeScale(0.3f, 0.2f, 1, 0);
            await AsyncService.Delay(delay, this);
            balloonState1.SetActive(false);
            balloonState2.SetActive(true);
            balloonState2.GetComponent<RectTransform>().DOAnchorPos(balloonState2EndPos.anchoredPosition, balloonFlyDuration)
                .OnComplete(() => {
                    balloonState2.GetComponent<CanvasGroup>().DOFade(0f, balloonFadeDuration);
                });
        }
        
        private void OnClicked()
        {
            _onClick?.Invoke();
        }
    }
}