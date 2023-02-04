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

    [SerializeField] private Sprite diamondMedal;
    [SerializeField] private Sprite goldMedal;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private Sprite bronzeMedal;

    Level level;
    Slider slider;

    List<float> timeValues = new List<float>();


    private void Start()
    {
        level = GetComponent<Level>();
        slider = HUDSlider.GetComponent<Slider>();

        Time.timeScale = 1.0f;

        timeValues.Add(level.lightning);
        timeValues.Add(level.cheetah);
        timeValues.Add(level.bunny);

    }
    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);

        if(this.GetComponent<Level>().CheckMedal() == "lightning")
            HUDMedal.GetComponent<Image>().sprite = diamondMedal;
        else if (this.GetComponent<Level>().CheckMedal() == "cheetah")
            HUDMedal.GetComponent<Image>().sprite = goldMedal;
        else if (this.GetComponent<Level>().CheckMedal() == "bunny")
            HUDMedal.GetComponent<Image>().sprite = silverMedal;
        else
            HUDMedal.GetComponent<Image>().sprite = bronzeMedal;


        CheckTimeValue();


        if (Input.GetKeyDown(KeyCode.B) && FindObjectOfType<Death>().isDead == false) //Change to escape later
        {
            TogglePause();
        }

    }


    void CheckTimeValue()
    {
        float timeValue = -1;
        int timeValueInd = -1;
        for (int i = 0; i < timeValues.Count; i++)
        {
            if(level.stopwatch <= timeValues[i])
            {
                timeValue = timeValues[i];
                timeValueInd = i;
                break;
            }
        }

        if (timeValueInd == -1) return;

        if(timeValueInd > 0)
            slider.value = Mathf.Lerp(1, 0, (level.stopwatch - timeValues[timeValueInd-1]) / (timeValue - timeValues[timeValueInd - 1]));

        else
            slider.value = Mathf.Lerp(1, 0, level.stopwatch / timeValue);
    }



    public void TogglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        HUDSlider.SetActive(!HUDSlider.activeSelf);
        HUDMedal.SetActive(!HUDMedal.activeSelf);

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
