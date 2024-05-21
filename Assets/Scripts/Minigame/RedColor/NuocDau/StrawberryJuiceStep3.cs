using System.Collections;
using Sound.Service;
using Spine.Unity;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep3 : BaseStep
    {
        public SkeletonAnimation animLy;

        public GameObject ongHut, dau;

        [SpineAnimation]
        public string animNone, animFill, animDone;

        public ParticleSystem particleSystem;

        private void Start()
        {
            AudioUtility.PlaySFX(AudioClipName.Pour_water);
            var track = animLy.AnimationState.SetAnimation(0, animFill, false);
            track.Complete += entry => {
                AudioUtility.StopSFX();
                AudioUtility.PlaySFX(AudioClipName.Clearstep);
                particleSystem.Play();
                ongHut.SetActive(true);
                dau.SetActive(true);
                StartCoroutine(CompletedScene());
            };
        }

        private IEnumerator CompletedScene()
        {
            yield return new WaitForSeconds(2.5f);
            CompletedStep();
        }
    }
}