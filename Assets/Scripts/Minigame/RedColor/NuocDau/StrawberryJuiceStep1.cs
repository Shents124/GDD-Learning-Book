using System.Collections.Generic;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep1 : BaseStep
    {
        [SerializeField] private int numberStrawberry = 7;
        [SerializeField] private List<Strawberry> strawberries;

        private void Start()
        {
            SetData();
        }

        private void SetData()
        {
            foreach (var strawberry in strawberries)
            {
                strawberry.SetData(OnCleanStrawberry);
            }
        }
        
        private void OnCleanStrawberry()
        {
            numberStrawberry--;
            if (numberStrawberry <= 0)
            {
                CompletedStep();
            }
        }

        private void CompletedStep()
        {
            
        }
    }
}