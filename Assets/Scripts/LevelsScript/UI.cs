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
    [SerializeField] private GameObject resultMedal;
    [SerializeField] private GameObject HUDSlider;
    [SerializeField] private GameObject HUDSight;
    [SerializeField] private GameObject whiteScreen;

    [SerializeField] private Sprite diamondMedal;
    [SerializeField] private Sprite goldMedal;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private Sprite bronzeMedal;

    Level level;
    Slider slider;

    List<float> timeValues = new List<float>();

    [HideInInspector] public bool notDuringPause;

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

        if (this.GetComponent<Level>().CheckMedal() == "lightning") 
        {
            resultMedal.GetComponent<Image>().sprite = diamondMedal;
            HUDMedal.GetComponent<Image>().sprite = diamondMedal;
        }
        else if (this.GetComponent<Level>().CheckMedal() == "cheetah")
        {
            resultMedal.GetComponent<Image>().sprite = goldMedal;
            HUDMedal.GetComponent<Image>().sprite = goldMedal;
        }
        else if (this.GetComponent<Level>().CheckMedal() == "bunny")
        {
            resultMedal.GetComponent<Image>().sprite = silverMedal;
            HUDMedal.GetComponent<Image>().sprite = silverMedal;
        }
        else
        {
            resultMedal.GetComponent<Image>().sprite = bronzeMedal;
            HUDMedal.GetComponent<Image>().sprite = bronzeMedal;
        }

        notDuringPause = pauseMenu.activeInHierarchy;


        CheckTimeValue();


        if (Input.GetKeyDown(KeyCode.Escape) && FindObjectOfType<Death>().isDead == false && FindObjectOfType<Level>().levelFinished) //Change to escape later
        {
            TogglePause();
        }

    }

    public IEnumerator WhiteFadeOn(GameObject resultPanel)
    {
        float t = 0;
        while(t<=1)
        {
            whiteScreen.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime;
            yield return null;
        }
        whiteScreen.GetComponent<CanvasGroup>().alpha = 1;
        TogglePause();
        resultPanel.SetActive(true);
        StartCoroutine(WhiteFadeOff());
    }

    public IEnumerator WhiteFadeOff()
    {
        float t = 0;
        while (t <= 1)
        {
            whiteScreen.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, t);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        whiteScreen.GetComponent<CanvasGroup>().alpha = 0;

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
        HUDSight.SetActive(!HUDSight.activeSelf);

        if (notDuringPause)
            FindObjectOfType<AudioMixerHolder>().TurnSoundOn();
        else
            FindObjectOfType<AudioMixerHolder>().TurnSoundOff();

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
