using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ScratchCardAsset;
using Step7;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;

public class Step8Activity : MonoBehaviour
{
    public float percentToDone = 0.9f;
    private TypeObject _typeObject;
    public GameObject[] imagesFill;
    public Image[] imageNotDones;
    public Image imageDone;
    public ColorPenController colorPenController;
    public GameObject penNotReady;
    public GameObject penReady;

    public ScratchCardManager CardManager;
    public EraseProgress EraseProgress;


    private void Start()
    {
        CardManager.MainCamera = Camera.main;
        EraseProgress.OnProgress += DoneFillColor;
    }

    public void InitData(TypeObject type)
    {
        //TODO: Animation
        colorPenController.isPlay = true;
        penNotReady.SetActive(false);
        penReady.SetActive(true);
        foreach (var image in imagesFill)
        {
            image.SetActive(false);
        }
        imagesFill[(int)type].SetActive(true);
        CardManager.ImageCard = imageNotDones[(int)type].gameObject;
        CardManager.ScratchSurfaceSprite = imageNotDones[(int)type].sprite;
        CardManager.InitData();
        _typeObject = type;

    }

    private void DoneFillColor(float progress)
    {
        if(progress >= percentToDone)
        {
            imageNotDones[(int)_typeObject].gameObject.SetActive(false);
            //TODO: anim fill done color
            this.gameObject.SetActive(false);
            //TODO: request event done
            EventManager.SendSimpleEvent(Events.FillColorDone);
        }
    }
}
