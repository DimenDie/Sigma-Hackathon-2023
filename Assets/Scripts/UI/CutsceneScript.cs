using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneScript : MonoBehaviour
{
    [SerializeField] int intOfSceneToLoad;
    [SerializeField] VideoPlayer vPlayer;
    double videoLength;
    double timer;

    void Start()
    {
        videoLength = vPlayer.clip.length;
        vPlayer.Play();
    }

    void Update()
    {
        if (System.Convert.ToSingle(vPlayer.clockTime) >= System.Convert.ToSingle(videoLength) || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(intOfSceneToLoad);
    }
}
