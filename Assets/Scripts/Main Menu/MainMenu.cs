using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Main Menu scene
public class MainMenu : MonoBehaviour
{

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;


    private void Start()
    {
        this.ActivateMenu();
    }

    // Show the save slot menu with loading flag flase.
    public void OnNewGameClicked()
    {
        DisableMenuButtons();

        saveSlotsMenu.ActivateMenu(false);
    }

    // Show the save slot menu with loading flag true.
    public void OnLoadGameMenu()
    {
        DisableMenuButtons();

        saveSlotsMenu.ActivateMenu(true);
    }

    public void OnSettingsClicked()
    {
        GameManager.Instance.settingsMenu.SetActive(true);
    }

    // Disable the menu buttons
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        loadGameButton.interactable = false;
    }

    // Set the tutorial to be active.
    public void ShowTutorial()
    {
        tutorial.SetActive(true);
    }

    // Go to the game scene after the tutorial button is clicked.
    public void OnTutorialStartGameClicked()
    {
        DataPersistenceManager.instance.NewGame();
        GameManager.Instance.GoToGameScene(Scenes.HOUSE);
    }

    // Activate the main menu and set the load game button to be interactable if there is data.
    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        newGameButton.interactable = true;
        
        // Set the load game button to be interactable if there is data
        loadGameButton.interactable = DataPersistenceManager.instance.HasGameData();
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
