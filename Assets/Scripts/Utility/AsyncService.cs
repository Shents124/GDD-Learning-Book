using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utility
{
    public static class AsyncService
    {
        public static async UniTask Delay(float timeDelay, MonoBehaviour monoBehaviour)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(timeDelay),
                cancellationToken: monoBehaviour.GetCancellationTokenOnDestroy());
        }
    }
}