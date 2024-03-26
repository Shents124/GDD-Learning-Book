using System;
using Constant;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace Step345Screen
{
    public class Step345RedScreen : Activity
    {
        [SerializeField] private float characterMoveDuration = 1.75f;
        [SerializeField] private float giftMoveDuration = 0.3f;
        [SerializeField] private float foodMoveDuration = 0.4f;
        [SerializeField] private RectTransform characterTransform;
        [SerializeField] private Image fillImage;
        [SerializeField] private Step345Gift gift;
        [SerializeField] private Button[] foods;

        [SerializeField] private Board board;
        
        [SerializeField] private RectTransform[] foodPositions; 
        [SerializeField] private RectTransform characterEndPosition;
        [SerializeField] private RectTransform characterEnd2Position;
        [SerializeField] private RectTransform giftEndPosition;

        private int _fillCount;

        private ColorType _colorType;
        
        public override UniTask Initialize(Memory<object> args)
        {
            if (args.IsEmpty == false)
                _colorType = (ColorType)args.ToArray()[0];
            
            return base.Initialize(args);
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
                var rectTransform = food.GetComponent<RectTransform>();
                
                rectTransform.anchoredPosition = giftEndPosition.anchoredPosition;
                rectTransform.localScale = Vector3.zero;
                rectTransform.gameObject.SetActive(true);
                rectTransform.DOJump(foodPositions[i].transform.position, 0.25f, 1, foodMoveDuration);
                rectTransform.DOScale(Vector3.one, foodMoveDuration);
                
                food.onClick.AddListener(() => OnClickedFood(food));
            }
        }

        private void OnClickedFood(Button button)
        {
           
            var rectTransform = button.GetComponent<RectTransform>();
            rectTransform.DOJump(characterEndPosition.transform.position, 400f, 1, foodMoveDuration);
            rectTransform.DOScale(Vector3.zero, foodMoveDuration * 2).OnComplete(Fill);
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
            board.DoMove(() => {
                board.Initialize(OnClickedCard);
            });
        }

        private void OnClickedCard(ColorType colorType)
        {
            if (_colorType != colorType)
                return;
            
            Debug.Log("Chọn đúng màu rồi");
        }
    }
}