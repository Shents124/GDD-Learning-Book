using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class AnimalFlyRandom : MonoBehaviour
{
    public RectTransform areaRect;

    public float speedFly;

    private bool isFly = true;

    private void Start()
    {
        isFly = true;
        MoveRandom();
    }

    public void CancelFly()
    {
        transform.DOKill();
        isFly = false;
    }

    private Vector2 GetRandomPositionInUI(RectTransform areaRect)
    {
        Vector2 min = areaRect.rect.min;
        Vector2 max = areaRect.rect.max;

        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);

        return new Vector2(randomX, randomY);
    }

    private void MoveRandom()
    {
        if (!isFly)
            return;
        Vector2 randomPosition = GetRandomPositionInUI(areaRect);
        if(randomPosition.x - transform.position.x > 0)
        {
            GetComponent<SkeletonGraphic>().initialFlipX = true;
        }
        else
        {
            GetComponent<SkeletonGraphic>().initialFlipX = false;
        }

        float dis = Vector2.Distance(transform.position, randomPosition);
        transform.DOLocalMove(randomPosition, dis / speedFly).OnComplete(() => {
            MoveRandom();
        });
    }
}
