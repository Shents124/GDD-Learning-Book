using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System;

public class Cake : MonoBehaviour
{
    public GameObject[] piecesDone;

    public GameObject fakeAppleDone;


    [SerializeField] private float timeMove = 1f;

    public Transform posDone;

    public void FillPieces(int currentStep)
    {
        piecesDone[currentStep].SetActive(true);
    }

    public async UniTask DoneStep()
    {
        transform.DOMove(posDone.position, timeMove).SetEase(Ease.Linear);
        transform.DOScale(posDone.localScale, timeMove).SetEase(Ease.Linear);
        fakeAppleDone.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(timeMove));
        this.gameObject.SetActive(false);
    }
}
