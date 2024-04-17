using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Minigame.BlueColor
{
    public class BlueMiniGame2Activity : BaseActivity
    {
        [SerializeField] private float duration = 2f;
        
        public override void DidEnter(Memory<object> args)
        {
            StartCoroutine(NextMiniGame());
            base.DidEnter(args);
        }

        private IEnumerator NextMiniGame()
        {
            yield return new WaitForSeconds(duration);
            UIService.OpenActivityWithFadeIn(ActivityType.MiniGameBlue3Screen);
        }
    }
}