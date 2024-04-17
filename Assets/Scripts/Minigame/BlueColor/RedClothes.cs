using UnityEngine;

namespace Minigame.BlueColor
{
    public class RedClothes : MonoBehaviour
    {
        [SerializeField] private GameObject dotted;
        [SerializeField] private GameObject front;
        [SerializeField] private GameObject behind;

        private void Start()
        {
            Show(false);
        }

        public void Show(bool value)
        {
            front.SetActive(value);
            behind.SetActive(value);
            dotted.SetActive(!value);
        }
    }
}