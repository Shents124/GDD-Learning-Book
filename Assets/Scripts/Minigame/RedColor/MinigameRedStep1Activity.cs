using System.Collections.Generic;
using Constant;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Minigame.RedColor;
using Sound.Service;
using Spine.Unity;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;
using Utility;

public class MinigameRedStep1Activity : BaseActivity
{
    public MakeCakeManager MakeCakeManager;

    public List<BaseStep> stepInMiniGame;

    private int currentStep = 0;

    public CanvasGroup talkCanvas;

    [SpineAnimation(dataField: "animPlayer")]
    public string animTalk, animRun;

    public SkeletonGraphic animPlayer;

    private Vector2 posFallPlayer;

    protected override void Start()
    {
        ProductTracking.step = 4;
        ProductTracking.miniGameSession = 1;
        ProductTracking.miniGameStep = 1;
        ProductTracking.LogMiniGameStart();
        
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
    private void ShowTalkComplete()
    {
        talkCanvas.gameObject.SetActive(true);
        posFallPlayer = animPlayer.transform.position;
        talkCanvas.DOFade(1, 0.5f).OnComplete(() => 
        {
            AudioUtility.PlaySFX(AudioClipName.Clearstep);
            animPlayer.gameObject.SetActive(true);
            var track = animPlayer.AnimationState.SetAnimation(0, animTalk, true);
            AudioUtility.PlaySFX(this, AudioClipName.Red_cook, callback: () => {
                animPlayer.transform.localScale = new Vector2(-animPlayer.transform.localScale.x, animPlayer.transform.localScale.y);
                animPlayer.AnimationState.SetAnimation(0, animRun, true);
                animPlayer.transform.DOMove(posFallPlayer, 1f).OnComplete(() =>
                {
                    UIService.PlayFadeIn(() => {
                        var step = LoadResourceService.LoadStep<StrawberryJuiceStepManager>(PathConstants.MINI_GAME_RED_STEP_2);
                        UIService.CloseActivityAsync(ActivityType.MinigameRed, true).Forget();
                        UIService.PlayFadeOut();
                    });

                });
            });
        });
    }

    protected override void OnClickedNextBtn()
    {
        AudioUtility.PlayUISfx(AudioClipName.Button);
        AdsManager.Instance.ShowInterstitial((result) => {
            
            SetDataTrackingAd();
            AdTracker.LogAdInter(trackingAdInter, result);
            
            UIService.PlayFadeIn(() => {
                NextStep();
                UIService.PlayFadeOut();
            });
        });

    }
    
    protected override void SetDataTrackingAd()
    {
        trackingAdInter = new TrackingAdInter {
            hasData = true,
            levelName = LevelName.red,
            adLocation = AdLocation.start, 
            miniGameSession = "2", 
            isWoaAd = false
        };
    }
    
    void NextStep()
    {
        if (currentStep == stepInMiniGame.Count - 1)
        {
            ShowTalkComplete();
            stepInMiniGame[currentStep].gameObject.SetActive(false);
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
