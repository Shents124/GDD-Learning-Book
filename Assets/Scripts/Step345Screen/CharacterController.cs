using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Step345Screen
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic skeletonAnimation;
        [SerializeField] private SkeletonGraphic maskAnim;
        [SerializeField] private RectMask2D rectMask2D;

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
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string sadAnimation= "";
        
        [SpineSkin(dataField: "skeletonAnimation")]
        public string noneSkin= "";
        
        
        private void Start()
        {
            skeletonAnimation.initialSkinName = noneSkin;
        }

        public void PlayAnim(int trackIndex, string animName, bool loop, Action onFinish = null, bool applyToMask = true)
        {
            var track = skeletonAnimation.AnimationState.SetAnimation(trackIndex, animName, loop);
            track.Complete += entry => onFinish?.Invoke();

            if (applyToMask)
            {
                maskAnim.AnimationState.SetAnimation(trackIndex, animName, loop);
            }
        }

        public void DoMask(float value, Action onFinish)
        {
            StartCoroutine(DoMaskCoroutine(value, onFinish));
        }

        private IEnumerator DoMaskCoroutine(float value, Action onFinish, float duration = 0.75f)
        {
            var currentValue = rectMask2D.padding.y;
          
            var elapsedTime = 0f;
            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                currentValue = Mathf.Lerp(currentValue, value, elapsedTime / duration);
                rectMask2D.padding = new Vector4(0, currentValue, 0, 0);
                yield return null;
            }
            onFinish?.Invoke();
        }

        public void DisableMask()
        {
            rectMask2D.gameObject.SetActive(false);
        }
        
        public void ChangeSkin(string skinName)
        {
            skeletonAnimation.Skeleton.SetSkin(skinName);
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.LateUpdate();
        }

        public void FlipX()
        {
            skeletonAnimation.Skeleton.ScaleX = 1f;
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.Skeleton.SetBonesToSetupPose();
        }
        
        public void FlipX(float value)
        {
            skeletonAnimation.Skeleton.ScaleX = value;
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.Skeleton.SetBonesToSetupPose();
        }
    }
}