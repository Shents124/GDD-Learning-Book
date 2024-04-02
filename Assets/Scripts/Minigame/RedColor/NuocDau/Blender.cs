using System;
using Spine.Unity;
using UnityEngine;

namespace Minigame.RedColor
{
    public class Blender : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField] private ItemClick itemClick;
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string fullName = "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string halfName = "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string noneName = "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string noneToFullName = "";

        private Action _onFill;
        
        private void Start()
        {
            itemClick.gameObject.SetActive(false);
        }

        public void Initialize(Action onFill)
        {   
            itemClick.gameObject.SetActive(true);
            skeletonAnimation.AnimationState.SetAnimation(0, noneName, false);
            itemClick.AddListener(PlayAnimFill);
            _onFill = onFill;
        }

        private void PlayAnimFill()
        {
            _onFill?.Invoke();
            var track = skeletonAnimation.AnimationState.SetAnimation(0, noneToFullName, false);
            track.Complete += entry => {

            };
        }
    }
}