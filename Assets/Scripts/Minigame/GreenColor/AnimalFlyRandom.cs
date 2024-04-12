﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class AnimalFlyRandom : MonoBehaviour
{
    public bool isCurrentAnimal;

    public RectTransform areaRect;

    public float speedFly;

    private bool isFly = true;

    public Action actionClick {  get; set; }

    private void Start()
    {
        isFly = true;
        MoveRandom();
        GetComponent<Button>().onClick.AddListener(ClickAnimal);
    }

    public void CancelFly()
    {
        transform.DOKill();
        isFly = false;
    }

    private void ClickAnimal()
    {
        actionClick?.Invoke();
    }

    private Vector2 GetRandomPositionInUI(RectTransform areaRect)
    {
        Vector2 min = areaRect.rect.min;
        Vector2 max = areaRect.rect.max;

        float randomX = UnityEngine.Random.Range(min.x, max.x);
        float randomY = UnityEngine.Random.Range(min.y, max.y);

        return new Vector2(randomX, randomY);
    }

    private void MoveRandom()
    {
        if (!isFly)
            return;
        Vector2 randomPosition = GetRandomPositionInUI(areaRect);
        if(randomPosition.x - transform.localPosition.x > 0)
        {
            transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
        }

        float dis = Vector2.Distance(transform.localPosition, randomPosition);
        //transform.rotation = Vector2Extensions.LookAt2D(transform.position, randomPosition);
        transform.DOLocalMove(randomPosition, dis / speedFly).OnComplete(() => {
            MoveRandom();
        });
    }
}
