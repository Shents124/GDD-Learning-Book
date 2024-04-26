using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;
using Utility;

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
            await AsyncService.Delay(delay, this);
            var track = skeletonAnimation.AnimationState.SetAnimation(0, noneToFullName, false);
        }
    }
}