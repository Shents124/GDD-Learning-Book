using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Minigame.YellowColor
{
    public class BasketEgg : MonoBehaviour
    {
        [SerializeField] private float eggMoveDuration = 1.5f;
        [SerializeField] private float shakeDuration = 0.2f;
        [SerializeField] private float showDuration = 0.5f;
        [SerializeField] private List<RectTransform> eggPositions;
        [SerializeField] private List<YellowEgg> eggs;

        [SerializeField] private RectTransform showPosition;
        [SerializeField] private GameObject backDrop;
        
        private int _currentEgg;
        private Action _onFinish;
        private RectTransform _rectTransform;
        private RectTransform _parent;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            backDrop.SetActive(false);
        }

        public void Initialize(Action onFinish, RectTransform parent)
        {
            _onFinish = onFinish;
            _parent = parent;
            
            for (int i = 0, n = eggs.Count; i < n; i++)
            {
                eggs[i].Initialize(eggPositions[i], eggMoveDuration, OnGetEgg);
            }
        }

        private void OnGetEgg()
        {
            _rectTransform.DOShakeScale(shakeDuration, 0.1f, 1, 0).OnComplete(() => {
                
                _currentEgg++;

                if (_currentEgg < 4)
                    return;

                backDrop.SetActive(true);
                _rectTransform.SetParent(_parent);
                _rectTransform.DOAnchorPos(showPosition.anchoredPosition, showDuration)
                    .OnComplete(PlayAnimRun);
            });
        }

        private void PlayAnimRun()
        {
            StartCoroutine(Coroutine());
            foreach (var egg in eggs)
            {
                egg.BirthChicken();
            }
        }

        private IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(3f);
            _onFinish?.Invoke();
        }
    }
}