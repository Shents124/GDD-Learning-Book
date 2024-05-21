using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static Coroutine DelayTask(this MonoBehaviour monoBehaviour, float delayTime, System.Action action = null)
    {
        return monoBehaviour.StartCoroutine(DelayCoroutine(delayTime, action));
    }

    private static IEnumerator DelayCoroutine(float delayTime, System.Action action)
    {
        yield return new WaitForSeconds(delayTime);
        action?.Invoke();
    }
}