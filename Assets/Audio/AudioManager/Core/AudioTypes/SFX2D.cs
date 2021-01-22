using UnityEngine.Audio;
using UnityEngine;

namespace Ruinarc
{
    public class SFX2D : AudioObject
    {
        public SFX2D(AudioClip clip, Transform audioCarrier, AudioMixerGroup audioMixerGroup) : base(clip, audioCarrier, audioMixerGroup)
        {
            
        }
    }
}
