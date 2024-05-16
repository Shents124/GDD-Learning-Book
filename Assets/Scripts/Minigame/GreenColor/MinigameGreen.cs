using System.Collections;
using System.Collections.Generic;
using Tracking;
using UI;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

public class MinigameGreen : BaseActivity
{
    protected override void Start()
    {
        base.Start();
        UIService.PlayFadeOut();
        ProductTracking.miniGameStep = 1;
        ProductTracking.miniGameSession = 1;
        ProductTracking.step = 4;
        ProductTracking.LogMiniGameStart();
    }
}
