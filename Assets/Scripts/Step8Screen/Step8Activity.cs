using System;
using System.Collections;
using System.Reflection;
using Coffee.UIExtensions;
using DG.Tweening;
using ScratchCardAsset;
using Sound.Service;
using Tracking;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class Step8Activity : MonoBehaviour
{
    public RectTransform content;
    public float percentToDone = 0.9f;
    private TypeObject _typeObject;
    public GameObject[] imagesFill;
    public Image[] imageNotDones;
    public Image[] imageNotDoneFakes;
    public Image[] imageDone;
    public ColorPenController colorPenController;
    public ScratchCardManager CardManager;
    public EraseProgress EraseProgress;
    
    private Action<TypeObject> _callBack;
    private bool _isDone;

    public UIParticle vfxTo, vfxDone;
    
    private void Start()
    {
        CardManager.MainCamera = Camera.main;
    }

    public void InitData(TypeObject type)
    {
        _isDone = false;
        EraseProgress.OnProgress -= DoneFillColor;
        EraseProgress.OnProgress += DoneFillColor;
        colorPenController.StartInGame();

        foreach (var image in imagesFill)
        {
            image.SetActive(false);
        }
        imagesFill[(int)type].SetActive(true);
        CardManager.ImageCard = imageNotDones[(int)type].gameObject;
        CardManager.ScratchSurfaceSprite = imageNotDones[(int)type].sprite;
        CardManager.InitData();
        CardManager.EraseTextureScale = Vector2.one * 2f;
        _typeObject = type;
        content.localScale = Vector3.zero;
        content.DOScale(Vector3.one, 0.5f).OnComplete(() => {
            colorPenController.isReady = true;
        });
        CardManager.Card.InputEnabled = false;
    }

    public void InitData(TypeObject type, Action<TypeObject> callback)
    {
        EraseProgress.OnProgress -= DoneFillColor2;
        EraseProgress.OnProgress += DoneFillColor2;
        colorPenController.StartInGame();

        _isDone = false;
        _callBack = callback;

        foreach (var image in imagesFill)
        {
            image.SetActive(false);
        }
        imagesFill[(int)type].SetActive(true);
        CardManager.ImageCard = imageNotDones[(int)type].gameObject;
        CardManager.ScratchSurfaceSprite = imageNotDones[(int)type].sprite;
        CardManager.InitData();
        CardManager.EraseTextureScale = Vector2.one * 2f;
        _typeObject = type;

        content.localScale = Vector3.zero;

        content.DOScale(Vector3.one, 0.5f).OnComplete(() => {
            colorPenController.isReady = true;
        });
        CardManager.Card.InputEnabled = false;
    }
    
    private async void DoneFillColor(float progress)
    {
        if(progress >= percentToDone && !_isDone)
        {
            _isDone = true;
            AudioUtility.StopSFX();
            AudioUtility.PlaySFX(AudioClipName.Clearstep);
            vfxDone.Play();
            imageNotDones[(int)_typeObject].gameObject.SetActive(false);
            imageDone[(int)_typeObject].gameObject.SetActive(true);
            vfxTo.gameObject.SetActive(false);
            colorPenController.StartInGame();
            await AsyncService.Delay(1f, this);
            transform.DOScale(Vector3.one, 0.5f).OnComplete(() => {
                transform.localScale = Vector3.one;
                EventManager.SendSimpleEvent(Events.FillColorDone);
                this.gameObject.SetActive(false);
            });
        }
        else if(progress > 0)
        {
            if (imageNotDoneFakes[(int)_typeObject].gameObject.activeSelf)
            {
                imageNotDoneFakes[(int)_typeObject].gameObject.SetActive(false);
            };

            if (!colorPenController.isPlaySound && colorPenController.IsPlay)
            {
                AudioUtility.PlaySFX(AudioClipName.Crayon, true);
                colorPenController.isPlaySound = true;
                vfxTo.gameObject.SetActive(true);
            }

        }
    }
    
    private void DoneFillColor2(float progress)
    {
        if (progress > 0)
        {
            if (imageNotDoneFakes[(int)_typeObject].gameObject.activeSelf)
            {
                imageNotDoneFakes[(int)_typeObject].gameObject.SetActive(false);
            };
            if (!colorPenController.isPlaySound && colorPenController.IsPlay)
            {
                AudioUtility.PlaySFX(AudioClipName.Crayon, true);
                colorPenController.isPlaySound = true;
                vfxTo.gameObject.SetActive(true);
            }
        }

        if (progress >= percentToDone && !_isDone)
        {
            ShowImage();
        }
    }

    private async void ShowImage()
    {
        _isDone = true;
        AudioUtility.StopSFX();
        AudioUtility.PlaySFX(AudioClipName.Clearstep);
        vfxDone.Play();
        var index = (int)_typeObject;
        imageNotDones[index].gameObject.SetActive(false);
        imageDone[index].gameObject.SetActive(true);
        vfxTo.gameObject.SetActive(false);
        colorPenController.StartInGame();
        await AsyncService.Delay(1f, this);
        content.DOScale(Vector3.one, 1f).OnComplete(() => {
            _callBack?.Invoke(_typeObject);
            gameObject.SetActive(false);
        });
        
    }
}
