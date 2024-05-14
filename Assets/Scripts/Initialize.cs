using Cysharp.Threading.Tasks;
using Tracking;
using UI;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        uiController.onInitializeDone += OnUiInitializeDone;
    }

    private void OnDisable()
    {
        uiController.onInitializeDone -= OnUiInitializeDone;
    }

    private static void OnUiInitializeDone()
    {
        UIService.OpenActivityAsync(ActivityType.HomeScreen).Forget();
        UIService.InitializeFadeScreen().Forget();
    }

    public void OnApplicationQuit()
    {
        ProductTracking.LogLevelEnd(ResultType.quit);
        ProductTracking.LogMiniGameEnd(ResultType.quit);
    }
}
