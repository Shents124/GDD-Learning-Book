using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotEndGame : MonoBehaviour
{
    public int captureWidth = 1920;
    public int captureHeight = 1080;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            CaptureImage();
        }    
    }

    public void CaptureImage()
    {
        StartCoroutine(CaptureUIRoutine());
    }

    private IEnumerator CaptureUIRoutine()
    {
        ScreenCapture.CaptureScreenshot("screenshot-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
        yield return new WaitForEndOfFrame();
    }
}
