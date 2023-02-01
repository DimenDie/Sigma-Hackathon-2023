using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    public int bunny;
    public int cheetah;
    public int lightning;

    public GameObject playerPrefab;
    public GameObject ghostPrefab;
    public GameObject levelStart;
    public GameObject levelEnd;
    public GameObject playerObj;
    public GameObject ghostObj;

    public bool levelFinished;

    private string saveName;

    [HideInInspector] public SaveCoords saveCoords; //coords to write into a save file of ghost replay
    public SaveCoords loadCoords;

    

    private void Start()
    {
        playerObj = Instantiate(playerPrefab, levelStart.transform.position, levelStart.transform.rotation);
        ghostObj = Instantiate(ghostPrefab, levelStart.transform.position, levelStart.transform.rotation);

        if (index < SaveManager.Load<SaveCoords>("GhostReplays", "test").savePositions.Count)
        {
            loadCoords.savePositions = SaveManager.Load<SaveCoords>("GhostReplays", "test").savePositions;
            loadCoords.saveRotations = SaveManager.Load<SaveCoords>("GhostReplays", "test").saveRotations;
            index++;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

    }
    int index;
    private void FixedUpdate()
    {
        if (index < loadCoords.savePositions.Count)
        {
            ghostObj.transform.position = loadCoords.savePositions[index];
            ghostObj.transform.rotation = loadCoords.saveRotations[index];
            index++;
        }
        //Debug.Log(playerObj.transform.GetChild(0).position);
        if (!levelFinished)
        {
            saveCoords.savePositions.Add(playerObj.transform.GetChild(0).position);
            saveCoords.saveRotations.Add(playerObj.transform.GetChild(0).rotation);
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

[Serializable]
public struct SaveCoords
{
    public List<Vector3> savePositions;
    public List<Quaternion> saveRotations;
}
