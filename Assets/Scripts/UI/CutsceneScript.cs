using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneScript : MonoBehaviour
{
    [SerializeField] int intOfSceneToLoad;
    [SerializeField] VideoPlayer vPlayer;
    double previousClockTime, currentClockTime;

    void Start()
    {
        vPlayer.Play();
        previousClockTime = -1;
        currentClockTime = -1;
    }

    void Update()
    {
        currentClockTime = vPlayer.clockTime;
        if (vPlayer.clockTime > 0 && (currentClockTime == previousClockTime || Input.GetKeyDown(KeyCode.Space)))
        {
            SceneManager.LoadScene(intOfSceneToLoad);
        }
        previousClockTime = currentClockTime;
    }
}
