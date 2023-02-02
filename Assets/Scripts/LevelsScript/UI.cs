using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] private GameObject HUDMedal;
    [SerializeField] private GameObject HUDSlider;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);

        HUDMedal.GetComponent<TextMeshProUGUI>().text = this.GetComponent<Level>().CheckMedal();

        Debug.Log(HUDSlider.GetComponent<Slider>().value);

        if (this.GetComponent<Level>().stopwatch < this.GetComponent<Level>().lightning)
            HUDSlider.GetComponent<Slider>().value = (this.GetComponent<Level>().lightning - this.GetComponent<Level>().stopwatch) / this.GetComponent<Level>().lightning;
        else if (this.GetComponent<Level>().stopwatch < this.GetComponent<Level>().cheetah)
            HUDSlider.GetComponent<Slider>().value = (this.GetComponent<Level>().cheetah - this.GetComponent<Level>().lightning - this.GetComponent<Level>().stopwatch) / this.GetComponent<Level>().cheetah;
        else if (this.GetComponent<Level>().stopwatch < this.GetComponent<Level>().bunny)
            HUDSlider.GetComponent<Slider>().value = (this.GetComponent<Level>().bunny - this.GetComponent<Level>().stopwatch - this.GetComponent<Level>().lightning - this.GetComponent<Level>().cheetah) / this.GetComponent<Level>().bunny;
        else
            HUDSlider.SetActive(false);


        if (Input.GetKeyDown(KeyCode.B)) //Change to escape later
        {
            TogglePause();
        }

    }

    public void TogglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !Cursor.visible;

        ToggleTime();

    }

    public void ToggleTime()
    {
        if (Time.timeScale == 1.0f)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }
}
