using System;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;


public class BaseActivity : Activity
{
    [SerializeField] protected Button backBtn;
    [SerializeField] protected Button nextBtn;


    public override UniTask Initialize(Memory<object> args)
    {
        FadeOut();
        InitializeData(args);
        return base.Initialize(args);
    }

    protected virtual void InitializeData(Memory<object> args)
    {
        
    }
    
    private static void FadeOut()
    {
        UIService.PlayFadeOut();
    }
}