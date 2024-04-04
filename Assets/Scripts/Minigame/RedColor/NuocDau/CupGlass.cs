using System;
using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

namespace Minigame.RedColor
{
    public class CupGlass : MonoBehaviour
    {
        [SerializeField] private float delay = 0.5f;
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string noneToFullName = "";
        
        public async UniTask PlayAnim()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            var track = skeletonAnimation.AnimationState.SetAnimation(0, noneToFullName, false);
        }
    }
}