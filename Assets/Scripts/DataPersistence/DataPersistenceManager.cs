using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

// Manages the saving and loading of game data
public class DataPersistenceManager : MonoBehaviour
{

    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    
    private GameData gameData;
    private List<IDataPersistence> dataPersistences;
    private FileDataHandler dataHandler;

    public InventoryObject playerInventory;
    public InventoryObject playerEquipment;

    private string selectedProfileID = "";


    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        // Check if there is already an instance of DataPersistenceManager in the scene. If there is, destroy this one.
        if(instance != null)
        {
            Debug.Log("Found more than one DataPersistenceManager in scene. Newest destroyed.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
        
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        // Select the first profile.
        this.selectedProfileID = dataHandler.LoadAllProfiles().FirstOrDefault().Key;
    }

    // Add a listener for when a scene is loaded
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Remove the listener when the object is disabled
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        this.dataPersistences = FindAllDataPersistences();
        LoadGame();
    }


    public void ChangeProfile(string profileID)
    {
        // Change the profile ID
        this.selectedProfileID = profileID;

        // Load the game data
        LoadGame();
    }

    // Start a new game save. Clear the inventory scriptable objects just in case they weren't cleared before.
    public void NewGame()
    {
        Debug.Log("New Game started");
        playerInventory.Clear();
        playerEquipment.Clear();
        this.gameData = new GameData();
        this.SaveGame();
    }

    public void LoadGame()
    {
        Debug.Log("(DataPersistenceManager) LoadGame()");
        // Load game data from file with data handler
        this.gameData = dataHandler.Load(selectedProfileID);

        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        // If there is no save data, initialize to a new game
        if(this.gameData == null)
        {
            Debug.Log("No save data found. A new game needs to be started before load");
            return;
        }

        foreach(IDataPersistence dataPersistence in dataPersistences)
        {
            Debug.Log(dataPersistence);
            dataPersistence.LoadData(gameData);
        }

        Debug.Log("Loaded Player HP: " + gameData.playerHP);
    }

    public void SaveGame()
    {
        Debug.Log("(DataPersistenceManager) SaveGame()");
        if(this.gameData == null)
        {
            Debug.LogWarning("No data found. A new game must be started. Game was likely started from scene other than main menu.");
            return;
        }

        // Pass data to other scripts to be updated
        foreach(IDataPersistence dataPersistence in dataPersistences)
        {
            dataPersistence.SaveData(gameData);
        }

        // save the gathered data to file with data handler
        dataHandler.Save(gameData, selectedProfileID);
        Debug.Log("Saved Player health: " + gameData.playerHP);
    }

    public void DeleteProfile(string profileID)
    {
        dataHandler.DeleteProfile(profileID);
    }

    private void OnApplicationQuit()
    {
        if(SceneManager.GetActiveScene().buildIndex == (int) Scenes.MAIN_MENU)
            return;
        SaveGame();
        Debug.Log("Saved");
    }

    // Find all objects in the scene that implement IDataPersistence
    private List<IDataPersistence> FindAllDataPersistences()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public string GetSelectedProfileID()
    {
        return selectedProfileID;
    }

    public Dictionary<string, GameData> GetAllProfilesData()
    {
        return dataHandler.LoadAllProfiles();
    }


}
