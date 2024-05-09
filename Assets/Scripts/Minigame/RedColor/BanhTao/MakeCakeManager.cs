﻿
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sound.Service;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class MakeCakeManager : MonoBehaviour
{
    public List<BaseStep> allSteps;

    public Button btnNext, btnBack;

    private int currentStep = 0;

    private bool _isMoveStep;

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

    public void PlaySoundButTao()
    {
        AudioUtility.PlaySFX(AudioClipName.Remove);
    }

    private void NextStep()
    {
        if (currentStep == allSteps.Count - 1)
        {
            if (_isMoveStep)
                return;
            
            _isMoveStep = true;
            AdsManager.Instance.ShowInterstitial(() => {
                UIService.PlayFadeIn(() => {
                    UIService.OpenActivityAsyncNoClose(ActivityType.BakeCake).Forget();
                    Destroy(this.gameObject);
                });
            });

        }
        else
        {
            currentStep++;
        }
    }


    private async void OnClickedBackBtn()
    {
        AudioUtility.PlayUISfx(AudioClipName.Button);
        await UIService.OpenActivityAsyncNoClose(ActivityType.MenuScreen);
        UIService.PlayFadeOut();
        Destroy(this.gameObject);
    }

    private void OnClickedNextBtn()
    {
        AudioUtility.PlayUISfx(AudioClipName.Button);
        if (_isMoveStep)
            return;

        _isMoveStep = true;
        AdsManager.Instance.ShowInterstitial(() =>
        {
            UIService.PlayFadeIn(() => {
                UIService.OpenActivityAsyncNoClose(ActivityType.BakeCake).Forget();
                Destroy(this.gameObject);
            });
        });
    }
}
