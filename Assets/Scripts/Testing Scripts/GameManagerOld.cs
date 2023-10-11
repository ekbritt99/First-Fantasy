using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerOld : MonoBehaviour
{
    public string villageScene = "Test Village Scene";
    public string overworldScene = "Test World Scene";
    public string battleScene = "Battle Scene";
    public string inventoryScene = "InventoryUI Scene";
    public string shopScene = "Shop Scene";

    public InventoryInfoScreen InventoryInfoScreen;
    public ShopInfoScreen ShopInfoScreen;
    public StartInfoScreen StartInfoScreen;

    bool pause = false;
    public GameObject pauseMenu;

    public GameObject swordUpgradeMenu;

    public GameObject dataManager;

    public TMP_Text lblCurrentMoneyAmount;

    public GameObject sceneTrackerObj;

    public GameObject dialogueBox;

    public GameObject playerObj;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        sceneTrackerObj = GameObject.FindGameObjectWithTag("Scene Tracker");

        // Return early if the game is started from a scene without a tracker
        if (sceneTrackerObj == null) {
            return;
        }

        if (sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory.Count > 0)
        {
            int numOfScenes = sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory.Count - 1;
            if (sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory[numOfScenes] == "House Scene")
            {
                playerObj.transform.position = new Vector3(7.71f, -0.81f, 1.57f);
            }
            if (sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory[numOfScenes] == "Shop Scene")
            {
                playerObj.transform.position = new Vector3(-5.71f, 0.45f, 1.57f);
            }
            if (sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory[numOfScenes] == "Test World Scene")
            {
                playerObj.transform.position = new Vector3(7.71f, -0.81f, 1.57f);
            }
        }
        
        if (SceneManager.GetActiveScene().name == "House Scene")
        {
            playerObj.transform.position = new Vector3(1.3f, -0.23f, -3f);
        }

        int scenes = sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory.Count - 1;
        if (SceneManager.GetActiveScene().name == "Test World Scene" && sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory[scenes] == "Battle Scene")
        {
            int positions = sceneTrackerObj.GetComponent<SceneTracker>().positionHistory.Count - 1;
            playerObj.transform.position = sceneTrackerObj.GetComponent<SceneTracker>().positionHistory[positions];
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Test World Scene")
            {
                playerObj.transform.position = new Vector3(-2.79f, -1.22f, 1.57f);
            }
        }

        if (SceneManager.GetActiveScene().name == "Test Village Scene" && sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory[scenes] == "InventoryUI Scene")
        {
            int positions = sceneTrackerObj.GetComponent<SceneTracker>().positionHistory.Count - 1;
            playerObj.transform.position = sceneTrackerObj.GetComponent<SceneTracker>().positionHistory[positions];
        }

        if (SceneManager.GetActiveScene().name == "Test World Scene" && sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory[scenes] == "InventoryUI Scene")
        {
            int positions = sceneTrackerObj.GetComponent<SceneTracker>().positionHistory.Count - 1;
            playerObj.transform.position = sceneTrackerObj.GetComponent<SceneTracker>().positionHistory[positions];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.E) && SceneManager.GetActiveScene().name != "InventoryUI Scene")
        {
            GoToInventory();
        }

        // if (SceneManager.GetActiveScene().name == "Shop Scene" || SceneManager.GetActiveScene().name == "Test Village Scene")
        // {
        //     lblCurrentMoneyAmount.text = dataManager.GetComponent<DataPersistenceManager>().getMoneyAmount().ToString();
        // }
        
    }

    void Pause()
    {
        pauseMenu.SetActive(!pause);
        if (!pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        pause = !pause;
    }

    public void DisplayDialogueBox(string text)
    {
        dialogueBox.GetComponentInChildren<TextMeshProUGUI>().text = text;
        dialogueBox.SetActive(true);
    }

    public void HideDialogeBox()
    {
        dialogueBox.SetActive(false);
    }


    void openSwordUpgradeMenu()
    {
        swordUpgradeMenu.SetActive(true);
    }

    void GoToVillage()
    {
        SceneManager.LoadScene(villageScene);
    }

    void GoToOverWorld()
    {
        SceneManager.LoadScene(overworldScene);
    }



    void GoToBattle()
    {
        SceneManager.LoadScene(battleScene);
    }
    void GoToInventory()
    {
        sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
        sceneTrackerObj.GetComponent<SceneTracker>().rememberScene();
        SceneManager.LoadScene(inventoryScene);
    }
    void GoToShop()
    {
        SceneManager.LoadScene(shopScene);
    }

    void GoToVillageFromHouseOrShop()
    {
        sceneTrackerObj.GetComponent<SceneTracker>().rememberScene();
        SceneManager.LoadScene(villageScene);
    }


    //Info Screen Management
    public void OpenInventoryInfo()
    {
        InventoryInfoScreen.Setup();
    }
    public void CloseInventoryInfo()
    {
        InventoryInfoScreen.Shutdown();
    }
    public void OpenShopInfo()
    {
        ShopInfoScreen.Setup();
    }
    public void CloseShopInfo()
    {
        ShopInfoScreen.Shutdown();
    }
    public void OpenStartInfo()
    {
        StartInfoScreen.Setup();
    }
    public void CloseStartInfo()
    {
        StartInfoScreen.Shutdown();
    }
    public void StartfromInfoScreen()
    {
        GoToHouseScene();
    }
    public void GoToHouseScene()
    {
        SceneManager.LoadScene("House Scene");
    }

   public void goToPreviousScene()
    {
        sceneTrackerObj.GetComponent<SceneTracker>().rememberScene();
        int numOfScenes = sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory.Count - 1;
        SceneManager.LoadScene(sceneTrackerObj.GetComponent<SceneTracker>().sceneHistory[numOfScenes-1]);
    }

    public void Quit()
    {
        Application.Quit(0);
    }

    private void OnApplicationQuit()
    {

    }
}
