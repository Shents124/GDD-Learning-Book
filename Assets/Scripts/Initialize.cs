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
        UIService.OpenActivity(ActivityType.HomeScreen, false);
    }
}