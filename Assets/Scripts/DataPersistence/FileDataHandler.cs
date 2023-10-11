using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

// Handles reading and writing game data to a file
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    // Load the data from the file.
    public GameData Load(string profileID)
    {
        // Don't load if the profileID is empty or null
        if(profileID == "" || profileID == null)
        {
            Debug.Log("No data exists.");
            return null;
        }

        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                // Load the data from file
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Deserialize from json to object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file " + fullPath + "\n" + e);
            }
        } 

        return loadedData;
    }

    // Save the data to the file.
    public void Save(GameData data, string profileID)
    {
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        try
        {
            // Create directory for file if it doesnt exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize the game data to json string
            string dataToStore = JsonUtility.ToJson(data, true);

            // Write data to file
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file " + fullPath + "\n" + e);
        }
    }

    // Load all profiles from the data directory into dictionary.
    public Dictionary<String, GameData> LoadAllProfiles()
    {
        Dictionary<String, GameData> profileDictionary = new Dictionary<String, GameData>();

        // Loop over all directories in the data directory path
        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo directoryInfo in directoryInfos)
        {
            // Get each profileID from the directory name
            string profileID = directoryInfo.Name;

            // Check if the data file exists to prevent reading non save data directories
            string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
            if(!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory " + profileID + " because it does not contain a data file");
                continue;
            }

            // Load the data from file
            GameData loadedData = Load(profileID);
            // Check that data isn't null
            if(loadedData == null)
            {
                Debug.LogWarning("Skipping directory " + profileID + " because it does not contain valid data");
                continue;
            } 
            else 
            {
                // Add the data to the dictionary
                profileDictionary.Add(profileID, loadedData);
            }
        }

        return profileDictionary;
    }

    // Delete the data file for the given profileID.
    public void DeleteProfile(string profileID)
    {
        string fullPath = Path.Combine(dataDirPath, profileID);
        if(Directory.Exists(fullPath))
        {
            try
            {
                Directory.Delete(fullPath, true);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to delete data file " + fullPath + "\n" + e);
            }
        }
    }
}
