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
        private Vector3 _startPos;
        
        private Action _onTrigger;
        private int _sortingOrder;
        private Vector3 _center;
        
        private void Start()
        {
            _startPos = transform.position;
            _center = center.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sortingOrder = _spriteRenderer.sortingOrder;
        }

        public void AddListener(Action action)
        {
            _onTrigger = action;
        }
        
        private Vector3 _mousePos;
        
        private void OnMouseDown()
        {
            _mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            _spriteRenderer.sortingOrder = sortingOrder;
        }

        private void OnMouseDrag()
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePos);

            if (Vector3.Distance(center.position, _center) >= threshold)
            {
                _onTrigger?.Invoke();
                gameObject.SetActive(false);
            }
        }

        private void OnMouseUp()
        {
            transform.position = _startPos;
            _spriteRenderer.sortingOrder = _sortingOrder;
        }

        protected void OnTrigger()
        {
            _onTrigger?.Invoke();
        }
    }
}
