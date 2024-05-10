using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sound.Service;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaveChoose : MonoBehaviour
{
    public bool isCurrentLeave;
    public ChooseLeavesManager manager;
    public Transform posDone;

    public Vector2 scaleClick;

    private bool isClick = false;
    private bool isStarted = false;

    private Vector2 posStart;
    private Vector2 scaleStart;

    private void Start()
    {
        ResetStartPos();
        scaleStart = transform.localScale;
    }

    public void ResetStartPos()
    {
        posStart = transform.position;
    }

    private void Update()
    {
        if (isClick && isStarted)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void StartChoose()
    {
        isStarted = true;
    }

    public void ResetLeave()
    {
        isClick = false;
        isStarted = false;
        transform.localScale = scaleStart;
        transform.position = posStart;
    }

    public void OnBeginDrag()
    {
        if (!isStarted)
            return;
        isClick = true;
        transform.localScale = scaleClick;
    }

    public void OnEndDrag()
    {
        if (Vector2.Distance(transform.position, posDone.position) < 50f)
        {
            if (!isCurrentLeave)
            {
                AudioUtility.PlaySFX(AudioClipName.Fail);
                isClick = false;
                transform.position = posStart;
                transform.localScale = scaleStart;
                transform.DOShakePosition(0.5f, 15, 50, 90);
            }
            else
            {
                AudioUtility.PlaySFX(AudioClipName.Correct);
                manager.NextStep();
            }
        }
        else
        {
            isClick = false;
            transform.position = posStart;
            transform.localScale = scaleStart;
        }
    }
}
