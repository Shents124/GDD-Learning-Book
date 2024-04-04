using System;
using System.Collections;
using System.Collections.Generic;
using Constant;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Minigame.RedColor;
using Spine.Unity;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using ZBase.UnityScreenNavigator.Core.Activities;

public class BakeCake : Activity
{
    [Header("Done Step")]
    public Transform doneAll;
    public Transform cakeShow;

    [Header("Step Bake")]
    public Image cakeNotDone;
    public Image cakeComplete;

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

    public void ActiveMachine()
    {
        btnDongLo.gameObject.SetActive(false);
        btnMoLo.gameObject.SetActive(true);
        disActiveLo.DOFade(0, 1f);
        activeLo.DOFade(1, 1f);
        currentTimeBake = timeBake;
        UpdateNumber();
        StartBakeCake();
        AnimBakeCake();
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
            UpdateNumber();
            animCuaLo.AnimationState.SetAnimation(0, animMo, false);
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

    private void AnimBakeCake()
    {
        animCuaLo.transform.DOScale(0.98f, 0.25f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        cakeNotDone.GetComponent<DOTweenAnimation>().DOPlay();
        cakeNotDone.DOColor(Color.yellow, timeBake / 2).OnComplete(() => {
            cakeComplete.gameObject.SetActive(true);
            cakeComplete.DOFade(1, timeBake / 2);
            cakeComplete.transform.DOScaleY(0.8f, 0.25f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            cakeNotDone.DOFade(0, timeBake / 2).OnComplete(async () => {
                cakeNotDone.gameObject.SetActive(false);
                cakeComplete.transform.DOKill();
                animCuaLo.transform.DOKill();
                animCuaLo.AnimationState.SetAnimation(0, animMo, false);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                doneAll.gameObject.SetActive(true);
                cakeComplete.transform.parent = doneAll;
                cakeComplete.transform.DOMove(cakeShow.position, 0.75f);
                cakeComplete.transform.DOScale(cakeShow.localScale, 0.75f).OnComplete(async () => {
                    await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
                    UIService.PlayFadeIn(NextStep);
                });
            });
            // vfx active
        });
    }

    private void NextStep()
    {
        var step = LoadResourceService.LoadStep<StrawberryJuiceStepManager>(PathConstants.MINI_GAME_STEP_2);
        UIService.CloseActivityAsync(ActivityType.BakeCake, false).Forget();
        UIService.PlayFadeOut();
    }
}
