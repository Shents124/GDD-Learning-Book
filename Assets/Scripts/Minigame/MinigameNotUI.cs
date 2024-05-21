using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameNotUI : MonoBehaviour
{
    private const float ScreenHeight = 1080f;
    private const float ScreenWidth = 1920f;

    protected virtual void Start()
    {
        Vector2 scale = transform.localScale;
        transform.localScale = scale * ((float)Screen.width / Screen.height) * (ScreenHeight / ScreenWidth);
    }
}
