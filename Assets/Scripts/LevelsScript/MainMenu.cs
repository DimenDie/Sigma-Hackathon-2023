using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [HideInInspector] public string sceneName;
    [HideInInspector] public string folderName;
    public void ContinueGame()
    {
        SceneManager.LoadScene(1); //Should change later to other index, maybe some kind of save
    }
    public void ExitGame()
    {
        Application.Quit();
    } 
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public bool CheckSave(int previousIndex)
    {
        folderName = "GhostReplays";
        string path = SceneUtility.GetScenePathByBuildIndex(previousIndex);
        sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1); //Stupid Unity bug
        bool previousSaveExists = File.Exists(Application.dataPath + $"/{folderName}/" + sceneName + ".json");

        return previousSaveExists;
    }
}
