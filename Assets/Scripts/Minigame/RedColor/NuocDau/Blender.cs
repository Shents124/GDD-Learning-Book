using System;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using UnityEngine;
using Utility;

namespace Minigame.RedColor
{
    public class Blender : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField] private ItemClick itemClick;

        [SpineAnimation(dataField: "skeletonAnimation")]
        public string fullToHalfName = "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string noneName = "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string noneToFullName = "";

        private Action _onFill;
        private Action _onRun;
        
        private void Start()
        {
            itemClick.gameObject.SetActive(false);
        }

        public void Initialize(Action onFill, Action onRun)
        {   
            itemClick.gameObject.SetActive(true);
            skeletonAnimation.AnimationState.SetAnimation(0, noneName, false);
            itemClick.AddListener(PlayAnimFill);
            _onFill = onFill;
            _onRun = onRun;
        }

        private void PlayAnimFill()
        {
            AudioUtility.PlaySFX(AudioClipName.Blender);
            _onFill?.Invoke();
            itemClick.gameObject.SetActive(false);
            var track = skeletonAnimation.AnimationState.SetAnimation(0, noneToFullName, false);
            track.TimeScale = 0.75f;
            track.Complete += async entry => {
                AudioUtility.StopSFX();
                await AsyncService.Delay(0.75f, this);
                PlayAnimRun();
            };
        }

        private void PlayAnimRun()
        {
            _onRun?.Invoke();
            AudioUtility.PlaySFX(AudioClipName.Pour_water);
            var track = skeletonAnimation.AnimationState.SetAnimation(0, fullToHalfName, false);
            track.TimeScale = 0.75f;
            track.Complete += entry => {
                AudioUtility.StopSFX();
            };
        }
    }
}