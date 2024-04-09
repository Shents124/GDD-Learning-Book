using DG.Tweening;
using Spine.Unity;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class BabyChicken : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic skeletonAnimation;
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string runAnim= "";

        [SerializeField] private bool moveRight;
        [SerializeField] private float moveDuration = 3f;
        [SerializeField] private float moveDistance;

        public void PlayAnimRun()
        {
            var track = skeletonAnimation.AnimationState.SetAnimation(0, runAnim, true);
            moveDistance = moveRight ? moveDistance : -moveDistance;
            GetComponent<RectTransform>().DOAnchorPosX(moveDistance, moveDuration);
        }
    }
}