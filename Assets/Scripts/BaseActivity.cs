using System;
using Cysharp.Threading.Tasks;
using Sound.Service;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;


public class BaseActivity : Activity
{
    [SerializeField] protected Button backBtn;
    [SerializeField] protected Button nextBtn;
    [SerializeField] protected ActivityType nextActivity;

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

    protected virtual void InitializeData(Memory<object> args)
    {
        
    }

    private static async UniTask OnClickedBackBtn()
    {
        AudioUtility.PlayUISfx(AudioClipName.Button);
        await UIService.OpenActivityAsync(ActivityType.MenuScreen);
        UIService.PlayFadeOut();
    }

    protected virtual void OnClickedNextBtn()
    {
        AudioUtility.PlayUISfx(AudioClipName.Button);
        UIService.OpenActivityWithFadeIn(nextActivity);
    }
    
    private static void FadeOut()
    {
        UIService.PlayFadeOut();
    }
}