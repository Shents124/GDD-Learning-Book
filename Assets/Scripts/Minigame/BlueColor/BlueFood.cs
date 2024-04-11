using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.BlueColor
{
    public class BlueFood : MonoBehaviour
    {
        [SerializeField] private float delayTime = 0.2f;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button button;
        [SerializeField] private List<RectTransform> foods;
        
        private Action<int> _onClick;
        private RectTransform _characterTransform;
        private float _foodMoveDuration;
        private int _index;
        
        private void Start()
        {
            button.onClick.AddListener(OnClicked);
            canvasGroup.alpha = 0f;
        }

        public void Initialize(RectTransform characterTransform, float foodMoveDuration, 
            int index, Action<int> onClick)
        {
            _onClick = onClick;
            _index = index;
            _foodMoveDuration = foodMoveDuration;
            _characterTransform = characterTransform;
            canvasGroup.DOFade(1f, 0.5f);
        }

        public IEnumerator MoveFoods(Action onFinish)
        {
            var delay = new WaitForSeconds(delayTime);
            foreach (var food in foods)
            {
                food.DOJump(_characterTransform.transform.position, 400f, 1, _foodMoveDuration);
                food.DOScale(Vector3.zero, _foodMoveDuration * 2);

                yield return delay;
            }

            yield return new WaitForSeconds(_foodMoveDuration - delayTime);
            onFinish?.Invoke();
        }

        private void OnClicked()
        {
            _onClick?.Invoke(_index);
        }
    }
}