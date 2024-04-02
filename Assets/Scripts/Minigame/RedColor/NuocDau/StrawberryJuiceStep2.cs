using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep2 : BaseStep
    {
        [SerializeField] private float delayDropStrawberry = 3f;
        [SerializeField] private PlateStrawberryObject plateStrawberryObj;
        [SerializeField] private PlateStrawberry plateStrawberry;
        [SerializeField] private Blender blender;
        
        private void Start()
        {
            plateStrawberry.AddListener(OnClick);
        }

        private void OnClick()
        {
            plateStrawberryObj.Active();
            RemoveStrawberries().Forget();
        }

        private async UniTask RemoveStrawberries()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayDropStrawberry));
            plateStrawberryObj.SetPlate(false);
            blender.Initialize(() => {
                plateStrawberryObj.SetStrawberries(false);
            });
        }
    }
}