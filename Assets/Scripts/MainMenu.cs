using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene(1); //Should change later to other index, maybe some kind of save
    }
    public void ExitGame()
    {
        Application.Quit();
    } 
}
