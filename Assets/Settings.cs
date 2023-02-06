using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject sensSlider;
    [SerializeField] GameObject soundSlider;
    [SerializeField] GameObject player;
    float volumeValue, sensitivityValue;
    // Start is called before the first frame update
    void Start()
    {
        sensSlider.GetComponent<Slider>().value = player.GetComponent<Movement>().mouseSensivity;

        soundSlider.GetComponent<Slider>().value = player.GetComponent<AudioMixerHolder>().defaultVolume;
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
        volumeValue = soundSlider.GetComponent<Slider>().value;
    }

    private void ChangeSensitivity()
    {
        player.GetComponent<Movement>().mouseSensivity = sensSlider.GetComponent<Slider>().value;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("VolumeSetting", volumeValue);
    }

}
