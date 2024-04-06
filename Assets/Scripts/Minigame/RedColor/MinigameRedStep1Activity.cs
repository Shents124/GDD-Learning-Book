using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

public class MinigameRedStep1Activity : Activity
{
    public MakeCakeManager MakeCakeManager;

    public List<BaseStep> stepInMiniGame;

    private int currentStep = 0;

    protected override void Start()
    {
        UIService.PlayFadeOut();
        for (int i = 0; i < stepInMiniGame.Count; i++)
        {
            if (i == 0)
            {
                stepInMiniGame[i].nextStep = stepInMiniGame[i + 1];
            }
            else if(i == stepInMiniGame.Count - 1)
            {
                stepInMiniGame[i].previousStep = stepInMiniGame[i - 1];
            }
            else
            {
                stepInMiniGame[i].nextStep = stepInMiniGame[i + 1];
                stepInMiniGame[i].previousStep = stepInMiniGame[i - 1];
            }
            stepInMiniGame[i].actionNext = NextStep;
            stepInMiniGame[i].actionPrev = PrevStep;
        }
        stepInMiniGame[0].InActive();
    }

    void NextStep()
    {
        if (currentStep == stepInMiniGame.Count - 1)
        {
            var manager = Instantiate(MakeCakeManager);
            UIService.CloseActivityAsync(ActivityType.MinigameRed, true).Forget();
        }
        else
        {
            currentStep++;
        }
    }

    void PrevStep()
    {
        if(currentStep == 0) 
        {
            Debug.Log("Về step cũ");
        }
        else
        {
            stepInMiniGame[currentStep].BackStep();
            currentStep--;
        }
    }
}
