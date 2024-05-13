using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Minigame.YellowColor;
using Sound.Service;
using Tracking;
using Tracking.Constant;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Minigame.BlueColor
{
    public class BlueMiniGameActivity : BaseActivity
    {
        [SerializeField] private float toyContainerShowDuration = 1f;
        [SerializeField] private float toyMoveSpeed = 150f;
        [SerializeField] private float toyScale = 0.8f;
        
        [SerializeField] private ToyContainer blueToyContainer;
        [SerializeField] private ToyContainer redToyContainer;
        
        [SerializeField] private List<Toy> blueToys;
        [SerializeField] private List<Toy> redToys;

        public DropObject blueDropObject;

        private int _blueToyCount;
        private int _redToyCount;
        
        public override void DidEnter(Memory<object> args)
        {
            blueDropObject.Initialize(OnDrop);
            blueToyContainer.DoShow(toyContainerShowDuration, InitializeToy);
            base.DidEnter(args);
        }

        private void OnDrop(PointerEventData eventData)
        {
            var toy = eventData.pointerDrag.GetComponent<Toy>();
            toy.OnDrop();
        }

        private void InitializeToy()
        {
            AudioUtility.PlaySFX(AudioClipName.Blue_toy);
            foreach (var blueToy in blueToys)
            {
                blueToy.Initialize(_blueToyCount, OnClickBlueToy);
                _blueToyCount++;
            }
            
            foreach (var redToy in redToys)
            {
                redToy.Initialize(_redToyCount, OnClickRedToy);
                _redToyCount++;
            }
        }

        private void OnClickBlueToy(Toy blueToy)
        {
            AudioUtility.PlaySFX(AudioClipName.Correct);
            var index = blueToy.Index;
            var parent = blueToyContainer.GetToyParent(index);
            blueToy.GetComponent<Canvas>().overrideSorting = false;
            float toyMoveDuration = Vector2.Distance(parent.transform.position, blueToy.transform.position) / toyMoveSpeed;
            blueToy.DoMove(parent, new Vector3(toyScale, toyScale, toyScale), 0.5f, OnMoveToyFinish);
        }

        private void OnClickRedToy(Toy redToy)
        {
            AudioUtility.PlaySFX(AudioClipName.Fail);
            redToy.transform.DOShakePosition(0.5f, 15, 50, 90);
        }

        private void OnMoveToyFinish()
        {
            blueToyContainer.Shake(() => {
                
                _blueToyCount--;

                if (_blueToyCount <= 0)
                {
                    blueToyContainer.DoHide(toyContainerShowDuration, () => {
                        redToyContainer.DoShow(toyContainerShowDuration, () => {
                            StartCoroutine(MoveRedToys());
                        });
                    });
                }
            });
        }

        private IEnumerator MoveRedToys()
        {
            foreach (var redToy in redToys)
            {
                var index = redToy.Index;
                var parent = redToyContainer.GetToyParent(index);
                redToy.GetComponent<Canvas>().overrideSorting = false;
                redToy.DoMove(parent, new Vector3(toyScale, toyScale, toyScale), 1, null);
            }
            AudioUtility.PlaySFX(AudioClipName.Correct);
            yield return new WaitForSeconds(1);
            redToyContainer.Shake(() => {
                
                SetDataTrackingAd();
                UIService.OpenActivityWithFadeIn(ActivityType.MiniGameBlue1Screen, trackingAdInter: trackingAdInter);
            });
        }
        
        protected override void SetDataTrackingAd()
        {
            trackingAdInter = new TrackingAdInter {
                hasData = true,
                levelName = LevelName.blue,
                adLocation = AdLocation.start, 
                miniGameSession = "2", 
                isWoaAd = false
            };
        }
    }
}