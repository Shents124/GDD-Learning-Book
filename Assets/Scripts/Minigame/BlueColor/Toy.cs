using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.BlueColor
{
    public class Toy : MonoBehaviour
    {
        [SerializeField] private GameObject shadow;

        public bool canDrag = true;

        public DragObject dragObject;

        public int Index { get; private set; }
        
        private Button _button;
        private RectTransform _rectTransform;

        private Action<Toy> _onClick;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            if (canDrag)
            {
                dragObject = GetComponent<DragObject>();
                if(dragObject != null )
                {
                    dragObject.Initialize(OnClicked);
                }
            }
            else
            {
                _button = GetComponent<Button>();
                if( _button != null )
                    _button.onClick.AddListener(OnClicked);
            }
        }

        public void OnDrop()
        {
            OnClicked();
        }

        public void Initialize(int index, Action<Toy> onClick)
        {
            Index = index;
            _onClick = onClick;
        }
        
        public void DoMove(RectTransform destination, Vector3 scale, float duration, Action onFinish)
        {
            shadow.SetActive(false);
            if(_button)
                _button.interactable = false;
            _rectTransform.SetParent(destination);
            _rectTransform.DOJump(destination.transform.position, 400f, 1, duration).OnComplete(() => {
                onFinish?.Invoke();
            });
            _rectTransform.DOScale(scale, duration);
        }

        private void OnClicked()
        {
            _onClick?.Invoke(this);
        }
    }
}