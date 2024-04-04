using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep3 : BaseStep
    {
        [SerializeField] private ItemClick itemClick;

        private void Start()
        {
            itemClick.AddListener(CompletedStep);
        }
    }
}