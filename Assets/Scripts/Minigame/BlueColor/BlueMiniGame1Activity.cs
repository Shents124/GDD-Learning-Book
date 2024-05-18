using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using DG.Tweening;
using Sound.Service;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;
using Utility;

namespace Minigame.BlueColor
{
    public class BlueMiniGame1Activity : BaseActivity
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform newContentPosition;
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private float moveRedClothsDuration = 0.2f;
        [SerializeField] private List<RedClothes> redClothes;
        [SerializeField] private SelectedClothes selectedClothes;
        [SerializeField] private GameObject vfxsDone;

        protected override void InitializeData(Memory<object> args)
        {
            ProductTracking.miniGameSession = 2;
            ProductTracking.miniGameStep = 2;
            selectedClothes.Initialize(OnFinishSelectBlueClothes);
            base.InitializeData(args);
        }

        public override void DidEnter(Memory<object> args)
        {
            AudioUtility.PlaySFX(AudioClipName.Blue_clothes);
            base.DidEnter(args);
        }

        private async void OnFinishSelectBlueClothes()
        {
            vfxsDone.SetActive(true);
            await AsyncService.Delay(1f, this);
            vfxsDone.SetActive(false);
            content.DOAnchorPos(newContentPosition.anchoredPosition, moveDuration).OnComplete(MoveRedClothes);
        }

        private void MoveRedClothes()
        {
            ProductTracking.miniGameStep = 3;
            selectedClothes.MoveRedClothes(redClothes, moveRedClothsDuration, OnFinish);
        }

        private void OnFinish()
        {
            foreach (var redClothe in redClothes)
            {
                redClothe.Show(true);
            }
            vfxsDone.SetActive(true);
            StartCoroutine(MoveToNextStep());
        }

        private IEnumerator MoveToNextStep()
        {
            yield return new WaitForSeconds(1f);
            UIService.OpenActivityWithFadeIn(ActivityType.MiniGameBlue2Screen);
        }
    }
}