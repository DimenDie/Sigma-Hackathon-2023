using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public GameObject UIManager;
    public GameObject resultPanel;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            var currentLevel = UIManager.GetComponent<Level>();

            currentLevel.levelFinished = true;

            currentLevel.infoToSave.name = SceneManager.GetActiveScene().name;
            currentLevel.infoToSave.record = currentLevel.stopwatch;
            currentLevel.infoToSave.medal = currentLevel.CheckMedal();

            if (!currentLevel.saveExists || currentLevel.stopwatch < SaveManager.Load<SaveInfo>("GhostReplays", SceneManager.GetActiveScene().name).record)
            {
                SaveManager.Save("GhostReplays", SceneManager.GetActiveScene().name, currentLevel.infoToSave);
            }

            UIManager.GetComponent<UI>().TogglePause();

            resultPanel.SetActive(true);

            //resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = UIManager.GetComponent<Level>().CheckMedal();
        }
    }
}
