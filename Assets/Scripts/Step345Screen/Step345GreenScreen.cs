using System;
using System.Collections;
using Constant;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Step345Screen
{
    public class Step345GreenScreen : BaseActivity
    {
        [SerializeField] private float characterMoveDuration = 1.75f;
        [SerializeField] private float giftMoveDuration = 0.3f;
        [SerializeField] private float foodMoveDuration = 0.4f;
        [SerializeField] private RectTransform characterTransform;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private GameObject dialog;
        [SerializeField] private Step345Gift gift;
        [SerializeField] private Button[] foods;

        [SerializeField] private Board board;

        [SerializeField] private RectTransform[] foodPositions;
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
            var giftTransform = gift.GetComponent<RectTransform>();
            characterController.PlayAnim(0, characterController.runAnimation, true);
            characterTransform.DOAnchorPos(characterEndPosition.anchoredPosition, characterMoveDuration).OnComplete(
                () => {
                    characterController.PlayAnim(0, characterController.idleAnimation, true);

                    giftTransform.DOAnchorPos(giftEndPosition.anchoredPosition, giftMoveDuration).SetEase(Ease.OutQuad)
                        .OnComplete(() => {
                            characterController.PlayAnim(0, characterController.exitingAnimation, false, () => {
                                characterController.PlayAnim(0, characterController.idleAnimation, true);
                            });

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
            characterController.PlayAnim(0, characterController.idleEatAnimation, false, () => {
                characterController.PlayAnim(0, characterController.idleAnimation, true);

                if (_fillCount == 3)
                {
                    OnStep5();
                }
            });
        }

        private void OnStep5()
        {
            characterController.ChangeSkin(characterController.fullSkin);

            characterController.PlayAnim(0, characterController.cheerAnimation, false, () => {
                characterController.PlayAnim(0, characterController.runAnimation, true);
                characterTransform.DOAnchorPos(characterEnd2Position.anchoredPosition, characterMoveDuration * 2)
                    .OnComplete(ShowBoard);
            });
        }

        private void ShowBoard()
        {
            characterController.PlayAnim(0, characterController.idleTalkAnimation, false, () => {
                characterController.PlayAnim(0, characterController.idleAnimation, true);
            });

            dialog.SetActive(true);
            board.DoMove(() => {
                board.Initialize(OnClickedCard);
            });
        }

        private void OnClickedCard(ColorType colorType, Card card)
        {
            if (_colorType != colorType)
                return;

            characterController.PlayAnim(0, characterController.cheerAnimation, false, () => {
                characterController.PlayAnim(0, characterController.idleAnimation, true);
                card.transform.SetParent(transform);
                card.DoShow(showCardPosition.anchoredPosition, 1f, () => {
                    StartCoroutine(MoveToNextStep());
                });
            });
        }

        private static IEnumerator MoveToNextStep()
        {
            yield return new WaitForSeconds(1f);
            UIService.OpenActivityWithFadeIn(ActivityType.MinigameRed);
        }
    }
}