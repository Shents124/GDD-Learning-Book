using System;
using UnityEngine;

namespace Minigame.RedColor
{
    public class Draggable : MonoBehaviour
    {
        [SerializeField] private Transform center;
        [SerializeField] private float threshold = 5f;
        [SerializeField] private int sortingOrder = 100;
        
        private SpriteRenderer _spriteRenderer;
        protected Vector3 startPos;
        protected Vector3 mousePos;
        
        private Action _onTrigger;
        private int _sortingOrder;
        private Vector3 _center;
        
        private void Start()
        {
            startPos = transform.position;
            _center = center.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sortingOrder = _spriteRenderer.sortingOrder;
        }

        public void AddListener(Action action)
        {
            _onTrigger += action;
        }
        
        protected virtual void OnMouseDown()
        {
            mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            _spriteRenderer.sortingOrder = sortingOrder;
        }

        protected virtual void OnMouseDrag()
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos);

            if (Vector3.Distance(center.position, _center) >= threshold)
            {
                _onTrigger?.Invoke();
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnMouseUp()
        {
            transform.position = startPos;
            _spriteRenderer.sortingOrder = _sortingOrder;
        }

        protected void OnTrigger()
        {
            _onTrigger?.Invoke();
        }
    }
}
