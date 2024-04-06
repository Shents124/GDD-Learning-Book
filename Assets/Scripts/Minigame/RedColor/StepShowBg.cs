using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UI;
using UnityEngine;

public class StepShowBg : BaseStep
{

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
            UIService.PlayFadeIn(()=> 
            { 
                NextStep();
                UIService.PlayFadeOut();
                transform.position = posStart;
            });
        }).SetDelay(2f);
    }
}
