using System;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class BabyChicken : MonoBehaviour
    {
        [SerializeField] protected SkeletonGraphic skeletonAnimation;
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string runAnim= "";

        [SpineAnimation(dataField: "skeletonAnimation")]
        public string eatAnim = "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string jumpAnim = "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string vayCanh = "";
        
        [SerializeField] private bool moveRight;
        [SerializeField] private float moveDuration = 3f;
        [SerializeField] private float moveDistance;

        public void PlayAnimRun(Action onFinish = null)
        {
            var track = skeletonAnimation.AnimationState.SetAnimation(0, runAnim, true);
            moveDistance = moveRight ? moveDistance : -moveDistance;
            GetComponent<RectTransform>().DOAnchorPosX(moveDistance, moveDuration).OnComplete(() => {
                onFinish?.Invoke();
            });
        }
    }
}