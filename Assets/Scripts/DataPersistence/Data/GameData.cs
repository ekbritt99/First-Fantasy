using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Stores the player's game data, is saved with DataPersistenceManager
[System.Serializable]
public class GameData
{
    public int playerHP;
    public int playerMaxHP;
    public int money;
    public int playerDamage;
    public int playerDefense;
    public int playerIntellect;
    public int playerAgility;
    public int playerStrength;

    public int[] openedChests;

    public bool[] playerObjectives;

    // Constructor for default values on new game
    public GameData()
    {
        this.playerHP = 30;
        this.playerMaxHP = 30;
        this.money = 30;
        this.playerDamage = 5;
        this.playerDefense = 0;
        this.playerIntellect = 0;
        this.playerAgility = 0;
        this.playerStrength = 0;
        this.openedChests = new int[14];

        // Track player objectives
        playerObjectives = new bool[7];
        for (int i = 0; i < playerObjectives.Length; i++)
        {
            playerObjectives[i] = false;
        }

    }

    public int GetHealth()
    {
        return playerHP;
    }

    public int GetMaxHealth()
    {
        return playerMaxHP;
    }



}
