using System;
using Spine.Unity;
using UnityEngine;

namespace Step345Screen
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic skeletonAnimation;

        [SpineAnimation(dataField: "skeletonAnimation")]
        public string runAnimation;
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string idleEatAnimation;
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string idleTalkAnimation= "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string idleAnimation= "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string exitingAnimation= "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string cheerAnimation= "";
        
        [SpineSkin(dataField: "skeletonAnimation")]
        public string noneSkin= "";
        
        [SpineSkin(dataField: "skeletonAnimation")]
        public string fullSkin= "";
        
        private void Start()
        {
            skeletonAnimation.initialSkinName = noneSkin;
        }

        public void PlayAnim(int trackIndex, string animName, bool loop, Action onFinish = null)
        {
            var track = skeletonAnimation.AnimationState.SetAnimation(trackIndex, animName, loop);
            track.Complete += entry => onFinish?.Invoke();
        }

        public void ChangeSkin(string skinName)
        {
            skeletonAnimation.Skeleton.SetSkin(skinName);
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.LateUpdate();
        }
    }
}