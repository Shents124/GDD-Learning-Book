using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Spine.Unity;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;

public class BakeCake : Activity
{
    [Header("Step Bake")]
    public int timeBake;

    private int currentTimeBake;

    public Sprite[] numbers;

    public Image[] currentNumbers;

    public Image activeLo;
    public Image disActiveLo;

    [Header("Step Machine")]
    public SwiftActionUI btnMoLo;
    public Button btnAddCake;
    public SwiftActionUI btnDongLo;
    [SpineAnimation]
    public string animDong;

    [SpineAnimation]
    public string animMo;

    public GameObject cakeDone;

    public SkeletonGraphic animCuaLo;



    protected override void Start()
    {
        btnAddCake.onClick.AddListener(AddCake);
    }

    public async void AnimOpenMachine()
    {
        animCuaLo.AnimationState.SetAnimation(0, animDong, false);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        ActiveMachine();
    }

    public async void AnimCloseMachine()
    {
        animCuaLo.AnimationState.SetAnimation(0, animMo, false);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        OpenMachine();
    }

    public async void ActiveMachine()
    {
        btnDongLo.gameObject.SetActive(false);
        btnMoLo.gameObject.SetActive(true);
        disActiveLo.DOFade(0, 1f);
        activeLo.DOFade(1, 1f);
        //Show time
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
    }

    private void AddCake()
    {
        btnAddCake.transform.DOScale(cakeDone.transform.localScale, 1f).SetEase(Ease.Linear);
        btnAddCake.transform.DOMove(cakeDone.transform.position, 1f).SetEase(Ease.Linear).OnComplete(() => {
            btnAddCake.gameObject.SetActive(false);
            cakeDone.SetActive(true);
            btnDongLo.gameObject.SetActive(true);
        });
    }

    private void OpenMachine()
    {
        btnMoLo.gameObject.SetActive(false);
        btnAddCake.gameObject.SetActive(true);
        btnAddCake.transform.localPosition = Vector3.zero;
        btnAddCake.transform.DOScale(1f, 1f).SetEase(Ease.Linear);
    }

    private async void StartBakeCake()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        currentTimeBake--;
        if(currentTimeBake <= 0)
        {
            //Anim banh done
        }
        else
        {
            UpdateNumber();
            StartBakeCake();
        }
    }

    private void UpdateNumber()
    {
        int time = currentTimeBake * 5;
        currentNumbers[0].sprite = numbers[time % 10];
        currentNumbers[1].sprite = numbers[time / 10];
    }
}
