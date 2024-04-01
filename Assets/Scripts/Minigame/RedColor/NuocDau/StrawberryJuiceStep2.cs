using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep2 : MonoBehaviour
    {
        [SerializeField] private GameObject plateStrawberryObj;
        [SerializeField] private PlateStrawberry plateStrawberry;

        private void Start()
        {
            plateStrawberryObj.SetActive(false);
            plateStrawberry.AddListener(OnClick);
        }

        private void OnClick()
        {
            plateStrawberryObj.SetActive(true);
        }
    }
}