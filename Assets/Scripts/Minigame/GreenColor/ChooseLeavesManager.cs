using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Spine.Unity;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLeavesManager : MonoBehaviour
{
    public float jumpPower = 100f;

    [SpineAnimation]
    public string animJump, animSession, animIdle;

    public LeaveChoose currentLeave;

    public SkeletonGraphic frogAnim;

    public int currentStep;

    public int stepNeed;

    public List<Leave> leaves;

    public GameObject choseLeaves;

    public Transform choseLeavesStart;
    public Transform choseLeavesEnd;

    public Transform[] btnChoose;

    public Transform posDoneStep;

    private List<Vector2> posLeaveChooseStart = new();

    private Vector2 posDoneBg;

    private void Start()
    {
        posDoneBg = transform.position;
        foreach (Transform t in btnChoose)
        {
            posLeaveChooseStart.Add(t.position);
        }
        currentLeave.posDone = leaves[0].posAnimDone;
        choseLeaves.transform.position = choseLeavesEnd.position;
        choseLeaves.transform.DOMove(choseLeavesStart.position, 0.5f).SetDelay(2f).OnComplete(() => {
            currentLeave.StartChoose();
        });

    }

    private void PlayAnimJum()
    {
        var track = frogAnim.AnimationState.SetAnimation(0, animJump, false);
        frogAnim.transform.DOJump(leaves[currentStep].posFrog.position, jumpPower, 1, 1.34f);
        track.Complete += entry => 
        {
            frogAnim.AnimationState.SetAnimation(0, animIdle, true);
            currentStep++;
            if (currentStep >= stepNeed)
            {
                frogAnim.AnimationState.SetAnimation(0, animJump, false);
                frogAnim.transform.DOJump(posDoneStep.position, jumpPower, 1, 1.34f).OnComplete(async () => {
                    frogAnim.AnimationState.SetAnimation(0, animSession, true);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));
                    UIService.PlayFadeIn(() => {
                        AdsManager.Instance.ShowInterstitial(() => {
                            UIService.OpenActivityAsync(ActivityType.MinigameGreen2Screen, false).Forget();
                        });
                    });
                });
            }
            else
            {
                if (currentStep == 2)
                {
                    transform.DOMove(posDoneBg, 1f);
                }
                choseLeaves.SetActive(true);
                choseLeaves.transform.DOMove(choseLeavesStart.position, 0.5f).OnComplete(() => {
                    currentLeave.StartChoose();
                });
                leaves[currentStep].gameObject.SetActive(true);
                currentLeave.posDone = leaves[currentStep].posAnimDone;
            }
        };
    }

    public async void NextStep()
    {
        currentLeave.ResetLeave();
        choseLeaves.transform.DOMove(choseLeavesEnd.position, 0.5f).OnComplete(() => {
            choseLeaves.SetActive(false);
        });
        ResetCollect();
        await leaves[currentStep].ShowDone();
        PlayAnimJum();
    }

    private void ResetCollect()
    {
        for (int i = 0; i < btnChoose.Length; i++)
        {
            btnChoose[i].position = posLeaveChooseStart[i];
            btnChoose[i].gameObject.SetActive(true);
        }
    }
}
