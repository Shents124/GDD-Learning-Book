﻿using System.Collections.Generic;
using Constant;
using Cysharp.Threading.Tasks;
using Minigame.RedColor;
using UI;
using UnityEngine;
using Utility;

public class MinigameRedStep1Activity : BaseActivity
{
    public MakeCakeManager MakeCakeManager;

    public List<BaseStep> stepInMiniGame;

    private int currentStep = 0;

    protected override void Start()
    {
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

    protected override void OnClickedNextBtn()
    {
        UIService.PlayFadeIn(() => {
            NextStep();
            UIService.PlayFadeOut();
        });
    }
    
    void NextStep()
    {
        if (currentStep == stepInMiniGame.Count - 1)
        {
            //var manager = Instantiate(MakeCakeManager);
            var step = LoadResourceService.LoadStep<StrawberryJuiceStepManager>(PathConstants.MINI_GAME_RED_STEP_2);
            UIService.CloseActivityAsync(ActivityType.MinigameRed, true).Forget();
        }
        else
        {
            stepInMiniGame[currentStep].gameObject.SetActive(false);
            currentStep++;
            stepInMiniGame[currentStep].gameObject.SetActive(true);
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
