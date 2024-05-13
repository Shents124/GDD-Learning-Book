using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sound.Service;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;

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

        protected override void InitializeData(Memory<object> args)
        {
            selectedClothes.Initialize(OnFinishSelectBlueClothes);
            base.InitializeData(args);
        }

        public override void DidEnter(Memory<object> args)
        {
            AudioUtility.PlaySFX(AudioClipName.Blue_clothes);
            base.DidEnter(args);
        }

        private void OnFinishSelectBlueClothes()
        {
            content.DOAnchorPos(newContentPosition.anchoredPosition, moveDuration).OnComplete(MoveRedClothes);
        }

        private void MoveRedClothes()
        {
            selectedClothes.MoveRedClothes(redClothes, moveRedClothsDuration, OnFinish);
        }

        private void OnFinish()
        {
            foreach (var redClothe in redClothes)
            {
                redClothe.Show(true);
            }
            StartCoroutine(MoveToNextStep());
        }

        private IEnumerator MoveToNextStep()
        {
            yield return new WaitForSeconds(1f);
            UIService.OpenActivityWithFadeIn(ActivityType.MiniGameBlue2Screen);
        }
    }
}