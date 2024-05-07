using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sound.Service;
using UnityEngine;
using Utility;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep1 : BaseStep
    {
        [SerializeField] private int numberStrawberry = 7;
        [SerializeField] private List<Strawberry> strawberries;
        [SerializeField] private GameObject _effect;

        private void Start()
        {
            SetData();
            _effect.SetActive(false);
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
            AudioUtility.PlaySFX(AudioClipName.Remove);
            numberStrawberry--;
            if (numberStrawberry <= 0)
            {
                _effect.SetActive(true);
                await AsyncService.Delay(1f, this);
                CompletedStep();
            }
        }
    }
}