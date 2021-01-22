using UnityEngine;

namespace Ruinarc
{
    public class SoundPlayer : MonoBehaviour
    {
        public void PlaySound(AudioClip clip)
        {
            AudioManager.Instance.Play(clip, AudioObjectType.SFX2D);
        }

        public void PlaySoundWithRandomPitch(AudioClip clip)
        {
            AudioManager.Instance.Play(clip, AudioObjectType.SFX2D, randomPitch: true);
        }
    }
}
