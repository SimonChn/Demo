using System.Collections;
using UnityEngine;

namespace Ruinarc
{
    public sealed class AudioManager : MonoBehaviour
    {
        public bool audioPause = false;

        public IAudioSettings audioSettings;
        public AudioStorage audioStorage;

        [HideInInspector]
        public AudioManagerFader audioManagerFader;

        public static GameObject audioPlayerObject;

        #region SingletonRealization

        private static AudioManager instance;
        private static object syncRoot = new System.Object();

        private AudioManager()
        {
        }

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                            audioPlayerObject = instance.gameObject;

                            instance.audioManagerFader = audioPlayerObject.AddComponent<AudioManagerFader>(); // Init components
                            instance.audioStorage = new AudioStorage();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        private void Awake()
        {
            InitAudioSettingsSOFromResources();
            if (audioSettings == null)
            {
                Debug.LogWarning("Settings don't exist, audio system will be crushed");
            }
            
            DontDestroyOnLoad(this);
        }

        #region Play

        public float minPitch = 0.95f;
        public float maxPitch = 1.05f;

        public AudioObject Play(AudioClip clip, AudioObjectType audioObjectType, Transform clipCarrier = null, bool randomPitch = false)
        {
            if (!CanPlay())
            {
                return null;
            }

            if(clipCarrier == null)
            {
                clipCarrier = audioPlayerObject.transform;              
            }

            AudioObject audioObject = AudioFabric.CreateAudioObject(clip, clipCarrier, audioObjectType);
            audioStorage.currentAudio.Add(audioObject);
          
            if (randomPitch)
            {
                audioObject.audioSourse.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            }

            audioObject.audioSourse.Play();         
            StartCoroutine(DestroyAudioObjectWhenFinish(audioObject));

            return audioObject;
        }

        #endregion

        #region Controls

        public void Pause()
        {
            audioPause = true;           
            AudioListener.pause = true;   //The most simple pause
        }
        public void UnPause()
        {
            audioPause = false;        
            AudioListener.pause = false;  //The most simple unpause
        }
        
        #endregion

        #region Utils

        public void InitAudioSettingsSOFromResources(string path = "AudioSettings")
        {
            AudioSettings audioSettingsSO = Resources.Load<AudioSettings>(path);
            audioSettingsSO.InitSettings();
            audioSettings = audioSettingsSO;
        }

        private bool CanPlay()
        {
            return audioStorage.HasPlace() ? true : false; 
        }

        #endregion

        #region Cleaner

        IEnumerator DestroyAudioObjectWhenFinish(AudioObject audioObject)
        {
            while (true)
            {
                yield return null;

                if (audioPause)
                {
                    continue;
                }

                if (audioObject.canBeDeleted)
                {
                    break;
                }

                if (audioObject.audioSourse == null)
                {
                    break;
                }

                if (audioObject.audioSourse.time >= audioObject.clip.length && !audioObject.audioSourse.loop)
                {
                    break;
                }
            }

            audioObject.Clear();
            audioStorage.currentAudio.Remove(audioObject);
        }

        #endregion
    }
}