using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

namespace Minigame.RedColor
{
    public class CupController : Draggable
    {
        [SerializeField] private float moveDuration = 2f;
        [SerializeField] private Transform playAnimTransform;
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private GameObject hand;
        [SerializeField] private SkeletonAnimation skeletonAnimation;

        [SpineAnimation(dataField: "skeletonAnimation")]
        public string noneToFullName = "";
        
        [SpineAnimation(dataField: "skeletonAnimation")]
        public string fullToHalfName = "";

        private Vector3 _originPosition;
        private Action _onCupToGlass;
        private Action _onFinish;

        private bool _canDrag;
        
        private void Start()
        {
            boxCollider2D.enabled = false;
            _originPosition = transform.position;
        }

        protected override void OnMouseDown()
        {
            mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            hand.SetActive(false);
        }

        protected override void OnMouseDrag()
        {
            if (_canDrag)
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos);
        }

        protected override void OnMouseUp()
        {
            if (_canDrag)
                transform.position = startPos;
        }

        public void SetCanDrag(Action onCupToGlass, Action onFinish)
        {
            boxCollider2D.enabled = true;
            hand.SetActive(true);
            _canDrag = true;
            _onCupToGlass = onCupToGlass;
            _onFinish = onFinish;
        }
        
        public void PlayAnimFull(Action onFinish)
        {
            var track = skeletonAnimation.AnimationState.SetAnimation(0, noneToFullName, false);
            track.Complete += entry => {
                OnFinish(onFinish).Forget();
            };
        }

        private async UniTask OnFinish(Action onFinish)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            onFinish?.Invoke();
        }
        
        private void PlayAnimFullToHalf()
        {
            boxCollider2D.enabled = false;
            _canDrag = false;
            transform.position = playAnimTransform.position;
            _onCupToGlass?.Invoke();
            
            var track = skeletonAnimation.AnimationState.SetAnimation(0, fullToHalfName, false);
            
            track.Complete += entry => {
                transform.DOMove(_originPosition, moveDuration).OnComplete(() => {
                    _onFinish?.Invoke();
                });
            };
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            PlayAnimFullToHalf();
        }
    }
}