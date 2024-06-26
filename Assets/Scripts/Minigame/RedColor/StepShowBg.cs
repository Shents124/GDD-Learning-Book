using System;
using DG.Tweening;
using Sound.Service;
using Spine.Unity;
using UI;
using UnityEngine;

public class StepShowBg : BaseStep
{
    [SerializeField] private SkeletonGraphic animPlayer;
    [SerializeField] private RectTransform m_RectTransform;

    [SpineAnimation(dataField: "animPlayer")]
    public string animTalk;

    private Vector2 posStart;

    public float distanceIndex = 1f;

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
                += new Vector2(m_RectTransform.rect.width * (Array.IndexOf(pageGbj, page) - 1) * distanceIndex, 0);/*transform.position + new Vector3(Screen.width, 0, 0) * (Array.IndexOf(pageGbj, page) -1);*/
        }
    }

    public override void InActive()
    {
        base.InActive();
        SetUpPage();
        posStart = transform.position;
        m_RectTransform.DOAnchorPosX(m_RectTransform.rect.width * distanceIndex, 2.5f).OnComplete(() => {
            var track = animPlayer.AnimationState.SetAnimation(0, animTalk, true);
            AudioUtility.PlaySFX(this, AudioClipName.Red_harvest, callback: () => {
                UIService.PlayFadeIn(() =>
                {
                    NextStep();
                    UIService.PlayFadeOut();
                    transform.position = posStart;
                });
            });
        }).SetDelay(1f);
    }
}
