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

        public override UniTask Initialize(Memory<object> args)
        {
            UIService.PlayFadeOut();
            return base.Initialize(args);
        }

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