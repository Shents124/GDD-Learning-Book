using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PeelApple : MonoBehaviour
{
    public float timeAnimFake = 0.5f;
    public GameObject donePeel, fakeApple, lineCut;

    public Transform posFakeMoveDone;

    private Vector2 _posStartFake, _rotStartFake;

    private void Start()
    {
        _posStartFake = fakeApple.transform.position;
        _rotStartFake = fakeApple.transform.rotation.eulerAngles;
    }


    public async UniTask OnDonePeel()
    {
        fakeApple.SetActive(true);
        donePeel.SetActive(true);
        lineCut.SetActive(false);
        fakeApple.transform.DOLocalRotate(Vector2.zero, timeAnimFake).SetEase(Ease.Linear);
        fakeApple.transform.DOMove(posFakeMoveDone.position, timeAnimFake).OnComplete(() =>
        {
            fakeApple.GetComponent<SpriteRenderer>().DOFade(0, timeAnimFake).SetEase(Ease.Linear).OnComplete(() => {
                this.donePeel.SetActive(false);
            });
        }).SetEase(Ease.Linear);
        await UniTask.Delay(TimeSpan.FromSeconds(timeAnimFake * 2));
    }

    public void ResetStep()
    {
        this.gameObject.SetActive(false);
        donePeel.SetActive(false);
        lineCut.SetActive(true);
        fakeApple.SetActive(false);
        fakeApple.transform.position = _posStartFake;
        fakeApple.transform.rotation = Quaternion.Euler(_rotStartFake);
    }
}