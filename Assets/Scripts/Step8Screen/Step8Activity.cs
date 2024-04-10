using DG.Tweening;
using ScratchCardAsset;
using UI;
using UnityEngine;
using UnityEngine.UI;

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

    public DOTweenAnimation animBoard;

    public ScratchCardManager CardManager;
    public EraseProgress EraseProgress;


    private void Start()
    {
        CardManager.MainCamera = Camera.main;
        EraseProgress.OnProgress += DoneFillColor;
    }

    public void InitData(TypeObject type)
    {
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
