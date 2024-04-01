using UnityEngine;

namespace Minigame.RedColor
{
    public class PlateStrawberry : Draggable
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            OnTrigger();
            gameObject.SetActive(false);
        }
    }
}