using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<Sound> sounds = new List<Sound>();
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        Sound soundToPlay = null;
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].nameOfSound == soundName)
            {
                soundToPlay = sounds[i];
            }
        }
        if (soundToPlay == null) return;

        audioSource.clip = soundToPlay.audioClip;
        audioSource.volume = soundToPlay.volume;
        audioSource.Play();
    }
}
