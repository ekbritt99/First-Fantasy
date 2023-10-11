using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

// This object is for player stats that should not be destroyed on new scene loads
// It is essentially double dipping on the job of DataPersistencyManager so this will probably change later
public class PlayerPersistency : MonoBehaviour, IDataPersistence
{
    // Player Stats
    public int damage;
    public int maxHP;
    public int currentHP;
    public Currency money;
    // Int List for indexing opened chests
    public int[] openedChests;

    public bool[] playerObjectives;

    // Attributes contains all of the player's stats that are not HP or damage
    public Attribute[] attributes;

    public InventoryObject inventory;
    public InventoryObject equipment;


    public Vector3 spawnPosition;
    public Scenes spawnScene;

    private static PlayerPersistency _instance = null;
    public static PlayerPersistency Instance { get { return _instance; } }

    // Singleton pattern
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetHP(int hp)
    {
        if(hp <= maxHP && hp >= 0)
        {
            currentHP = hp;
        }
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > maxHP)
            currentHP = maxHP;
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        currentHP = maxHP;
    }

    // Load data from GameData object into this object on scene load using DataPersistenceManager.
    public void LoadData(GameData data)
    {
        if(_instance != this)
            return;

        this.currentHP = data.playerHP;
        this.maxHP = data.playerMaxHP;
        this.money.SetCurrency(data.money);
        this.damage = data.playerDamage;

        // Load attributes from GameData
        for (int i = 0; i < attributes.Length; i++)
        {
            if(attributes[i].type == Attributes.Defense)
            {
                attributes[i].value = data.playerDefense;
            }
            if(attributes[i].type == Attributes.Agility)
            {
                attributes[i].value = data.playerAgility;
            }
            if(attributes[i].type == Attributes.Strength)
            {
                attributes[i].value = data.playerStrength;
            }
            if(attributes[i].type == Attributes.Intellect)
            {
                attributes[i].value = data.playerIntellect;
            }
        }

        this.openedChests = data.openedChests;
        this.playerObjectives = data.playerObjectives;
        // this.spawnPosition = data.spawnPosition;
    }

    public void SaveData(GameData data)
    {
        if(_instance != this)
            return;

        data.playerHP = this.currentHP;
        data.playerMaxHP = this.maxHP;
        data.money = this.money.GetCurrency();
        data.playerDamage = this.damage;
        data.playerObjectives = this.playerObjectives;

        // Save attributes to GameData
        for (int i = 0; i < attributes.Length; i++)
        {
            if(attributes[i].type == Attributes.Defense)
            {
                data.playerDefense = attributes[i].value;
            }
            if(attributes[i].type == Attributes.Agility)
            {
                data.playerAgility = attributes[i].value;
            }
            if(attributes[i].type == Attributes.Strength)
            {
                data.playerStrength = attributes[i].value;
            }
            if(attributes[i].type == Attributes.Intellect)
            {
                data.playerIntellect = attributes[i].value;
            }

            data.openedChests = this.openedChests;
        }

        
    }

    private void OnDisable()
    {
        if(_instance != this)
            return;
        inventory.Save();
        equipment.Save();
        inventory.Clear();
        equipment.Clear();
    }

}
