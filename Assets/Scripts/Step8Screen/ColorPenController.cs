using System;
using System.Collections;
using System.Collections.Generic;
using Sound.Service;
using UnityEngine;
using UnityEngine.UI;

public class ColorPenController : MonoBehaviour
{
    [SerializeField] private GameObject penNotReady, penReady;

    public Step8Activity step8;

    [SerializeField] private Transform posStart;

    public bool IsPlay { get => isPlay; }

    private bool isPlay;

    public bool isReady = false, isPlaySound = false;

    public void StartInGame()
    {
        transform.position = posStart.position;
        isPlay = false;
        isReady = false;
        isPlaySound = false;
        penNotReady.SetActive(true);
        penReady.SetActive(false);
    }

    private void Update()
    {
        if (isPlay && isReady)
        {
            transform.position = Input.mousePosition;
        }
    }


    public void OnStartDrag()
    {
        if (!isReady)
            return;
        isPlay = true;
        penNotReady.SetActive(false);
        penReady.SetActive(true);  
        step8.CardManager.Card.InputEnabled = true;
    }

    public void OnEndDrag()
    {

        if (!isReady)
            return;
        if (isPlaySound)
        {
            isPlaySound = false;
            AudioUtility.StopSFX();
            step8.vfxTo.gameObject.SetActive(false);
        }
        isPlay = false;
        transform.position = posStart.position;
        penNotReady.SetActive(true);
        penReady.SetActive(false);
        step8.CardManager.Card.InputEnabled = false;
    }
}
