using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Minigame.BlueColor
{
    public class BlueMiniGameActivity : BaseActivity
    {
        [SerializeField] private float toyContainerShowDuration = 1f;
        [SerializeField] private float toyMoveDuration = 1f;
        [SerializeField] private float toyScale = 0.8f;
        
        [SerializeField] private ToyContainer blueToyContainer;
        [SerializeField] private ToyContainer redToyContainer;
        
        [SerializeField] private List<Toy> blueToys;
        [SerializeField] private List<Toy> redToys;

        private int _blueToyCount;
        private int _redToyCount;
        
        public override void DidEnter(Memory<object> args)
        {
            blueToyContainer.DoShow(toyContainerShowDuration, InitializeToy);
            base.DidEnter(args);
        }

        private void InitializeToy()
        {
            foreach (var blueToy in blueToys)
            {
                blueToy.Initialize(_blueToyCount, OnClickBlueToy);
                _blueToyCount++;
            }
            
            foreach (var redToy in redToys)
            {
                redToy.Initialize(_redToyCount, null);
                _redToyCount++;
            }
        }

        private void OnClickBlueToy(Toy blueToy)
        {
            var index = blueToy.Index;
            var parent = blueToyContainer.GetToyParent(index);
            blueToy.DoMove(parent, new Vector3(toyScale, toyScale, toyScale), toyMoveDuration, OnMoveToyFinish);
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

                redToy.DoMove(parent, new Vector3(toyScale, toyScale, toyScale), toyMoveDuration, null);
            }

            yield return new WaitForSeconds(toyMoveDuration);
            redToyContainer.Shake(() => {
                UIService.OpenActivityWithFadeIn(ActivityType.MiniGameBlue1Screen);
            });
        }
    }
}