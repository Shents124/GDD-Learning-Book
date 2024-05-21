using Cysharp.Threading.Tasks;
using Tracking;
using UI;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Utility;

public class Initialize : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    private bool _isShowedNoInternet, _hadInternet;

    public float checkInterval = 5f; // Thời gian chờ giữa các lần kiểm tra (giây)

    private async void CheckReconnectInternet()
    {
        await AsyncService.Delay(checkInterval, this, false);
        if(_hadInternet)
        {
            _isShowedNoInternet = false;
            UIService.CloseActivity(ActivityType.CheckInternetActivity, true);
        }
        else
        {
            EventManager.SendSimpleEvent(Events.CheckNotInternet);
        }
    }

    private async UniTask CheckInternetConnectionRoutine()
    {
        while (true)
        {
            CheckInternetConnection();

            await AsyncService.Delay(checkInterval, this, false);
        }
    }

    private void CheckInternetConnection()
    {
        bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;
        Debug.Log("isConnected " + isConnected);
        if (!isConnected)
        {
            _hadInternet = false;
            if (!_isShowedNoInternet)
            {
                _isShowedNoInternet = true;
                UIService.OpenActivityAsyncNoClose(ActivityType.CheckInternetActivity).Forget();
            }
        }
        else
        {
            _hadInternet = true;
        }
    }
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

    private async void OnUiInitializeDone()
    {
        EventManager.Connect(Events.ReconectInternet, CheckReconnectInternet);
        UIService.InitializeFadeScreen().Forget();
        await UIService.OpenActivityAsync(ActivityType.HomeScreen);
        CheckInternetConnectionRoutine().Forget();
    }

    public void OnApplicationQuit()
    {
        ProductTracking.LogLevelEnd(ResultType.quit);
        ProductTracking.LogMiniGameEnd(ResultType.quit);
    }
}
