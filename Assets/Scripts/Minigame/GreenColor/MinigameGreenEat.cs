using System;
using System.Collections;
using System.Collections.Generic;
using Constant;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;
using Utility;
using ZBase.UnityScreenNavigator.Core.Activities;

public class MinigameGreenEat : BaseActivity
{
    private int _currentStep;

    public int stepNeed;

    public List<AnimalFlyRandom> animalFlyRandoms;

    public List<GameObject> animalMissions;

    public TongleFrog frog;

    public SkeletonGraphic frogAnim, animPlayer;

    [SpineAnimation]
    public string animOpen, animMid, animEat, animIdle, animSession;

    [SpineAnimation(dataField: "animPlayer")]
    public string animPlayerTalk, animPlayerIdle;
    private bool isEating;

    public Transform posFall, posStart;

    public GameObject bgPlayer;
    protected override void Start()
    {
        UIService.PlayFadeOut();
        ProductTracking.miniGameStep = 2;
        ProductTracking.miniGameSession = 1;
        isEating = false;
        ShowTalk();
        foreach (var anim in animalFlyRandoms)
        {
            anim.actionClick += ()=>
            {
                if (isEating)
                    return;
                isEating = true;
                frog.Eat(anim, OnClickAnimal, StartEat);
            };
        }
    }
    private void ShowTalk()
    {
        animPlayer.transform.DOLocalMoveY(posStart.localPosition.y, 1f).OnComplete(() => 
        {
            var track = animPlayer.AnimationState.SetAnimation(0, animPlayerTalk, true);
            AudioUtility.PlaySFX(this, AudioClipName.Green_frog_eat, 0, () => {
                animPlayer.AnimationState.SetAnimation(0, animPlayerIdle, true);
                animPlayer.transform.DOLocalMoveY(posFall.localPosition.y, 1f).OnComplete(() => {
                    bgPlayer.SetActive(false);
                });
            });
        });

    }

    protected override void OnClickedNextBtn()
    {
        AudioUtility.PlayUISfx(AudioClipName.Button);
        AdsManager.Instance.ShowInterstitial((result) => {
            
            AdTracker.LogAdInter(GetTrackingAdInter(), result);
            
            UIService.PlayFadeIn(() => {
                var step = LoadResourceService.LoadStep<MinigameGreenWay>(PathConstants.MINI_GAME_GREEN_STEP_3);
                UIService.CloseActivityAsync(ActivityType.MinigameGreen2Screen, false).Forget();
                UIService.PlayFadeOut();
            });
        });

    }
    private void StartEat()
    {
        var track = frogAnim.AnimationState.SetAnimation(0, animOpen, false);
        track.Complete += Entry => {
            frogAnim.AnimationState.SetAnimation(0, animMid, true);
        };

    }

    public void OnClickAnimal(bool isCurrentAnimal)
    {
        AudioUtility.PlaySFX(AudioClipName.Frog_eat);
        var track = frogAnim.AnimationState.SetAnimation(0, animEat, false);
        track.Complete += Entry => {
            AudioUtility.StopSFX();
            if (isCurrentAnimal)
            {
                animalMissions[_currentStep].SetActive(true);
                animalMissions[_currentStep].transform.localScale = Vector3.one * 4f;
                animalMissions[_currentStep].transform.DOScale(Vector3.one * 2f, 0.25f).SetEase(Ease.InOutElastic).OnComplete(async () => {
                    _currentStep++;
                    if (_currentStep >= stepNeed)
                    {
                        AudioUtility.PlaySFX(AudioClipName.Frog_laugh);
                        frogAnim.AnimationState.SetAnimation(0, animSession, true);
                        await AsyncService.Delay(1.5f, this);
                        AudioUtility.StopSFX();
                        AdsManager.Instance.ShowInterstitial((result) => {
                            
                            AdTracker.LogAdInter(GetTrackingAdInter(), result);
                            
                            UIService.PlayFadeIn(() => {
                                var step = LoadResourceService.LoadStep<MinigameGreenWay>(PathConstants.MINI_GAME_GREEN_STEP_3);
                                UIService.CloseActivityAsync(ActivityType.MinigameGreen2Screen, false).Forget();
                                UIService.PlayFadeOut();
                            }, alphaDone : 0.75f);
                        });
                        return;
                    }
                    else
                    {
                        AudioUtility.PlaySFX(AudioClipName.Frog_laugh);
                        frogAnim.AnimationState.SetAnimation(0, animSession, false);
                        await AsyncService.Delay(1f, this);
                        AudioUtility.StopSFX();
                        frogAnim.AnimationState.SetAnimation(0, animIdle, true);
                    }
                    isEating = false;
                });
            }
            else
            {
                AudioUtility.PlaySFX(AudioClipName.Fail);
                frogAnim.AnimationState.SetAnimation(0, animIdle, true);
                isEating = false;
            }
        };
    }
    
    private static TrackingAdInter GetTrackingAdInter()
    {
        return new TrackingAdInter {
            hasData = true,
            adLocation = AdLocation.start,
            levelName = LevelName.green,
            miniGameSession = "3",
            isWoaAd = false
        };
    }
}
