using Cysharp.Threading.Tasks.Triggers;
using Spine.Unity;
using UnityEngine;

public class MinigameGreenWay : MonoBehaviour
{
    public Transform xIcon;

    [SpineAnimation]
    public string animIdle, animJump, animWin;

    public SkeletonAnimation animFrog;

    public int numberTurn;

    private int _currentTurn;

    public Land[] lands;

    public Transform[] landTransRandom;

    public Transform[] rockTransRandom;

    public Rock[] rocks;

    public MoveFollowLine playerController;

    private bool isDrawing = false, isDone = false;

    private void Start()
    {
        playerController.StartDraw();
        EventManager.Connect(Events.ErrorWay, OnErrorWay);
    }

    public void OnErrorWay()
    {
        if (!isDrawing)
            return;
        playerController.StopDraw();
        ShowErrorWay();
    }

    private void ShowErrorWay()
    {

    }

    private void ShowCorrectWay()
    {

    }

    private void OnMoveDone()
    {

    }
}
