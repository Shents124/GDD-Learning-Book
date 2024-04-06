using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BtnCollectFruit : MonoBehaviour
{
    [NonSerialized] public StepCollect managerStep;

    public float jumpower;

    public float timeJump = 0.5f;

    public bool correctFruit;

    private Button btnClick;

    private Vector2 posStart;

    [SerializeField] private DOTweenAnimation animShake;

    private void Start()
    {
        posStart = transform.position;
        btnClick = GetComponent<Button>();
        btnClick.onClick.AddListener(CheckDoneObj);
    }
    private void CheckDoneObj()
    {
        if (correctFruit)
        {
            Vector2 scale = transform.localScale;
            transform.DOScale(scale * 0.8f, timeJump);
            transform.DOJump(managerStep.posCollect[managerStep.CurrentFruit].position, jumpower, 1, timeJump).OnComplete(() => {
                managerStep.IncreaseFruit();
                this.gameObject.SetActive(false);
                transform.position = posStart;
                transform.localScale = scale;
            }).SetEase(Ease.Linear);
        }
        else
        {
            animShake.DOPlay();
        }
    }

    public void OnFall()
    {
        transform.DOMoveY(managerStep.posFall.position.y, timeJump).OnComplete(() => {
            transform.position = posStart;
            this.gameObject.SetActive(false);
        });
    }
}
