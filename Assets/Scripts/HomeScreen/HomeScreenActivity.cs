using System;
using ButtonUI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

namespace HomeScreen
{
    public class HomeScreenActivity : Activity
    {
        [SerializeField] private BaseButtonUI button;

        public override UniTask Initialize(Memory<object> args)
        {
            button.AddListener(OnClickedPlay);
            return base.Initialize(args);
        }

        private void OnClickedPlay()
        {
            Debug.Log("Play game");
        }
    }
}