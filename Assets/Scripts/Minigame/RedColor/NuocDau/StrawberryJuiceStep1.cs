using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep1 : BaseStep
    {
        [SerializeField] private int numberStrawberry = 7;
        [SerializeField] private List<Strawberry> strawberries;

        private void Start()
        {
            SetData();
        }

        private void SetData()
        {
            foreach (var strawberry in strawberries)
            {
                strawberry.SetData(() => OnCleanStrawberry().Forget());
            }
        }
        
        private async UniTask OnCleanStrawberry()
        {
            numberStrawberry--;
            if (numberStrawberry <= 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                CompletedStep();
            }
        }
    }
}