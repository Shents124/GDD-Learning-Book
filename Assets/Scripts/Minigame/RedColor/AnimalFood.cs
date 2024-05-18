using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sound.Service;
using UnityEngine;

public class AnimalFood : MonoBehaviour
{
    public AudioClipName audioFall;
    public float speedFly = 200f;
    public List<Transform> posFoods;
    public Transform posFall;
    private int _idFoodMove;
    private bool isFall = false;
    [SerializeField] private bool isFallDown;

    private void Start()
    {
        _idFoodMove = -1;
        MoveToFood();
    }

    public void MoveToFood()
    {
        if (isFall)
            return;

        int id = UnityEngine.Random.Range(0, posFoods.Count);
        while(id == _idFoodMove)
        {
            id = UnityEngine.Random.Range(0, posFoods.Count);
        }
        _idFoodMove = id;

        if(posFoods[_idFoodMove].position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
        }

        float time = Vector2.Distance(transform.position, posFoods[_idFoodMove].position) / speedFly;

        transform.DOMove(posFoods[_idFoodMove].position, time).OnComplete(() => {
            MoveToFood();
        });
    }

    public void OnFall()
    {
        isFall = true;
        transform.DOKill();
        AudioUtility.PlaySFX(audioFall);
        if (isFallDown)
        {
            transform.DOMoveY(posFall.position.y, 1.5f).OnComplete(() => {
                gameObject.SetActive(false);
            });
        }
        else
        {
            if (posFall.position.x - transform.position.x > 0)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
            }

            transform.DOMoveX(posFall.position.x, 1.5f).OnComplete(() => {
                gameObject.SetActive(false);
            });
        }

    }
}
