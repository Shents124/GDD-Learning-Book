using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sound.Service;
using UnityEngine;
using Utility;

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
        [SerializeField] private Transform showBlenderPosition;
        [SerializeField] private Vector3 newScale = new(1.2f, 1.2f, 1.2f);
        [SerializeField] private float zoomDuration = 1.5f;
        
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
            await AsyncService.Delay(delayDropStrawberry / 4, this);
            AudioUtility.PlaySFX(AudioClipName.Strawberry_pouring);
            await AsyncService.Delay(delayDropStrawberry * (3f / 4), this);
            ZoomIn();
        }

        private void ZoomIn()
        {
            plateStrawberryObj.SetPlate(false);
            transform.DOMove(showBlenderPosition.position, zoomDuration).SetEase(Ease.Linear);
            transform.DOScale(newScale, zoomDuration).SetEase(Ease.Linear).OnComplete(() => {
                blender.Initialize(OnBenderFill, OnBlenderRun);
            });
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
            await AsyncService.Delay(1f, this);
            CompletedStep();
        }
    }
}