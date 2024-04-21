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

    public LineController lineController;

    private bool isDrawing = false, isDone = false;

    public void OnMoveToRock()
    {
        if (!isDrawing)
            return;
        lineController.Init();
    }

    public void OnMoveDone()
    {

    }
}
