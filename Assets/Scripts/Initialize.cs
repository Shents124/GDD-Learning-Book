using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    private void OnEnable()
    {
        uiController.onInitializeDone += OnUiInitializeDone;
    }

    private void OnDisable()
    {
        uiController.onInitializeDone -= OnUiInitializeDone;
    }

    private void OnUiInitializeDone()
    {
        UIService.OpenActivityAsync(ActivityType.HomeScreen, false).Forget();
    }
}