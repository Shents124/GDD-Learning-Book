using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Leave : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ChooseLeavesManager manager;
    public Transform posDone;
    private bool isClick = false;

    private void Update()
    {
        if (isClick)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isClick = true;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector3.Distance(transform.position, posDone.position) < 2f)
        {
            manager.NextStep();
        }
        else
        {
            isClick = false;
        }
    }
}
