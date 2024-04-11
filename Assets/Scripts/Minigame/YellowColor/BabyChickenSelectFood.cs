using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Minigame.YellowColor
{
    public class BabyChickenSelectFood : BabyChicken
    {
        [SerializeField] public YellowFood yellowFood;
        [SerializeField] private RectTransform eatPosition;
        [SerializeField] private GameObject dialog;
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
            dialog.SetActive(false);
            foodItem.OnSelectRight(eatPosition, 0.5f, () => 
            {
                onHide?.Invoke();
                var eatTrack = skeletonAnimation.AnimationState.SetAnimation(0, eatAnim, false);
                var duration = skeletonAnimation.SkeletonData.FindAnimation(eatAnim).Duration;
                
                foodItem.DoScaleMinimum(duration);
                eatTrack.Complete += entry => JumpAndRun(onFinish);
                
            } );
        }
        
        private void JumpAndRun(Action onFinish)
        {
            var jumpTrack = skeletonAnimation.AnimationState.SetAnimation(0, jumpAnim, false);
            jumpTrack.Complete += entry => {
                PlayAnimRun(onFinish);
            };
        }

        public void ShowDialog()
        {
            foodSprite.sprite = LoadSpriteService.LoadYellowFood(yellowFood);
            dialog.SetActive(true);
        }

        private void HideDialog() => dialog.SetActive(false);
    }
}