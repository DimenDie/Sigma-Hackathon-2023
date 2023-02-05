using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIconUpdater : MonoBehaviour
{
    public int sceneToUnlock;
    public int sceneToCheckMedal;
    public GameObject MainMenu;

    public Sprite locked;
    public Sprite unlocked;

    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;
    public Sprite diamond;

    public GameObject medalOfLevel;
    void Start()
    {
        this.GetComponent<Image>().sprite = locked;
        this.GetComponent<Button>().interactable = false;
        if (sceneToUnlock == 0 || MainMenu.GetComponent<MainMenu>().CheckSave(sceneToUnlock))
        {
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Image>().sprite = unlocked;
        }
        medalOfLevel.GetComponent<Image>().enabled = false;
        if (!MainMenu.GetComponent<MainMenu>().CheckSave(sceneToCheckMedal)) return;

        medalOfLevel.GetComponent<Image>().enabled = true;

        if (SaveManager.Load<SaveInfo>(MainMenu.GetComponent<MainMenu>().folderName, MainMenu.GetComponent<MainMenu>().sceneName).medal == "lightning")
            medalOfLevel.GetComponent<Image>().sprite = diamond;
        else if (SaveManager.Load<SaveInfo>(MainMenu.GetComponent<MainMenu>().folderName, MainMenu.GetComponent<MainMenu>().sceneName).medal == "cheetah")
            medalOfLevel.GetComponent<Image>().sprite = gold;
        else if (SaveManager.Load<SaveInfo>(MainMenu.GetComponent<MainMenu>().folderName, MainMenu.GetComponent<MainMenu>().sceneName).medal == "bunny")
            medalOfLevel.GetComponent<Image>().sprite = silver;
        else if (SaveManager.Load<SaveInfo>(MainMenu.GetComponent<MainMenu>().folderName, MainMenu.GetComponent<MainMenu>().sceneName).medal == "snail")
            medalOfLevel.GetComponent<Image>().sprite = bronze;


    }

}
