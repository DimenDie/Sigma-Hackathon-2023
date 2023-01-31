using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] private GameObject stopwatch;

    private void Start()
    {
    }
    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);

        stopwatch.GetComponent<TextMeshProUGUI>().text = string.Format("{0:D1}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

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
