using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    public GameObject UIManager;
    public GameObject ResultPanel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Finish!!");


            UIManager.GetComponent<Level>().levelFinished = true;

            SaveManager.Save("GhostReplays", "test", UIManager.GetComponent<Level>().saveCoords);

            UIManager.GetComponent<UI>().TogglePause();
            ResultPanel.SetActive(true);
            ResultPanel.GetComponentInChildren<TextMeshProUGUI>().text = UIManager.GetComponent<Level>().CheckMedal();



        }
    }
}
