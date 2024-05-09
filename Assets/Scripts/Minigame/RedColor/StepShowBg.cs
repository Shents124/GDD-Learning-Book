using System;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using UI;
using UnityEngine;

public class StepShowBg : BaseStep
{
    [SpineAnimation]
    public string animTalk;

    [SerializeField] private SkeletonGraphic animPlayer;
    [SerializeField] private RectTransform m_RectTransform;

    private Vector2 posStart;

    [SerializeField] private RectTransform[] pageGbj;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    void SetUpPage()
    {
        foreach (var page in pageGbj)
        {
            page.anchoredPosition
                += new Vector2(m_RectTransform.rect.width * (Array.IndexOf(pageGbj, page) - 1), 0);/*transform.position + new Vector3(Screen.width, 0, 0) * (Array.IndexOf(pageGbj, page) -1);*/
        }
    }

    public override void InActive()
    {
        base.InActive();
        SetUpPage();
        posStart = transform.position;
        m_RectTransform.DOAnchorPosX(m_RectTransform.rect.width, 1.5f).OnComplete(() => {
            AudioUtility.PlaySFX(AudioClipName.Red_harvest);
            var track = animPlayer.AnimationState.SetAnimation(0, animTalk, false);
            track.Complete += entry => {
                UIService.PlayFadeIn(() =>
                {
                    NextStep();
                    UIService.PlayFadeOut();
                    transform.position = posStart;
                });
            };
        }).SetDelay(1f);
    }
}
