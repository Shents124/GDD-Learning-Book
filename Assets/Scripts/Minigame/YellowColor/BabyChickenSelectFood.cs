using System;
using System.Collections;
using DG.Tweening;
using Sound.Service;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Minigame.YellowColor
{
    public class BabyChickenSelectFood : BabyChicken
    {
        [SerializeField] public YellowFood yellowFood;
        [SerializeField] private RectTransform eatPosition;
        [SerializeField] private CanvasGroup dialog;
        [SerializeField] private Image foodSprite;

        private void Start()
        {
            HideDialog();
        }

        public void RunToPosition(RectTransform position, float duration, Action onFinish = null)
        {
            var run = skeletonAnimation.AnimationState.SetAnimation(0, runAnim, true);
            GetComponent<RectTransform>().DOAnchorPos(position.anchoredPosition, duration).OnComplete(() => {
                onFinish?.Invoke();
                var idle = skeletonAnimation.AnimationState.SetAnimation(0, vayCanh, true);
            });
        }
        
        public void OnEat(TableFoodItem foodItem, Action onHide, Action onFinish)
        {
            dialog.gameObject.SetActive(false);
            foodItem.OnSelectRight(eatPosition, 0.5f, () => 
            {
                AudioUtility.PlaySFX(AudioClipName.Chicken_happy);
                onHide?.Invoke();
                var eatTrack = skeletonAnimation.AnimationState.SetAnimation(0, eatAnim, true);
                var duration = skeletonAnimation.SkeletonData.FindAnimation(eatAnim).Duration * 4;
                
                foodItem.DoScaleMinimum(duration / 2);

                StartCoroutine(JumpAndRun(duration, onFinish));
            } );
        }
        
        private IEnumerator JumpAndRun(float delay, Action onFinish)
        {
            yield return new WaitForSeconds(delay);
            var jumpTrack = skeletonAnimation.AnimationState.SetAnimation(0, jumpAnim, false);
            jumpTrack.Complete += entry => {
                PlayAnimRun(onFinish);
            };
        }

        public void ShowDialog()
        {
            foodSprite.sprite = LoadSpriteService.LoadYellowFood(yellowFood);
            dialog.gameObject.SetActive(true);
            dialog.alpha = 0f;
            dialog.DOFade(1f, 1f);
        }

        private void HideDialog() => dialog.gameObject.SetActive(false);
    }
}