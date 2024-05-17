using System;
using System.Collections;
using Constant;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Minigame.BlueColor;
using Sound.Service;
using Spine.Unity;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;

namespace Step345Screen
{
    public class Step345BlueScreen : Step345BaseScreen
    {
        [SerializeField] private float characterMoveDuration = 1.75f;
        [SerializeField] private float giftMoveDuration = 0.3f;
        [SerializeField] private float foodMoveDuration = 0.4f;
        [SerializeField] private RectTransform characterTransform;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private GameObject dialog;
        [SerializeField] private Step345Gift gift;
        [SerializeField] private BlueFood[] foods;

        [SerializeField] private Board board;
        [SerializeField] private GameObject dark, vfx, player;
        
        [SerializeField] private RectTransform characterEndPosition;
        [SerializeField] private RectTransform characterEnd2Position;
        [SerializeField] private RectTransform giftEndPosition;
        [SerializeField] private RectTransform showCardPosition;

        private int _fillCount;
        private bool _isFilled = false;

        [SerializeField] private ColorType _colorType;
        
        public override void DidEnter(Memory<object> args)
        {
            var giftTransform = gift.GetComponent<RectTransform>();
            characterController.PlayAnim(0, characterController.runAnimation, true);
            characterTransform.DOAnchorPos(characterEndPosition.anchoredPosition, characterMoveDuration).OnComplete(
                () => {
                    characterController.PlayAnim(0, characterController.idleTalkAnimation, true);
                    AudioUtility.PlaySFX(this, AudioClipName.Blue_intro, 0, () => {
                        characterController.PlayAnim(0, characterController.idleAnimation, true);

                        AudioUtility.PlaySFX(AudioClipName.Gift_fall);
                        giftTransform.DOAnchorPos(giftEndPosition.anchoredPosition, giftMoveDuration).SetEase(Ease.OutQuad)
                            .OnComplete(() => {
                                characterController.PlayAnim(0, characterController.exitingAnimation, false, () => {
                                    characterController.PlayAnim(0, characterController.idleAnimation, true);
                                });

                                gift.Initialize(OnClickOpenGift).Forget();
                            });
                    });
                });
            
            base.DidEnter(args);
        }

        private void OnClickOpenGift()
        {
            AudioUtility.PlaySFX(AudioClipName.Gift_open);
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

            ProductTracking.step = 2;
        }

        private void OnClickedFood(int index)
        {
            if (_isFilled)
                return;
            
            _isFilled = true;
            AudioUtility.PlaySFX(AudioClipName.Jump);
            StartCoroutine(foods[index].MoveFoods(Fill));
        }

        private void Fill()
        {
            _fillCount++;
            AudioUtility.PlaySFX(AudioClipName.Crayon_eat);
            characterController.PlayAnim(0, characterController.idleEatAnimation, false, () => {
                characterController.PlayAnim(0, characterController.idleAnimation, true);
                switch (_fillCount)
                {
                    case 1:
                        player.GetComponent<SkeletonGraphic>().DOFade(1, 0f);
                        characterController.DoMask(250f, () => { _isFilled = false; });
                        break;
                    case 2:
                        characterController.DoMask(400f, () => { _isFilled = false; });
                        break;
                    case 3:
                        characterController.DoMask(600f, OnStep5);
                        break;
                }
            });
        }

        private void OnStep5()
        {
            ProductTracking.step = 3;
            
            characterController.DisableMask();
            AudioUtility.PlaySFX(AudioClipName.Hooray_WF);
            characterController.PlayAnim(0, characterController.cheerAnimation, false, () => {
                AudioUtility.PlaySFX(AudioClipName.Hooray_WF);
                characterController.PlayAnim(0, characterController.cheerAnimation, false, () => {
                    characterController.PlayAnim(0, characterController.runAnimation, true);
                    characterTransform.DOAnchorPos(characterEnd2Position.anchoredPosition, characterMoveDuration * 2)
                        .OnComplete(ShowBoard);
                });
            });
        }

        private void ShowBoard()
        {
            AudioUtility.PlaySFX(AudioClipName.Falldown);
            characterController.FlipX();
            characterController.PlayAnim(0, characterController.idleTalkAnimation, false, () => {
                characterController.PlayAnim(0, characterController.idleAnimation, true);
            });

            dialog.SetActive(true);
            board.DoMove(() => {
                AudioUtility.PlaySFX(AudioClipName.Blue_quiz);
                board.Initialize(OnClickedCard);
            });
        }

        private void OnClickedCard(ColorType colorType, Card card)
        {
            if (_colorType != colorType)
            {
                AudioUtility.PlaySFX(AudioClipName.Fail);
                card.transform.DOShakePosition(0.5f, 15, 50, 90);
                characterController.PlayAnim(0, characterController.sadAnimation, false, () => {
                    characterController.PlayAnim(0, characterController.idleAnimation, true);
                });
                
                return;
            }
            
            AudioUtility.PlaySFX(AudioClipName.Correct);
            AudioUtility.PlaySFX(AudioClipName.Hooray_WF);
            card.ShowVfx();
            characterController.PlayAnim(0, characterController.cheerAnimation, false, () => {
                characterController.PlayAnim(0, characterController.idleAnimation, true);
                dark.SetActive(true);
                card.transform.SetParent(transform);
                card.DoShow(showCardPosition.anchoredPosition, 1f, () => {
                    AudioUtility.PlaySFX(AudioClipName.Clearstep);
                    StartCoroutine(MoveToNextStep());   
                    vfx.SetActive(true);
                });
            });
        }

        private static IEnumerator MoveToNextStep()
        {
            yield return new WaitForSeconds(1f);
            var trackingAdInter = new TrackingAdInter {
                hasData = true,
                levelName = LevelName.blue,
                adLocation = AdLocation.start, 
                miniGameSession = "1", 
                isWoaAd = false
            };
            UIService.OpenActivityWithFadeIn(ActivityType.MiniGameBlueScreen, trackingAdInter: trackingAdInter);
        }
    }
}