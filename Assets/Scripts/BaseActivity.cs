using System;
using Cysharp.Threading.Tasks;
using Sound.Service;
using Tracking;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;

public class BaseActivity : Activity
{
    [SerializeField] protected Button backBtn;
    [SerializeField] protected Button nextBtn;
    [SerializeField] protected ActivityType nextActivity;

    protected TrackingAdInter trackingAdInter;
    
    protected override void Awake()
    {
        backBtn.onClick.AddListener(() => {
            UIService.PlayFadeIn(() => {
                OnClickedBackBtn().Forget();
            });
        });
        
        nextBtn.onClick.AddListener(OnClickedNextBtn);
        
        base.Awake();
    }

    public override UniTask Initialize(Memory<object> args)
    {
        FadeOut();
        InitializeData(args);
        return base.Initialize(args);
    }

    protected virtual void InitializeData(Memory<object> args) { }

    private async UniTask OnClickedBackBtn()
    {
        AudioUtility.StopSFX();
        AudioUtility.PlayUISfx(AudioClipName.Button);
        ProductTracking.LogLevelEnd(ResultType.back);
        ProductTracking.LogMiniGameEnd(ResultType.back);
        await UIService.OpenActivityAsync(ActivityType.MenuScreen);
        UIService.PlayFadeOut();
    }

    protected virtual void OnClickedNextBtn()
    {
        AudioUtility.StopSFX();
        AudioUtility.PlayUISfx(AudioClipName.Button);
        SetDataTrackingAd();
        UIService.OpenActivityWithFadeIn(nextActivity, trackingAdInter: trackingAdInter);
    }

    protected virtual void SetDataTrackingAd()
    {
        trackingAdInter = default;
    }
    
    private static void FadeOut()
    {
        UIService.PlayFadeOut();
    }
}