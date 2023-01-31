using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public int bunny;
    public int cheetah;
    public int lightning;

    public GameObject player;
    public GameObject levelStart;
    public GameObject levelEnd;

    private void Start()
    {
        Instantiate(player, levelStart.transform.position, levelStart.transform.rotation);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public string CheckMedal()
    {
        float stopwatch = Time.timeSinceLevelLoad;

        if (stopwatch < lightning)
            return "lightning";
        else if (stopwatch < cheetah)
            return "cheetah";
        else if (stopwatch < bunny)
            return "bunny";
        else
            return "snail";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
