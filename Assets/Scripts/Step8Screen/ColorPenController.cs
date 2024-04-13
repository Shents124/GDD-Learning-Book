using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPenController : MonoBehaviour
{
    private Vector3 mousePosition;

    private Vector3 posStart;

    [SerializeField] private Rect bounds; // Giới hạn vị trí của bút trong hình chữ nhật

    public bool isPlay = false;

    private void Awake()
    {
        posStart = transform.position;   
    }

    private void OnEnable()
    {
        transform.position = posStart;
    }

    private void Update()
    {
        if (isPlay)
        {
            transform.position = Input.mousePosition;
        }

    }
}
