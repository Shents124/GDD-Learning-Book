using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minigame.BlueColor
{
    public class Clothes : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField] private Image dottedLine;
        [SerializeField] private GameObject front;
        [SerializeField] private GameObject behind;


        
        
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrop(PointerEventData eventData)
        {
            
        }
    }
}