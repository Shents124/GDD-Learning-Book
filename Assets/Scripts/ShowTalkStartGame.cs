using System;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using Utility;

public class ShowTalkStartGame : MonoBehaviour
{
    public Transform posFall;

    public SkeletonGraphic animPlayer;

    [SpineAnimation(dataField: "animPlayer")]
    public string animPlayerTalk, animPlayerIdle;

    public async void ShowTalk(Action callback)
    {
        await AsyncService.Delay(1, this);
        var track = animPlayer.AnimationState.SetAnimation(0, animPlayerTalk, false);
        track.Complete += Entry => {
            animPlayer.AnimationState.SetAnimation(0, animPlayerIdle, true);
            animPlayer.transform.DOLocalMoveY(posFall.localPosition.y, 1).OnComplete(() => {
                gameObject.SetActive(false);
                callback?.Invoke();
            });
        };
    }
}
