using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPenController : MonoBehaviour
{
    private Vector3 mousePosition;

    [SerializeField] private Rect bounds; // Giới hạn vị trí của bút trong hình chữ nhật

    [NonSerialized] public bool isPlay = false;
    private void Update()
    {
        // Lấy vị trí của chuột trong không gian thế giới 2D

        // Giới hạn vị trí của chuột trong hình chữ nhật
        //mousePosition.x = Mathf.Clamp(mousePosition.x, bounds.xMin, bounds.xMax);
        //mousePosition.y = Mathf.Clamp(mousePosition.y, bounds.yMin, bounds.yMax);
        if(isPlay)
            transform.position = Input.mousePosition;
    }
}
