using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class YellowMiniGame3Activity : BaseActivity
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
            UIService.OpenActivityWithFadeIn(ActivityType.MiniGameYellow4Screen);
        }
    }
}