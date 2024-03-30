using System;
using UnityEngine;

namespace Minigame.RedColor
{
    public class Draggable : MonoBehaviour
    {
        private Vector3 _mousePos;

        private void GetMousePos()
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        private void OnMouseDown()
        {
            _mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        }

        private void OnMouseDrag()
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePos);
        }
    }
}
