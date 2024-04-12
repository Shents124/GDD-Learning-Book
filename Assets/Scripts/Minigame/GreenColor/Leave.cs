using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System;

public class Leave : MonoBehaviour
{
    public Transform posAnimStart;
    public Transform posAnimDone;
    public Transform posFrog;

    [SerializeField] private Transform leaveDone;

    [SerializeField] private Transform leaveFake;
    public async UniTask ShowDone()
    {
        leaveFake.gameObject.SetActive(false);
        leaveDone.gameObject.SetActive(true);
        leaveDone.position = posAnimStart.position;
        leaveDone.DOMove(posAnimDone.position, 0.75f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
    }
}
