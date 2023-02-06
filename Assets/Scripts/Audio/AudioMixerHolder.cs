using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerHolder : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] GameObject playerPrefab;
    public float defaultVolume;

    private void Start()
    {
        TurnSoundOn();
    }

    public void TurnSoundOff()
    {
        audioMixer.SetFloat("MasterVolume", -80);
    }

    public void TurnSoundOn()
    {
        float volume = PlayerPrefs.HasKey("VolumeSetting") ? PlayerPrefs.GetFloat("VolumeSetting") : defaultVolume;


        audioMixer.SetFloat("MasterVolume", volume);

    }



}
