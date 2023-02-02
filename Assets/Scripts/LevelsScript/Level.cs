using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    public float stopwatch;

    public float bunny;
    public float cheetah;
    public float lightning;

    public GameObject playerPrefab;
    public GameObject ghostPrefab;
    GameObject levelStart;
    GameObject levelEnd;
    public GameObject playerObj;
    public GameObject ghostObj;

    public bool levelFinished;

    private string saveName;
    private string saveFolderName;
    public bool saveExists;

    [HideInInspector] public SaveInfo infoToSave; //coords to write into a save file of ghost replay
    public SaveInfo infoToLoad;

    

    private void Start()
    {
        
        saveFolderName = "GhostReplays";

        levelStart = GameObject.Find("LevelStart"); //Call the police
        levelEnd = GameObject.Find("LevelEnd");

        playerObj = Instantiate(playerPrefab, levelStart.transform.position, levelStart.transform.rotation);

        saveExists = File.Exists(Application.dataPath + $"/{saveFolderName}/" + SceneManager.GetActiveScene().name + ".json");

        if (saveExists)
        {
            ghostObj = Instantiate(ghostPrefab, levelStart.transform.position, levelStart.transform.rotation);
            infoToLoad.ghostSavePositions = SaveManager.Load<SaveInfo>("GhostReplays", SceneManager.GetActiveScene().name).ghostSavePositions;
            infoToLoad.ghostSaveRotations = SaveManager.Load<SaveInfo>("GhostReplays", SceneManager.GetActiveScene().name).ghostSaveRotations;
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
        if (index < infoToLoad.ghostSavePositions.Count && saveExists)
        {
            ghostObj.transform.position = infoToLoad.ghostSavePositions[index];
            ghostObj.transform.rotation = infoToLoad.ghostSaveRotations[index];
            index++;
        }

        if (!levelFinished)
        {
            infoToSave.ghostSavePositions.Add(playerObj.transform.GetChild(0).position);
            infoToSave.ghostSaveRotations.Add(playerObj.transform.GetChild(0).rotation);
        }
    }

    public string CheckMedal()
    {
        stopwatch = Time.timeSinceLevelLoad;

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
public struct SaveInfo
{
    public string name;
    public float record;
    public string medal;
    public List<Vector3> ghostSavePositions;
    public List<Quaternion> ghostSaveRotations;
}
