using System;
using System.Collections;
using Cysharp.Threading.Tasks;
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
            UIService.OpenActivityAsync(ActivityType.MiniGameYellow4Screen).Forget();
        }
    }
}