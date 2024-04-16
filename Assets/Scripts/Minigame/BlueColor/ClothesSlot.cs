using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

namespace Minigame.BlueColor
{
    public class ClothesSlot : MonoBehaviour, IDropHandler, IDragHandler
    {
        [SerializeField] private Image dottedLine;
        [SerializeField] private Image front;
        [SerializeField] private Image behind;

        private bool _isAdd;
        
        private void Awake()
        {
            dottedLine.gameObject.SetActive(false);
            front.gameObject.SetActive(false);
            behind.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.onBeginDragBlueClothes += OnBeginDragBlueClothes;
            EventManager.onEndDragBlueClothes += OnEndDragBlueClothes;
        }

        private void OnDisable()
        {
            EventManager.onBeginDragBlueClothes -= OnBeginDragBlueClothes;
            EventManager.onEndDragBlueClothes -= OnEndDragBlueClothes;
        }

        private void OnEndDragBlueClothes()
        {
            dottedLine.gameObject.SetActive(false);
        }

        private void OnBeginDragBlueClothes(BlueClothesType obj)
        {
            if (_isAdd == false)
                ShowDotted(obj);
        }

        private void ShowDotted(BlueClothesType blueClothesType)
        {
            dottedLine.gameObject.SetActive(true);
            dottedLine.sprite = LoadSpriteService.LoadDottedClothes(blueClothesType);
        }

        private void ShowClothes(BlueClothesType blueClothesType)
        {
            dottedLine.gameObject.SetActive(false);
            front.gameObject.SetActive(true);
            behind.gameObject.SetActive(false);

            front.sprite = LoadSpriteService.LoadFrontClothes(blueClothesType);
            behind.sprite = LoadSpriteService.LoadBeHindClothes(blueClothesType);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (_isAdd)
                return;
            
            if (eventData.pointerDrag != null)
            {
                var clothes = eventData.pointerDrag.GetComponent<Clothes>();
                if (clothes == null)
                    return;
                
                clothes.SetCanDrag(false);
                clothes.SetPosition(GetComponent<RectTransform>().anchoredPosition);
                ShowClothes(clothes.BlueClothesType);
                clothes.gameObject.SetActive(false);
                _isAdd = true;
                EventManager.CallEndDragBlueClothes();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}