using System;
using System.Collections;
using UnityEngine;

namespace Ruinarc
{
    public class AudioManagerFader : MonoBehaviour
    {
        private AudioManager audioManager;

        public void SetManager(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        public void FadeAudioFromTo(AudioObject audioObject, float fromVolume, float toVolume, float fadeTime, IAudioManagerAskable client)
        {
            StartCoroutine(FadeAudioFromTo(client.AudioManagerHasDoneRequest, audioObject, fromVolume, toVolume, fadeTime));
        }

        IEnumerator FadeAudioFromTo(Action notifyClient, AudioObject audioObject, float fromVolume, float toVolume, float fadeTime)
        {         
            fromVolume = Mathf.Clamp01(fromVolume);
            toVolume = Mathf.Clamp01(toVolume);
            float minVolume = Mathf.Min(fromVolume, toVolume);
            float maxVolume = Mathf.Max(fromVolume, toVolume);

            audioObject.audioSourse.volume = fromVolume;
            float step = (toVolume - fromVolume) / fadeTime;

            float prevTime = Time.realtimeSinceStartup;
            float timeCounter = 0;

            while (audioObject.audioSourse != null && audioObject.audioSourse.volume != toVolume)
            {
                yield return null;

                if (AudioManager.Instance.audioPause)
                {
                    prevTime = Time.realtimeSinceStartup;
                    continue;
                }

                timeCounter += Time.realtimeSinceStartup - prevTime;
                prevTime = Time.realtimeSinceStartup;

                float nextStepVolume = fromVolume + timeCounter * step;

                audioObject.audioSourse.volume = Mathf.Clamp(nextStepVolume, minVolume, maxVolume);
            }

            notifyClient?.Invoke();

            yield return null;
        }
    }
}
