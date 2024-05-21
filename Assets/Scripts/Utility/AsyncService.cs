using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utility
{
    public static class AsyncService
    {
        public static async UniTask Delay(float delayTime, MonoBehaviour monoBehaviour, bool canCheckDisable = true)
        {
            if (canCheckDisable)
            {
                // Tạo CancellationTokenSource
                var cts = new CancellationTokenSource();
                var cancellationToken = cts.Token;
                // Hủy CancellationToken khi đối tượng bị destroy hoặc disable
                monoBehaviour.GetCancellationTokenOnDestroy().RegisterWithoutCaptureExecutionContext(() => cts.Cancel());

                // Hủy CancellationToken khi đối tượng bị disable
                monoBehaviour.gameObject.AddComponent<MonoBehaviourDisableHandler>().Initialize(cts);

                try
                {
                    // Đợi delayTime giây hoặc hủy nếu đối tượng bị hủy hoặc disable
                    await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: cancellationToken);
                }
                catch (OperationCanceledException)
                {
                }
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: monoBehaviour.GetCancellationTokenOnDestroy());
            }

        }
    }
}
public class MonoBehaviourDisableHandler : MonoBehaviour
{
    private CancellationTokenSource cts;

    public void Initialize(CancellationTokenSource cts)
    {
        this.cts = cts;
    }

    private void OnDisable()
    {
        cts?.Cancel();
    }
}