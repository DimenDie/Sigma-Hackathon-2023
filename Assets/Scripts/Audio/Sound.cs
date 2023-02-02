using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string nameOfSound;
    public AudioClip audioClip;
    [Range(0.0f, 1.0f)]
    public float volume;
}
