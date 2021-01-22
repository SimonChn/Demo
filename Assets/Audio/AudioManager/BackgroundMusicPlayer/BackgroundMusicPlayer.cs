using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Ruinarc
{
    public class BackgroundMusicPlayer : MonoBehaviour, IAudioManagerAskable
    {
        public static AudioObject currentBackgroundMusic;
        private static int currentTrackIndex = 0;
        public bool changeTrackWhenSwitchScene = true;
        public bool playFromStart = true;

        public float musicFadeTime = 1;

        public List<AudioClip> backgroundMusicTracks;

        private Action currentAction;

        private void Start()
        {
            if (!playFromStart)
            {
                return;
            }

            Play();

            if (changeTrackWhenSwitchScene)
            {
                currentTrackIndex = 0;
                SwitchTrack();
                return;
            }
        }

        private void Update()
        {
            currentAction();
        }

        public void NextTrack()
        {
            currentTrackIndex = (currentTrackIndex + 1 < backgroundMusicTracks.Count) ? (currentTrackIndex + 1) : 0;
            SwitchTrack();
        }

        public void PrevTrack()
        {
            currentTrackIndex = (currentTrackIndex > 0) ? (currentTrackIndex - 1) : backgroundMusicTracks.Count - 1;
            SwitchTrack();
        }

        public void SwitchTrack()
        {
            currentBackgroundMusic.DeleteSelf();
            currentBackgroundMusic = AudioManager.Instance.Play(backgroundMusicTracks[currentTrackIndex], AudioObjectType.Music);
        }

        public void FadeMusic()
        {
            AudioManager.Instance.audioManagerFader.FadeAudioFromTo(currentBackgroundMusic, currentBackgroundMusic.audioSourse.volume, 0, musicFadeTime, this);
        }

        public void OnSceneChanged()
        {
            FadeMusic();
            currentAction = delegate { };
        }

        private void UpdatePlayerTrack()
        {
            if (currentBackgroundMusic == null)
            {

            }
            else
            {
                if (currentBackgroundMusic.canBeDeleted)
                {
                    NextTrack();
                }
            }
        }

        public void Play()
        {
            currentAction = UpdatePlayerTrack;

            if (backgroundMusicTracks == null) return;

            if (currentBackgroundMusic == null)
            {
                currentBackgroundMusic = AudioManager.Instance.Play(backgroundMusicTracks[currentTrackIndex], AudioObjectType.Music);
                return;
            }
        }

        public void Stop()
        {
            currentBackgroundMusic.audioSourse.Stop();
            currentBackgroundMusic = null;
        }

        public void AudioManagerHasDoneRequest()
        {
            
        }
    }

}