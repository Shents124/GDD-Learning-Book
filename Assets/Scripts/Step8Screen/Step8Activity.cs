using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject penNotReady;
    public GameObject penReady;

    public DOTweenAnimation animBoard;

    public ScratchCardManager CardManager;
    public EraseProgress EraseProgress;
    
    private Action<TypeObject> _callBack;
    private bool _isDone;
    
    private void Start()
    {
        CardManager.MainCamera = Camera.main;
    }

    public void InitData(TypeObject type)
    {
        EraseProgress.OnProgress -= DoneFillColor;
        EraseProgress.OnProgress += DoneFillColor;
        
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

        penNotReady.SetActive(true);
        penReady.SetActive(false);
        Vector2 posStart = penNotReady.transform.position;
        var rotStart = penNotReady.transform.rotation;
        penNotReady.transform.DORotate(penReady.transform.rotation.eulerAngles, 1f);
        penNotReady.transform.DOMove(penReady.transform.position, 1f).OnComplete(() => {
            animBoard.DORestart();
            penNotReady.transform.position = posStart;
            penNotReady.transform.rotation = rotStart;
            colorPenController.isPlay = true;
            penNotReady.SetActive(false);
            penReady.SetActive(true);
        });
    }

    public void InitData(TypeObject type, Action<TypeObject> callback)
    {
        EraseProgress.OnProgress -= DoneFillColor2;
        EraseProgress.OnProgress += DoneFillColor2;
        
        _isDone = false;
        _callBack = callback;
        penNotReady.SetActive(false);
        penReady.SetActive(false);
        
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
            penNotReady.SetActive(true);

            Vector2 posStart = penNotReady.transform.position;
            var rotStart = penNotReady.transform.rotation;
            penNotReady.transform.DORotate(penReady.transform.rotation.eulerAngles, 1f);
            penNotReady.transform.DOMove(penReady.transform.position, 1f).OnComplete(() => {
                //animBoard.DORestart();
                penNotReady.transform.position = posStart;
                penNotReady.transform.rotation = rotStart;
                colorPenController.isPlay = true;
                penNotReady.SetActive(false);
                penReady.SetActive(true);
            });
        });
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
        yield return new WaitForSeconds(1f);
        _isDone = true;
        var index = (int)_typeObject;
        imageNotDones[index].gameObject.SetActive(false);
        imageDone[index].gameObject.SetActive(true);
            
        content.DOScale(Vector3.one, 1f).OnComplete(() => {
            _callBack?.Invoke(_typeObject);
            gameObject.SetActive(false);
        });
        
    }
}
