using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;

public class CheckInternetActivity : Activity
{
    public Button btnReconnect;
    public TMP_Text txtLoading;

    protected override void Start()
    {
        btnReconnect.onClick.AddListener(Reconnect);
        EventManager.Connect(Events.CheckNotInternet, NoInternet);
    }
    protected override void OnDisable()
    {
        EventManager.Disconnect(Events.CheckNotInternet, NoInternet);
    }

    private void Reconnect()
    {
        btnReconnect.interactable = true;
        txtLoading.text = "Loading ...";
        EventManager.SendSimpleEvent(Events.ReconectInternet);
    }

    private void NoInternet()
    {
        btnReconnect.interactable = true;
        txtLoading.text = "No Internet\nConnection";
    }
}
