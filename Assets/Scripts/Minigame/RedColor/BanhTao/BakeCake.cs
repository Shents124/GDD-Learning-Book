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
    public SwiftActionUI btnMoLo;
    public Button btnAddCake;
    public SwiftActionUI btnDongLo;

    public Image activeLo;
    public Image disActiveLo;

    public GameObject cakeDone;

    public SkeletonGraphic animCuaLo;

    [SpineAnimation]
    public string animDong;

    [SpineAnimation]
    public string animMo;

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
        UIService.PlayFadeIn(()=> { UIService.OpenActivityAsync(ActivityType.Step7).Forget(); });
    }

    public void AddCake()
    {
        btnAddCake.transform.DOScale(cakeDone.transform.localScale, 1f).SetEase(Ease.Linear);
        btnAddCake.transform.DOMove(cakeDone.transform.position, 1f).SetEase(Ease.Linear).OnComplete(() => {
            btnAddCake.gameObject.SetActive(false);
            cakeDone.SetActive(true);
            btnDongLo.gameObject.SetActive(true);
        });
    }

    public void OpenMachine()
    {
        btnMoLo.gameObject.SetActive(false);
        btnAddCake.gameObject.SetActive(true);
        btnAddCake.transform.localPosition = Vector3.zero;
        btnAddCake.transform.DOScale(1f, 1f).SetEase(Ease.Linear);
    }
}
