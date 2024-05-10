using System;
using Coffee.UIExtensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class BakeCake : BaseActivity
{
    [SpineAnimation(dataField: "animPlayer")]
    public string animWin, animIdle;

    public SkeletonGraphic animPlayer;

    [Header("Done Step")]
    public Transform doneAll;
    public CanvasGroup doneAllWithWater, showAfterDone;
    public UIParticle vfxDone;
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
        await AsyncService.Delay(0.5f, this);
        ActiveMachine();
    }

    public async void AnimCloseMachine()
    {
        animCuaLo.AnimationState.SetAnimation(0, animMo, false);
        await AsyncService.Delay(0.5f, this);
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
        btnAddCake.transform.DOKill();
        btnAddCake.transform.rotation = Quaternion.identity;
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
        btnAddCake.transform.DOLocalRotate(new Vector3(btnAddCake.transform.localRotation.x, btnAddCake.transform.localRotation.y, -20f), 0.5f).SetEase(Ease.Linear);
        btnAddCake.transform.DOLocalRotate(new Vector3(btnAddCake.transform.localRotation.x, btnAddCake.transform.localRotation.y, 20f), 0.25f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo)
        .SetDelay(0.5f);

        btnAddCake.transform.DOScale(1f, 1f).SetEase(Ease.Linear);
    }

    private async void StartBakeCake()
    {
        AudioUtility.PlaySFX(AudioClipName.Applepie_baking, true);
        await AsyncService.Delay(1f, this);
        currentTimeBake--;
        if(currentTimeBake <= 0)
        {
            UpdateNumber();
            AudioUtility.StopSFX();
            animCuaLo.AnimationState.SetAnimation(0, animMo, false);
            AudioUtility.PlaySFX(AudioClipName.Correct);
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
            cakeNotDone.DOFade(0, timeBake / 2).OnComplete(() => Callback().Forget());
            // vfx active
        });
    }

    private async UniTaskVoid Callback()
    {
        cakeNotDone.gameObject.SetActive(false);
        cakeComplete.transform.DOKill();
        animCuaLo.transform.DOKill();
        animCuaLo.AnimationState.SetAnimation(0, animMo, false);
        await AsyncService.Delay(0.5f, this);
        doneAll.gameObject.SetActive(true);
        cakeComplete.transform.parent = doneAll;
        cakeComplete.transform.DOMove(cakeShow.position, 0.75f);
        cakeComplete.transform.DOScale(cakeShow.localScale, 0.75f)
            .OnComplete(() => Action().Forget());
    }

    private async UniTaskVoid Action()
    {
        vfxDone.gameObject.SetActive(true);
        AudioUtility.PlaySFX(AudioClipName.Clearstep);
        cakeComplete.transform.SetAsFirstSibling();
        await AsyncService.Delay(1f, this);
        NextStep().Forget();
    }

    private async UniTaskVoid NextStep()
    {
        doneAllWithWater.gameObject.SetActive(true);
        doneAllWithWater.DOFade(1, 1f);
        await AsyncService.Delay(1f, this);
        var track = animPlayer.AnimationState.SetAnimation(0, animWin, false);
        AudioUtility.PlaySFX(AudioClipName.Hooray_WF);
        track.Complete += async Entry => {
            animPlayer.AnimationState.SetAnimation(0, animWin, true);
            await AsyncService.Delay(1f, this);
            UIService.OpenActivityWithFadeIn(ActivityType.Step7Red);
        };
    }
}
