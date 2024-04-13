using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeaveChoose : MonoBehaviour
{
    public ChooseLeavesManager manager;
    public Transform posDone;

    public Vector2 scaleClick;

    private bool isClick = false;
    private bool isStarted = false;

    private Vector2 posStart;
    private Vector2 scaleStart;

    private void Start()
    {
        posStart = transform.position;   
        scaleStart = transform.localScale;
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
            manager.NextStep();
        }
        else
        {
            isClick = false;
            transform.position = posStart;
            transform.localScale = scaleStart;
        }
    }
}
