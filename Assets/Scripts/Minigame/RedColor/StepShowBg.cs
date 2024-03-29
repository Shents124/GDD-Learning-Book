using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StepShowBg : BaseStep
{
    private Vector2 posStart;

    public override void InActive()
    {
        base.InActive();
        posStart = transform.position;
        GetComponent<RectTransform>().DOAnchorPosX(GetComponent<RectTransform>().rect.x + Screen.width, 1.5f).OnComplete(() => {
            NextStep();
            transform.position = posStart;
        }).SetDelay(2f);
    }
}
