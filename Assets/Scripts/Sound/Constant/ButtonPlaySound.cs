using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlaySound : MonoBehaviour
{
    public AudioClipName sound;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        AudioUtility.PlaySFX(sound);
    }
}
