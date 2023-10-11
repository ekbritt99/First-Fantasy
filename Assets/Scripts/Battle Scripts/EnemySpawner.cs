using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    //prefabs for all enemies
    public GameObject gameManager;
    public GameObject typeOneEnemy;
    public GameObject typeTwoEnemy;
    public GameObject typeThreeEnemy;
    public GameObject typeFourEnemy;
    public GameObject typeFiveEnemy;
    public GameObject typeSixEnemy;
    public GameObject typeSevenEnemy;
    public GameObject typeEightEnemy;
    public GameObject typeNineEnemy;
    public GameObject typeTenEnemy;

    //arrays which will hold all enemies to be spawned to save performance by avoid instantiating and destroying prefabs 
    public GameObject[] typeOneEnemies;
    public GameObject[] typeTwoEnemies;
    public GameObject[] typeThreeEnemies;
    public GameObject[] typeFourEnemies;
    public GameObject[] typeFiveEnemies;
    public GameObject[] typeSixEnemies;
    public GameObject[] typeSevenEnemies;
    public GameObject[] typeEightEnemies;
    public GameObject[] typeNineEnemies;
    public GameObject[] typeTenEnemies;

    //location where the unspawned enemies will reside so we can check which enemies are unspawned by their location
    public float DEF_X_POSITION = -11.0f;

    // start is used to instantiate the enemy arrays and populate them
    void Start()
    {
        typeOneEnemies = new GameObject[5];
        typeTwoEnemies = new GameObject[5];
        typeThreeEnemies = new GameObject[5];
        typeFourEnemies = new GameObject[5];
        typeFiveEnemies = new GameObject[5];
        typeSixEnemies = new GameObject[5];
        typeSevenEnemies = new GameObject[5];
        typeEightEnemies = new GameObject[5];
        typeNineEnemies = new GameObject[5];
        typeTenEnemies = new GameObject[5];

        populateEnemyArray(typeOneEnemies, typeOneEnemy);
        populateEnemyArray(typeTwoEnemies, typeTwoEnemy);
        populateEnemyArray(typeThreeEnemies, typeThreeEnemy);
        populateEnemyArray(typeFourEnemies, typeFourEnemy);
        populateEnemyArray(typeFiveEnemies, typeFiveEnemy);
        populateEnemyArray(typeSixEnemies, typeSixEnemy);
        populateEnemyArray(typeSevenEnemies, typeSevenEnemy);
        populateEnemyArray(typeEightEnemies, typeEightEnemy);
        populateEnemyArray(typeNineEnemies, typeNineEnemy);
        populateEnemyArray(typeTenEnemies, typeTenEnemy);

        InvokeRepeating("spawnEnemies", 0.0f, 2.0f);
    }

    //used to populate any enemy array with 5 copies of the same enemy
    public void populateEnemyArray(GameObject[] enemyArray, GameObject enemyType)
    {
        //set the default unspawned location of the enemy
        Vector3 location = Vector3.zero;
        location.x = DEF_X_POSITION;
        location.y = 100.0f;
        location.z = 1.0f;

        //create 5 enemies of the same type and instantiate them in the default location
        for (int i = 0; i < 5; i++)
        {
            enemyArray[i] = Instantiate(enemyType, location, Quaternion.identity);
        }
    }

    //depending on which area the player is in, spawn an enemy of random type
    public void spawnEnemies()
    {
        //int randomEnemyType = Random.Range(1, 11);
        int randomEnemyType = 1;
        string stringEncounter = GameManager.Instance.SceneString();

        if(stringEncounter == "World")
        {
            randomEnemyType = Random.Range(1,4);
        }
        if(stringEncounter == "Castle")
        {
            randomEnemyType = Random.Range(4,7);
        }
        // if(stringEncounter == "World2")
        // {
        //     randomEnemyType = Random.Range(7,11);
        // }
        if(stringEncounter == "None")
        {
            randomEnemyType = Random.Range(1,11);
        }
        
        //for each type of enemy, get the next available enemy from its array
        if (randomEnemyType == 1)
        {
            getNextAvailableEnemy(typeOneEnemies);
        }
        if (randomEnemyType == 2)
        {
            getNextAvailableEnemy(typeTwoEnemies);
        }
        if (randomEnemyType == 3)
        {
            getNextAvailableEnemy(typeThreeEnemies);
        }
        if (randomEnemyType == 4)
        {
            getNextAvailableEnemy(typeFourEnemies);
        }
        if (randomEnemyType == 5)
        {
            getNextAvailableEnemy(typeFiveEnemies);
        }
        if (randomEnemyType == 6)
        {
            getNextAvailableEnemy(typeSixEnemies);
        }
        if (randomEnemyType == 7)
        {
            getNextAvailableEnemy(typeSevenEnemies);
        }
        if (randomEnemyType == 8)
        {
            getNextAvailableEnemy(typeEightEnemies);
        }
        if (randomEnemyType == 9)
        {
            getNextAvailableEnemy(typeNineEnemies);
        }
        if (randomEnemyType == 10)
        {
            getNextAvailableEnemy(typeTenEnemies);
        }
    }

    //gets the next available enemy of a single type if there is one available in its array which isn't spawned
    public void getNextAvailableEnemy(GameObject[] enemyArray)
    {
        string scene = GameManager.Instance.SceneString();
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (enemyArray[i].transform.position.x == DEF_X_POSITION && (scene == "World" || scene == "World2"))
            {
                float newXPos = Random.Range(-4.5f, 7.14f);
                float newYPos = Random.Range(-4.5f, 2.5f);
                Vector3 newPosition = enemyArray[i].transform.position;
                newPosition.x = newXPos;
                newPosition.y = newYPos;
                enemyArray[i].transform.position = newPosition;
                return;
            }
            if (enemyArray[i].transform.position.x == DEF_X_POSITION && scene == "Castle")
            {
                float newXPos = Random.Range(-4.5f, 4.5f);
                float newYPos = Random.Range(-4.36f, 2.75f);
                Vector3 newPosition = enemyArray[i].transform.position;
                newPosition.x = newXPos;
                newPosition.y = newYPos;
                enemyArray[i].transform.position = newPosition;
                return;
            }
        }
        //this part is only called if all enemies of this type are spawned at the same time. recalls spawnEnemies to attempt to spawn another enemy
        spawnEnemies();
    }
    
    //spawn enemies of all type one for testing purposes
    public void spawnAllOfEnemyTypeOne()
    {
        for (int i = 0; i < typeOneEnemies.Length; i++)
        {
            if (typeOneEnemies[i].transform.position.x == DEF_X_POSITION)
            {
                float newXPos = Random.Range(-4.5f, 7.14f);
                float newYPos = Random.Range(-4.5f, 2.5f);
                Vector3 newPosition = typeOneEnemies[i].transform.position;
                newPosition.x = newXPos;
                newPosition.y = newYPos;
                typeOneEnemies[i].transform.position = newPosition;
            }
        }
    }

    //populate all the arrays for enemies to be spawned
    public void populateAllArrays()
    {
        typeOneEnemies = new GameObject[5];
        typeTwoEnemies = new GameObject[5];
        typeThreeEnemies = new GameObject[5];
        typeFourEnemies = new GameObject[5];
        typeFiveEnemies = new GameObject[5];
        typeSixEnemies = new GameObject[5];
        typeSevenEnemies = new GameObject[5];
        typeEightEnemies = new GameObject[5];
        typeNineEnemies = new GameObject[5];
        typeTenEnemies = new GameObject[5];

        populateEnemyArray(typeOneEnemies, typeOneEnemy);
        populateEnemyArray(typeTwoEnemies, typeTwoEnemy);
        populateEnemyArray(typeThreeEnemies, typeThreeEnemy);
        populateEnemyArray(typeFourEnemies, typeFourEnemy);
        populateEnemyArray(typeFiveEnemies, typeFiveEnemy);
        populateEnemyArray(typeSixEnemies, typeSixEnemy);
        populateEnemyArray(typeSevenEnemies, typeSevenEnemy);
        populateEnemyArray(typeEightEnemies, typeEightEnemy);
        populateEnemyArray(typeNineEnemies, typeNineEnemy);
        populateEnemyArray(typeTenEnemies, typeTenEnemy);

        populateEnemyArray(typeOneEnemies, typeOneEnemy);
        populateEnemyArray(typeTwoEnemies, typeTwoEnemy);
        populateEnemyArray(typeThreeEnemies, typeThreeEnemy);
        populateEnemyArray(typeFourEnemies, typeFourEnemy);
        populateEnemyArray(typeFiveEnemies, typeFiveEnemy);
        populateEnemyArray(typeSixEnemies, typeSixEnemy);
        populateEnemyArray(typeSevenEnemies, typeSevenEnemy);
        populateEnemyArray(typeEightEnemies, typeEightEnemy);
        populateEnemyArray(typeNineEnemies, typeNineEnemy);
        populateEnemyArray(typeTenEnemies, typeTenEnemy);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
