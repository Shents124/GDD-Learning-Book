using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class MakeCakeManager : MonoBehaviour
{
    public List<BaseStep> allSteps;

    public Button btnNext, btnBack;

    private int currentStep = 0;

    private void Start()
    {
        UIService.PlayFadeOut();
        btnBack.onClick.AddListener(OnClickedBackBtn);
        btnNext.onClick.AddListener(OnClickedNextBtn);
        for (int i = 0; i < allSteps.Count; i++)
        {
            if (i == 0)
            {
                allSteps[i].nextStep = allSteps[i + 1];
            }
            else if (i == allSteps.Count - 1)
            {
                allSteps[i].previousStep = allSteps[i - 1];
            }
            else
            {
                allSteps[i].nextStep = allSteps[i + 1];
                allSteps[i].previousStep = allSteps[i - 1];
            }
            allSteps[i].actionNext = NextStep;

        }
        //allSteps[0].InActive();
    }

    private void NextStep()
    {
        if (currentStep == allSteps.Count - 1)
        {
            UIService.PlayFadeIn(() => {

                UIService.OpenActivityAsyncNoClose(ActivityType.BakeCake).Forget();
                Destroy(this.gameObject);
            });

        }
        else
        {
            currentStep++;
        }
    }


    private async void OnClickedBackBtn()
    {
        await UIService.OpenActivityAsync(ActivityType.MenuScreen);
        UIService.PlayFadeOut();
        Destroy(this.gameObject);
    }

    private void OnClickedNextBtn()
    {
        UIService.PlayFadeIn(() => {
            UIService.OpenActivityAsyncNoClose(ActivityType.BakeCake).Forget();
            Destroy(this.gameObject);
            UIService.PlayFadeOut();
        });

    }
}
