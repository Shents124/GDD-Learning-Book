using System.Collections;
using UnityEngine;

namespace Minigame.RedColor
{
    public class StrawberryJuiceStep3 : BaseStep
    {
        private void Start()
        {
            StartCoroutine(CompletedScene());
        }

        private IEnumerator CompletedScene()
        {
            yield return new WaitForSeconds(2f);
            CompletedStep();
        }
    }
}