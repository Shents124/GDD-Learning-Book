using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

public class MinigameGreenEat : Activity
{
    private int _currentStep;

    public int stepNeed;

    public List<AnimalFlyRandom> animalFlyRandoms;

    public List<GameObject> animalMissions;

    public TongleFrog frog;

    public SkeletonGraphic frogAnim;

    [SpineAnimation]
    public string animEat, animIdle, animSession;

    protected override void Start()
    {
        foreach (var anim in animalFlyRandoms)
        {
            anim.actionClick += ()=>
            {
                frog.Eat(anim, OnClickAnimal);
            };
        }
    }

    public void OnClickAnimal(bool isCurrentAnimal)
    {
        var track = frogAnim.AnimationState.SetAnimation(0, animEat, false);
        track.Complete += Entry => {
            if (isCurrentAnimal)
            {
                animalMissions[_currentStep].SetActive(false);
                _currentStep++;
                if(_currentStep >= stepNeed)
                {
                    frogAnim.AnimationState.SetAnimation(0, animSession, true);
                    return;
                }
            }
            frogAnim.AnimationState.SetAnimation(0, animIdle, true);
        };
    }
}
