using System.Collections.Generic;
using UnityEngine;

namespace Minigame.RedColor
{
    public class PlateStrawberryObject : MonoBehaviour
    {
        [SerializeField] private GameObject plate;
        [SerializeField] private List<GameObject> strawberries;

        public void Active()
        {
            SetPlate(true);
            SetStrawberries(true);
            SetStrawberriesParent();
        }

        public void SetPlate(bool value)
        {
            plate.SetActive(value);
        }

        public void SetStrawberries(bool value)
        {
            foreach (var strawberry in strawberries)
            {
                strawberry.SetActive(value);
            }
        }

        private void SetStrawberriesParent()
        {
            foreach (var strawberry in strawberries)
            {
                strawberry.transform.SetParent(this.transform);
            }
        }
    }
}