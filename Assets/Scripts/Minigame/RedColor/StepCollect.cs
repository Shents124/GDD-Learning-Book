using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
                    NextStep();
                    bucketCollectDone.transform.position = pos;
                });
                bucketCollectDone.transform.DOMove(bucketDonePos.position, 1f).OnComplete(() => {
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
    }
}
