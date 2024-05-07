using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sound.Service;
using UnityEngine;

namespace Minigame.BlueColor
{
    public class SelectedClothes : MonoBehaviour
    {
        [SerializeField] private List<Clothes> blueClothes;
        [SerializeField] private List<RectTransform> redClothes;

        private int _blueClothesCount;
        private int _currentIndex;
        private Action _onFinish;
        
        public void Initialize(Action onFinish)
        {
            foreach (var item in blueClothes)
            {
                _blueClothesCount++;
                item.Initialize(OnDrag);
            }

            _onFinish = onFinish;
            ShowBlueClothes();
        }

        private void ShowBlueClothes()
        {
            for (int i = 0, n = blueClothes.Count; i < n; i++)
            {
                blueClothes[i].gameObject.SetActive(i == _currentIndex);
            }
        }

        public void MoveRedClothes(List<RedClothes> targets, float duration, Action onFinish)
        {
            StartCoroutine(MoveRedClothesCoroutine(targets, duration, onFinish));
        }

        private IEnumerator MoveRedClothesCoroutine(List<RedClothes> targets, float duration, Action onFinish)
        {
            var n = redClothes.Count;
            for (int i = 0; i < n; i++)
            {
                redClothes[i].transform.DOMove(targets[i].transform.position, duration);
            }

            yield return new WaitForSeconds(duration);
            AudioUtility.PlaySFX(AudioClipName.Hanging_clothes);
            for (int i = 0; i < n; i++)
            {
                redClothes[i].gameObject.SetActive(false);
            }
            
            onFinish?.Invoke();
        }
        
        private void OnDrag()
        {
            _blueClothesCount--;
            _currentIndex++;
            
            if (_blueClothesCount <= 0)
            {
                StartCoroutine(OnFinishCoroutine());
            }
            else
            {
                ShowBlueClothes();
            }
        }

        private IEnumerator OnFinishCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            _onFinish?.Invoke();
        }
    }
}