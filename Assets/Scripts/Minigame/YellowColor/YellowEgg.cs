using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.YellowColor
{
    public class YellowEgg : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject shadow;
        [SerializeField] private BabyChicken babyChicken;
        
        private RectTransform _rectTransform;
        private RectTransform _parent;
        private float _moveDuration;
        private Action _callback;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            button.interactable = false;
            button.onClick.AddListener(OnClick);
            babyChicken.gameObject.SetActive(false);
        }

        public void Initialize(RectTransform parent, float moveDuration, Action callback)
        {
            _parent = parent;
            _moveDuration = moveDuration;
            button.interactable = true;
            _callback = callback;
        }
        
        private void SetOnClick(bool value)
        {
            button.interactable = value;
        }
        
        public void DoMove(RectTransform endPosition, float duration, Action callback = null)
        {
            shadow.SetActive(false);
            _rectTransform.DOJump(endPosition.position, 400f, 1, duration).OnComplete(() => {
                callback?.Invoke();
                shadow.SetActive(true);
                SetOnClick(true);
            });
        }
        
        private void OnClick()
        {
            _rectTransform.SetParent(_parent);
            _rectTransform.DOAnchorPos(Vector2.zero, _moveDuration).OnComplete(() => {
                _callback?.Invoke();
                SetOnClick(false);
            });
        }

        public void BirthChicken()
        {
            button.gameObject.SetActive(false);
            shadow.gameObject.SetActive(false);
            babyChicken.gameObject.SetActive(true);
            babyChicken.PlayAnimRun();
        }
    }
}