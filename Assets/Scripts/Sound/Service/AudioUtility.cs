using System;
using Hellmade.Sound;
using UnityEngine;
using Utility;

namespace Sound.Service
{
    public static class AudioUtility
    {
        private static int s_indexGameplay = 0;
        
        private static AudioClip GetAudioClip(AudioClipName AudioClipName)
        {
            return LoadResourceService.LoadAsset<AudioClip>(AudioClipName.ToString());
        }

        public static async void PlaySFX(this MonoBehaviour monoBehaviour, AudioClipName AudioClipName, float timeDelayNext = 0, Action callback = null)
        {
            var audio = GetAudioClip(AudioClipName);
            EazySoundManager.Instance.PlaySound(audio, false);
            await AsyncService.Delay(audio.length + timeDelayNext, monoBehaviour);
            if (monoBehaviour == null || !monoBehaviour.gameObject.activeSelf)
                return;
            callback?.Invoke();
        }

        public static void PlayMusic(AudioClipName audioClipName, bool loop = true)
        {
            var audio = GetAudioClip(audioClipName);
            EazySoundManager.Instance.PlayMusic(audio, loop);
        }

        public static void PlayMusicGamePlay(bool loop = true)
        {
            if (s_indexGameplay % 2 == 0)
            {
                var gameplay1 = GetAudioClip(AudioClipName.Gameplay1);
                EazySoundManager.Instance.PlayMusic(gameplay1, loop);
            }
            else
            {
                var gameplay2 = GetAudioClip(AudioClipName.Gameplay2);
                EazySoundManager.Instance.PlayMusic(gameplay2, loop);
            }

            s_indexGameplay++;
        }
        
        public static void PlaySFX(AudioClipName AudioClipName, bool loop = false)
        {
            var audio = GetAudioClip(AudioClipName);
            EazySoundManager.Instance.PlaySound(audio, loop);
        }

        public static void StopSFX()
        {
            EazySoundManager.Instance.StopAllSounds();
        }
        
        public static void PlayUISfx(AudioClipName audioClipName)
        {
            var audio = GetAudioClip(audioClipName);
            EazySoundManager.Instance.PlayUISound(audio);
        }
    }
}
