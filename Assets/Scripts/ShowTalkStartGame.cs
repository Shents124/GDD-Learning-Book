using System;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using UnityEngine;
using Utility;

public class ShowTalkStartGame : MonoBehaviour
{
    public Transform posFall;
    public Transform posStart;

    public AudioClipName audioTalk;

    public SkeletonGraphic animPlayer;

    [SpineAnimation(dataField: "animPlayer")]
    public string animPlayerTalk, animPlayerIdle;

    public void ShowTalk(Action callback = null)
    {
        animPlayer.transform.DOLocalMoveY(posStart.localPosition.y, 1).OnComplete(() => {
            var track = animPlayer.AnimationState.SetAnimation(0, animPlayerTalk, true);
            AudioUtility.PlaySFX(this, audioTalk, 0, () => {
                animPlayer.AnimationState.SetAnimation(0, animPlayerIdle, true);
                animPlayer.transform.DOLocalMoveY(posFall.localPosition.y, 1).OnComplete(() => {
                    gameObject.SetActive(false);
                    callback?.Invoke();
                });
            });
        });
    }
}
