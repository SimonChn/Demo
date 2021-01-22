using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public interface IAudioSettings 
{
    bool MusicMuted
    {
        get;
        set;
    }

    bool SFXMuted
    {
        get;
        set;
    }

    float MasterVolume
    {
        get;
        set;
    }

    float MusicVolume
    {
        get;
        set;
    }

    float SFXVolume
    {
        get;
        set;
    }

    AudioMixer GetAudioMixer();
}
