using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep2 : BaseStep
    {
        [SerializeField] private float moveDuration = 2f;
        [SerializeField] private Transform outSidePosition;
        [SerializeField] private Transform cupGlassPosition;
        [SerializeField] private float delayDropStrawberry = 3f;
        [SerializeField] private PlateStrawberryObject plateStrawberryObj;
        [SerializeField] private PlateStrawberry plateStrawberry;
        [SerializeField] private Blender blender;
        [SerializeField] private CupController cupController;
        [SerializeField] private CupGlass cupGlass;
        
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
            blender.Initialize(OnBenderFill, OnBlenderRun);
        }

        private void OnBenderFill()
        {
            plateStrawberryObj.SetStrawberries(false);
        }

        private void OnBlenderRun()
        {
            cupController.PlayAnimFull(OnBlenderFinish);
        }

        private void OnBlenderFinish()
        {
            blender.transform.DOMove(outSidePosition.position, moveDuration).OnComplete(() => {
                cupGlass.transform.DOMove(cupGlassPosition.position, moveDuration).OnComplete(() => {
                    cupController.SetCanDrag(OnCupRunToGlass, OnFinishAllStep);
                });
            });
        }

        private void OnCupRunToGlass()
        {
            cupGlass.PlayAnim().Forget();
        }

        private void OnFinishAllStep()
        {
            OnCompletedStep().Forget();
        }
        
        private async UniTask OnCompletedStep()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            CompletedStep();
        }
    }
}