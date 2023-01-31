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

    private void Start()
    {
    }
    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        Debug.Log(timeSpan);

        HUDMedal.GetComponent<TextMeshProUGUI>().text = this.GetComponent<Level>().CheckMedal();

        if (Input.GetKeyDown(KeyCode.B)) //Change to escape later
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        ToggleTime();

        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToggleTime()
    {
        if (Time.timeScale == 1.0f)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }
}
