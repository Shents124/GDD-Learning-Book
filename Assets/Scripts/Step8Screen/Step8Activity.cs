using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ScratchCardAsset;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Step8Activity : MonoBehaviour
{
    public RectTransform content;
    public float percentToDone = 0.9f;
    private TypeObject _typeObject;
    public GameObject[] imagesFill;
    public Image[] imageNotDones;
    public Image[] imageDone;
    public ColorPenController colorPenController;

    public DOTweenAnimation animBoard;

    public ScratchCardManager CardManager;
    public EraseProgress EraseProgress;
    
    private Action<TypeObject> _callBack;
    private bool _isDone;

    public UIParticle vfxTo;
    
    private void Start()
    {
        CardManager.MainCamera = Camera.main;
    }

    public void InitData(TypeObject type)
    {
        EraseProgress.OnProgress -= DoneFillColor;
        EraseProgress.OnProgress += DoneFillColor;
        colorPenController.StartInGame();

        foreach (var image in imagesFill)
        {
            image.SetActive(false);
        }
        imageDone[(int)type].gameObject.SetActive(false);
        imagesFill[(int)type].SetActive(true);
        CardManager.ImageCard = imageNotDones[(int)type].gameObject;
        CardManager.ScratchSurfaceSprite = imageNotDones[(int)type].sprite;
        CardManager.InitData();
        CardManager.EraseTextureScale = Vector2.one * 2f;
        _typeObject = type;
        content.localScale = Vector3.zero;
        content.DOScale(Vector3.one, 0.5f).OnComplete(() => {
            if (imageNotDones[(int)type].material)
            {
                imageDone[(int)type].gameObject.SetActive(true);
            }
            animBoard.DORestart();
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
            if (imageNotDones[(int)type].material)
            {
                imageDone[(int)type].gameObject.SetActive(true);
            }
            animBoard.DORestart();
            colorPenController.isReady = true;
        });
        CardManager.Card.InputEnabled = false;
    }
    
    private void DoneFillColor(float progress)
    {
        if(progress >= percentToDone)
        {
            imageNotDones[(int)_typeObject].gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            EventManager.SendSimpleEvent(Events.FillColorDone);
        }
        else
        {
            if (!imageNotDones[(int)_typeObject].gameObject.activeSelf)
            {
                imageNotDones[(int)_typeObject].gameObject.SetActive(true);
            };

            if(progress * 100 % 20 == 0 && colorPenController.IsPlay)
            {
                vfxTo.Play();
                vfxTo.transform.position = colorPenController.transform.position;
            }

        }
    }
    
    private void DoneFillColor2(float progress)
    {
        if (!(progress >= percentToDone) || _isDone)
        {
            return;
        }

        StartCoroutine(ShowImage());
    }

    private IEnumerator ShowImage()
    {
        _isDone = true;
        yield return new WaitForSeconds(1f);
        var index = (int)_typeObject;
        imageNotDones[index].gameObject.SetActive(false);
        imageDone[index].gameObject.SetActive(true);
            
        content.DOScale(Vector3.one, 1f).OnComplete(() => {
            _callBack?.Invoke(_typeObject);
            gameObject.SetActive(false);
        });
        
    }
}
