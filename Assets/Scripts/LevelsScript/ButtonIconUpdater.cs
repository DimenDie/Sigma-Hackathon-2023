using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIconUpdater : MonoBehaviour
{
    public int sceneToUnlock;
    public GameObject MainMenu;

    public Sprite locked;
    public Sprite unlocked;

    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;
    public Sprite diamond;
    void Start()
    {
        this.GetComponent<Button>().interactable = false;
        int levelScene = sceneToUnlock + 1;
        if (sceneToUnlock == 0 || MainMenu.GetComponent<MainMenu>().CheckSave(sceneToUnlock))
        {
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Image>().sprite = unlocked;
        }

        if (!MainMenu.GetComponent<MainMenu>().CheckSave(levelScene)) return;

        if (SaveManager.Load<SaveInfo>(MainMenu.GetComponent<MainMenu>().folderName, MainMenu.GetComponent<MainMenu>().sceneName).medal == "lightning")
            this.GetComponent<Image>().sprite = diamond;
        else if (SaveManager.Load<SaveInfo>(MainMenu.GetComponent<MainMenu>().folderName, MainMenu.GetComponent<MainMenu>().sceneName).medal == "cheetah")
            this.GetComponent<Image>().sprite = gold;
        else if (SaveManager.Load<SaveInfo>(MainMenu.GetComponent<MainMenu>().folderName, MainMenu.GetComponent<MainMenu>().sceneName).medal == "bunny")
            this.GetComponent<Image>().sprite = silver;
        else if (SaveManager.Load<SaveInfo>(MainMenu.GetComponent<MainMenu>().folderName, MainMenu.GetComponent<MainMenu>().sceneName).medal == "snail")
            this.GetComponent<Image>().sprite = bronze;

    }

}
