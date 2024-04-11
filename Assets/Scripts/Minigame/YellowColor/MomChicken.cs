using System;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class MomChicken : MonoBehaviour
    {
        [SerializeField] private float jumpDuration = 1f;
        
        [SerializeField] private SkeletonGraphic skeletonAnimation;

        [SpineEvent(dataField: "skeletonAnimation")]
        public string layEggEvent;
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string animLayEgg= "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string animNone = "";
        
        [SerializeField] private YellowEgg eggOne;
        [SerializeField] private YellowEgg eggTwo;

        [SerializeField] private RectTransform eggOneEndPosition;
        [SerializeField] private RectTransform eggTwoEndPosition;
        
        private int _layEggCount;
        
        public void LayEgg(Action onFinish)
        {
            var track = skeletonAnimation.AnimationState.SetAnimation(0, animLayEgg, false);
            track.Event += HandleEvent;
            track.Complete += entry => LayEggTwo(onFinish);
        }

        private void LayEggTwo(Action onFinish)
        {
            var track = skeletonAnimation.AnimationState.SetAnimation(0, animLayEgg, false);
            track.Event += HandleEvent;
            track.Complete += entry => onFinish?.Invoke();
        }

        private void HandleEvent(TrackEntry trackEntry, Spine.Event e)
        {
            // Play some sound if the event named "footstep" fired.
            if (e.Data.Name == layEggEvent)
            {
                MoveEgg();
            }
        }

        private void MoveEgg()
        {
            if (_layEggCount <= 0)
            {
                eggOne.DoMove(eggOneEndPosition, jumpDuration);
                _layEggCount++;
            }
            else
            {
                eggTwo.DoMove(eggTwoEndPosition, jumpDuration);
            }
        }
    }
}