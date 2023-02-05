using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerHolder : MonoBehaviour
{
    public AudioMixer audioMixer;

    float defaultVolume;
    
    private void Start()
    {
        audioMixer.GetFloat("MasterVolume", out defaultVolume);
    }

    public void TurnSoundOff()
    {
        audioMixer.SetFloat("MasterVolume", -80);
    }

    public void TurnSoundOn()
    {
        audioMixer.SetFloat("MasterVolume", defaultVolume);
    }
}
