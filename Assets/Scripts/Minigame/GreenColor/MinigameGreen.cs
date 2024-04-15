using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core.Activities;

public class MinigameGreen : BaseActivity
{
    protected override void Start()
    {
        base.Start();
        UIService.PlayFadeOut();
    }
}
