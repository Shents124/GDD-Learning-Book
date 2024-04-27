using Utility;
using UnityEngine;
using System;
using Hellmade.Sound;
using Unity.VisualScripting;

public static class AudioUtility
{
    public static AudioClip GetAudioClip(AudioClipName AudioClipName)
    {
        return LoadResourceService.LoadAsset<AudioClip>(AudioClipName.ToString());
    }

    public static async void PlaySFX(this MonoBehaviour monoBehaviour, AudioClipName AudioClipName, Action callback = null)
    {
        var audio = GetAudioClip(AudioClipName);
        EazySoundManager.Instance.PlaySound(audio, false);
        await AsyncService.Delay(audio.length, monoBehaviour);
        callback?.Invoke();
    }

    public static void PlaySFX(AudioClipName AudioClipName)
    {
        var audio = GetAudioClip(AudioClipName);
        EazySoundManager.Instance.PlaySound(audio, false);
    }
}
