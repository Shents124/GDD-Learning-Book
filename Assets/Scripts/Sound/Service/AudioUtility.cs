using System;
using Hellmade.Sound;
using UnityEngine;
using Utility;

namespace Sound.Service
{
    public static class AudioUtility
    {
        private static AudioClip GetAudioClip(AudioClipName AudioClipName)
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

        public static void PlayUISfx(AudioClipName audioClipName)
        {
            var audio = GetAudioClip(audioClipName);
            EazySoundManager.Instance.PlayUISound(audio);
        }
    }
}
