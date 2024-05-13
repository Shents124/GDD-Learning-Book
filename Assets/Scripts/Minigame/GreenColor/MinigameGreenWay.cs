using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using Tracking;
using Tracking.Constant;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class MinigameGreenWay : MonoBehaviour
{
    public Button btnNext, btnBack;

    public float jumpPower = 100f;

    public Transform xIcon;

    [SpineAnimation]
    public string animIdle, animJump, animWin;

    public SkeletonAnimation animFrog;

    public Transform playerPos;

    public int numberTurn;

    private int _currentTurn = 0;

    public Land[] lands;

    public Transform[] landTransRandom;

    public MoveFollowLine playerController;

    private bool isDone = false;

    private Vector3 posPlayerStart;

    private Vector3 posAnimStart;

    [SerializeField] private Transform posJumDone;
    [SerializeField] private ShowTalkStartGame showTalk;

    private bool _isMoveStep;
    
    private void Start()
    {
        btnBack.onClick.AddListener(OnClickedBackBtn);
        btnNext.onClick.AddListener(OnClickedNextBtn);
        posPlayerStart = playerPos.position;
        posAnimStart = animFrog.transform.localPosition;
        showTalk.ShowTalk();
        StartGame();
        EventManager.Connect(Events.ErrorWay, OnErrorWay);
        EventManager.Connect(Events.CurrentWay, OnMoveDone);
        EventManager.Connect(Events.MoveWayDone, CompleteTurn);
    }
    private async void OnClickedBackBtn()
    {
        AudioUtility.StopSFX();
        await UIService.OpenActivityAsyncNoClose(ActivityType.MenuScreen);
        UIService.PlayFadeOut();
        Destroy(this.gameObject);
    }

    private void OnClickedNextBtn()
    {
        if (_isMoveStep)
            return;
        AudioUtility.StopSFX();
        AdsManager.Instance.ShowInterstitial((result) => {
            
            AdTracker.LogAdInter(GetTrackingAdInter(), result);
            _isMoveStep = true;
            UIService.PlayFadeIn(() => {
                UIService.OpenActivityAsyncNoClose(ActivityType.Step7Green).Forget();
                Destroy(this.gameObject);
                UIService.PlayFadeOut();
            });
        });


    }
    private void StartGame()
    {
        isDone = false;
        playerController.StartDraw();
        animFrog.transform.localPosition = posAnimStart;
        playerPos.position = posPlayerStart;
        RandomPosLand();
    }

    private void CompleteTurn()
    {
        _currentTurn++;
        animFrog.AnimationState.SetAnimation(0, animJump, false);
        animFrog.transform.DOMove(posJumDone.position, 1.34f).OnComplete(async () => {
            animFrog.AnimationState.SetAnimation(0, animWin, true);
            AudioUtility.PlaySFX(AudioClipName.Frog_laugh, true);
            await AsyncService.Delay(1.5f, this);
            AudioUtility.StopSFX();
            if (_currentTurn >= numberTurn)
            {
                if (_isMoveStep)
                    return;
                
                AdsManager.Instance.ShowInterstitial((result) => {
                    AdTracker.LogAdInter(GetTrackingAdInter(), result);
                    _isMoveStep = true;
                    UIService.PlayFadeIn(() => {
                        UIService.OpenActivityAsyncNoClose(ActivityType.Step7Green).Forget();
                        Destroy(this.gameObject);
                        UIService.PlayFadeOut();
                    });
                });

            }
            else
            {
                UIService.PlayFadeIn(() => {
                    playerController.StopDraw();
                    StartGame();
                    animFrog.AnimationState.SetAnimation(0, animIdle, true);
                    UIService.PlayFadeOut();
                });
            }
        });
    }

    private void RandomPosLand()
    {
        List<int> listRan = new List<int>();
        int i = 0;
        while(listRan.Count < landTransRandom.Length)
        {
            int ran = Random.Range(0, landTransRandom.Length);
            while (listRan.Contains(ran))
            {
                ran = Random.Range(0, landTransRandom.Length);
            }
            lands[i].transform.position = landTransRandom[ran].position;
            listRan.Add(ran);
            i++;
        }
    }

    public void OnErrorWay()
    {
        if (!playerController.isDrawing || isDone)
            return;
        playerController.StopDraw();
        ShowErrorWay();
        AudioUtility.PlaySFX(AudioClipName.Fail);
    }

    private void ShowErrorWay()
    {
        xIcon.gameObject.SetActive(true);
        xIcon.position = playerController.wayControl.getPoints().Last();
        xIcon.localScale = Vector3.zero;
        xIcon.DOScale(1, 0.75f).OnComplete(() => {
            xIcon.DOScale(0, 0.75f).OnComplete(() => {
                playerController.StartDraw();
                xIcon.gameObject.SetActive(false);
                playerController.wayControl.Init();
            });
        });
    }

    private void OnMoveDone()
    {
        if (!playerController.isDrawing || isDone)
            return;
        isDone = true;
        AudioUtility.PlaySFX(AudioClipName.Correct);
        playerController.StartMove();
    }
    
    private static TrackingAdInter GetTrackingAdInter()
    {
        return new TrackingAdInter {
            hasData = true,
            adLocation = AdLocation.end,
            levelName = LevelName.green,
            miniGameSession = "3",
            isWoaAd = false
        };
    }
}
