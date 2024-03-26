using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ScratchCard;
using ScratchCardAsset;
using Step7;
using UI;
using UnityEngine;
using UnityEngine.UI;
using ZBase.UnityScreenNavigator.Core.Activities;

public class Step8Activity : Activity
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


    public override UniTask Initialize(Memory<object> args)
    {
        CardManager.MainCamera = Camera.main;
        return base.Initialize(args);
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
        EraseProgress.OnProgress += DoneFillColor;
        _typeObject = type;

    }

    private void DoneFillColor(float progress)
    {
        if(progress >= percentToDone)
        {
            Debug.Log("Done");
            imageNotDones[(int)_typeObject].gameObject.SetActive(false);
            var step7 = UnityEngine.Object.FindObjectOfType<Step7Activity>(true);
            if(step7 != null)
            {
                if (step7.CheckDoneStep)
                {
                    Debug.Log("Change State");
                }
                else
                {
                    UIService.OpenActivityAsync(ActivityType.Step7).Forget();
                }
            }
        }
    }
}
