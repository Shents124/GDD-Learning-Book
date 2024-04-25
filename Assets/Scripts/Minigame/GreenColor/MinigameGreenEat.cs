using System;
using System.Collections;
using System.Collections.Generic;
using Constant;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Spine.Unity;
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

    public SkeletonGraphic frogAnim;

    [SpineAnimation]
    public string animEat, animIdle, animSession;

    private bool isEating;
    protected override void Start()
    {
        UIService.PlayFadeOut();
        isEating = false;
        foreach (var anim in animalFlyRandoms)
        {
            anim.actionClick += ()=>
            {
                if (isEating)
                    return;
                frog.Eat(anim, OnClickAnimal);
            };
        }
    }

    protected override void OnClickedNextBtn()
    {
        AdsManager.Instance.ShowInterstitial(() => {
            UIService.PlayFadeIn(() => {
                var step = LoadResourceService.LoadStep<MinigameGreenWay>(PathConstants.MINI_GAME_GREEN_STEP_3);
                UIService.CloseActivityAsync(ActivityType.MinigameGreen2Screen, false).Forget();
                UIService.PlayFadeOut();
            });
        });

    }

    public void OnClickAnimal(bool isCurrentAnimal)
    {
        isEating = true;
        var track = frogAnim.AnimationState.SetAnimation(0, animEat, false);
        track.Complete += Entry => {
            if (isCurrentAnimal)
            {
                animalMissions[_currentStep].SetActive(true);
                animalMissions[_currentStep].transform.localScale = Vector3.one * 4f;
                animalMissions[_currentStep].transform.DOScale(Vector3.one * 2f, 0.25f).SetEase(Ease.InOutElastic).OnComplete(async () => {
                    _currentStep++;
                    if (_currentStep >= stepNeed)
                    {
                        frogAnim.AnimationState.SetAnimation(0, animSession, true);
                        await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));
                        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
                        AdsManager.Instance.ShowInterstitial(() => {
                            UIService.PlayFadeIn(() => {
                                var step = LoadResourceService.LoadStep<MinigameGreenWay>(PathConstants.MINI_GAME_GREEN_STEP_3);
                                UIService.CloseActivityAsync(ActivityType.MinigameGreen2Screen, false).Forget();
                                UIService.PlayFadeOut();
                            });
                        });
                        return;
                    }
                    else
                    {
                        frogAnim.AnimationState.SetAnimation(0, animSession, false);
                        await UniTask.Delay(TimeSpan.FromSeconds(1f));
                        frogAnim.AnimationState.SetAnimation(0, animIdle, true);
                    }
                    isEating = false;
                });
            }
            else
            {
                frogAnim.AnimationState.SetAnimation(0, animIdle, true);
                isEating = false;
            }
        };
    }
}
