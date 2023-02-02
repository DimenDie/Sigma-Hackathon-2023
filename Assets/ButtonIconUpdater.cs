using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIconUpdater : MonoBehaviour
{
    public int indexOfPreviousLevelScene;
    public GameObject MainMenu;

    public Sprite locked;
    public Sprite unlocked;

    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;
    public Sprite diamond;
    void Start()
    {
        int indexOfCurrentLevelScene = indexOfPreviousLevelScene + 1;
        if (indexOfPreviousLevelScene == 0 || MainMenu.GetComponent<MainMenu>().CheckSave(indexOfPreviousLevelScene))
        {
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Image>().sprite = unlocked;
        }

        if (!MainMenu.GetComponent<MainMenu>().CheckSave(indexOfCurrentLevelScene)) return;

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
