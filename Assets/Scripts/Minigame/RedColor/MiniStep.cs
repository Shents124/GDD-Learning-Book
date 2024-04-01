using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MiniStep : MonoBehaviour
{
    public virtual async UniTask OnDone(float timeDelay)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(timeDelay));
    }
}