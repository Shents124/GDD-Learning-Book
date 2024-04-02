using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;

public class BakeCake : Activity
{
    public Button btnMoLo;
    public Button btnAddCake;
    public Button btnDongLo;

    public Image activeLo;
    public Image disActiveLo;

    public GameObject cakeDone;

    protected override void Start()
    {
        btnMoLo.onClick.AddListener(OpenMachine);
        btnDongLo.onClick.AddListener(ActiveMachine);
        btnAddCake.onClick.AddListener(AddCake);
    }

    public async void ActiveMachine()
    {
        btnDongLo.gameObject.SetActive(false);
        btnMoLo.gameObject.SetActive(true);
        disActiveLo.DOFade(0, 1f);
        activeLo.DOFade(1, 1f);
        //Show time
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
        UIService.OpenActivityAsync(ActivityType.Step7).Forget();
    }

    public void AddCake()
    {
        btnAddCake.transform.DOScale(cakeDone.transform.localScale, 1f).SetEase(Ease.Linear);
        btnAddCake.transform.DOMove(cakeDone.transform.position, 1f).SetEase(Ease.Linear).OnComplete(() => {
            btnAddCake.gameObject.SetActive(false);
            cakeDone.SetActive(true);
            btnDongLo.interactable = true;
        });
    }

    public void OpenMachine()
    {
        btnMoLo.gameObject.SetActive(false);
        btnDongLo.gameObject.SetActive(true);
        btnDongLo.interactable = false;
        btnAddCake.gameObject.SetActive(true);
        btnAddCake.transform.localPosition = Vector3.zero;
        btnAddCake.transform.DOScale(1f, 1f).SetEase(Ease.Linear);
    }
}
