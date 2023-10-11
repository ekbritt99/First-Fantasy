using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implement this interface on any class that needs to load or save data
public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(GameData data);
}
