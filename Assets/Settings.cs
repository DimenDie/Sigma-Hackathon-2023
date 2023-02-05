using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject sensSlider;
    [SerializeField] GameObject soundSlider;
    [SerializeField] GameObject player;
    float volumeValue;
    // Start is called before the first frame update
    void Start()
    {
        sensSlider.GetComponent<Slider>().value = player.GetComponent<Movement>().mouseSensivity;

        player.GetComponent<AudioMixerHolder>().audioMixer.GetFloat("MasterVolume", out volumeValue);
        soundSlider.GetComponent<Slider>().value = volumeValue;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSensitivity();
        ChangeVolume();
    }

    private void ChangeVolume()
    {
        player.GetComponent<AudioMixerHolder>().audioMixer.SetFloat("MasterVolume", soundSlider.GetComponent<Slider>().value);
    }

    private void ChangeSensitivity()
    {
        player.GetComponent<Movement>().mouseSensivity = sensSlider.GetComponent<Slider>().value;
    }
}
