using System;
using Constant;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Minigame.BlueColor;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Step345Screen
{
    public class Step345BlueScreen : BaseActivity
    {
        [SerializeField] private float characterMoveDuration = 1.75f;
        [SerializeField] private float giftMoveDuration = 0.3f;
        [SerializeField] private float foodMoveDuration = 0.4f;
        [SerializeField] private RectTransform characterTransform;
        [SerializeField] private GameObject dialog;
        [SerializeField] private Image fillImage;
        [SerializeField] private Step345Gift gift;
        [SerializeField] private BlueFood[] foods;

        [SerializeField] private Board board;
        
        [SerializeField] private RectTransform characterEndPosition;
        [SerializeField] private RectTransform characterEnd2Position;
        [SerializeField] private RectTransform giftEndPosition;
        [SerializeField] private RectTransform showCardPosition;

        private int _fillCount;

        private ColorType _colorType;
        
        protected override void InitializeData(Memory<object> args)
        {
            if (args.IsEmpty == false)
                _colorType = (ColorType)args.ToArray()[0];
            
            base.InitializeData(args);
        }

        public override void DidEnter(Memory<object> args)
        {
            fillImage.fillAmount = 0;
            
            var giftTransform = gift.GetComponent<RectTransform>();
            characterTransform.DOAnchorPos(characterEndPosition.anchoredPosition, characterMoveDuration).OnComplete(() => {
                giftTransform.DOAnchorPos(giftEndPosition.anchoredPosition, giftMoveDuration).SetEase(Ease.OutQuad).
                    OnComplete(() => {
                        gift.Initialize(OnClickOpenGift).Forget();
                    });
            });
            base.DidEnter(args);
        }

        private void OnClickOpenGift()
        {
            gift.gameObject.SetActive(false);
            ShowFoods();
        }

        private void ShowFoods()
        {
            for (int i = 0, n = foods.Length; i < n; i++)
            {
                var food = foods[i];
                food.gameObject.SetActive(true);
                food.Initialize(characterEndPosition, foodMoveDuration, i, OnClickedFood);
            }
        }

        private void OnClickedFood(int index)
        {
            StartCoroutine(foods[index].MoveFoods(Fill));
        }

        private void Fill()
        {
            _fillCount++;
            fillImage.fillAmount = _fillCount / 3f;

            if (_fillCount == 3)
            {
                OnStep5();
            }
        }

        private void OnStep5()
        {
            characterTransform.DOAnchorPos(characterEnd2Position.anchoredPosition, characterMoveDuration * 2).
                OnComplete(ShowBoard);
        }

        private void ShowBoard()
        {
            dialog.SetActive(true);
            board.DoMove(() => {
                board.Initialize(OnClickedCard);
            });
        }

        private void OnClickedCard(ColorType colorType, Card card)
        {
            if (_colorType != colorType)
                return;
            
            card.transform.SetParent(transform);
            card.DoShow(showCardPosition.anchoredPosition, 1f, () => {
                UIService.PlayFadeIn(MoveToNextStep);
            });
        }

        private void MoveToNextStep()
        {
            UIService.OpenActivityWithFadeIn(ActivityType.MiniGameBlue1Screen);
        }
    }
}