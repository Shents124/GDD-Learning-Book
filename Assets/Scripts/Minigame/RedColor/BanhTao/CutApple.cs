using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CutApple : MonoBehaviour
{
    public float timeAnimFake = 0.5f;
    public GameObject donePeel, lineCut;

    public GameObject[] appleCutDone;

    public Transform[] posAppleCutMoveDone;

    private Vector2[] _posStart, _rotStart;

    private void Start()
    {
        _posStart = new Vector2[appleCutDone.Length];
        _rotStart = new Vector2[appleCutDone.Length];
        for (int i = 0; i < appleCutDone.Length; i++)
        {
            _posStart[i] = appleCutDone[i].transform.position;
            _rotStart[i] = appleCutDone[i].transform.rotation.eulerAngles;
        }
    }

    public async UniTask OnDonePeel()
    {
        for (int i = 0; i < appleCutDone.Length; i++)
        {
            appleCutDone[i].SetActive(true);
            appleCutDone[i].transform.DOMove(posAppleCutMoveDone[i].position, timeAnimFake);
            appleCutDone[i].transform.DORotate(posAppleCutMoveDone[i].rotation.eulerAngles, timeAnimFake);
        }
        donePeel.SetActive(true);
        lineCut.SetActive(false);
        await UniTask.Delay(TimeSpan.FromSeconds(timeAnimFake * 2));
        donePeel.SetActive(false);
    }

    public void ResetStep()
    {
        lineCut.SetActive(true);
        donePeel.SetActive(false);
        for (int i = 0; i < appleCutDone.Length; i++)
        {
            appleCutDone[i].SetActive(false);
            appleCutDone[i].transform.position = _posStart[i];
            appleCutDone[i].transform.rotation = Quaternion.Euler(_rotStart[i]);
        }
    }
}