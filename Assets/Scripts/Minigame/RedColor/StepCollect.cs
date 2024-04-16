using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using DG.Tweening;
using UI;
using UnityEngine;

public class StepCollect : BaseStep
{
    public int numberFruitNeed = 4;

    public DOTweenAnimation bucketCollectDone;

    private int _currentStep;

    public Action nextStepAction;

    public List<BtnCollectFruit> listFruits;

    public List<Transform> posCollect;

    public Transform posFall;

    public Transform bucketDonePos;

    public GameObject bucketDoneObj;

    public UIParticle vfx;
    public int CurrentFruit
    {
        get => _currentStep;
        set
        {
            _currentStep = value;
            if(numberFruitNeed <= _currentStep)
            {
                Vector3 pos = bucketCollectDone.transform.position;
                bucketCollectDone.onComplete.RemoveAllListeners();
                bucketCollectDone.onComplete.AddListener(() => {
                    UIService.PlayFadeIn(()=> 
                    {
                        NextStep();
                        UIService.PlayFadeOut();
                        bucketCollectDone.transform.position = pos;
                    });
                });
                bucketCollectDone.transform.parent = bucketDoneObj.transform;
                bucketCollectDone.transform.SetAsLastSibling();
                bucketDoneObj.SetActive(true);
                bucketCollectDone.transform.DOScale(bucketDonePos.localScale, 1f);
                vfx.transform.localScale = Vector3.zero;
                vfx.transform.DOScale(1, 1f);
                bucketCollectDone.transform.DOMove(bucketDonePos.position, 1f).OnComplete(() => 
                {
                    vfx.gameObject.SetActive(true);
                    bucketCollectDone.DOPlay();
                });
            }
        }
    }

    public override void NextStep()
    {
        nextStepAction?.Invoke();
        base.NextStep();
    }

    private void Start()
    {
        ResetStep();
    }

    public override void ResetStep()
    {
        base.ResetStep();
        CurrentFruit = 0;
        foreach (var fruit in listFruits)
        {
            fruit.gameObject.SetActive(true);
            fruit.managerStep = this;
        }
        foreach(var fruitDone in posCollect)
        {
            fruitDone.gameObject.SetActive(false);
        }
    }

    public void IncreaseFruit()
    {
        posCollect[CurrentFruit].gameObject.SetActive(true);
        CurrentFruit++;
        //bucketCollectDone.DOPlay();
    }
}
