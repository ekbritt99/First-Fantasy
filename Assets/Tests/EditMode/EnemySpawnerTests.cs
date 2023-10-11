using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemySpawnerTests : ScriptableObject
{

    [SerializeField] GameObject EnemySpawnerObject;
    // A Test behaves as an ordinary method
    [Test]

    public void EnemySpawnerTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        GameObject EnemySpawnerObject2 = GameObject.Instantiate(EnemySpawnerObject) as GameObject;
        EnemySpawner testEnemySpawner = EnemySpawnerObject2.GetComponent<EnemySpawner>();
        //populate the arrays of enemies and spawn all the enemies of type one
        testEnemySpawner.populateAllArrays();
        testEnemySpawner.spawnAllOfEnemyTypeOne();
        //attempt to spawn another enemy of type one (it should spawn an enemy of another random type to avoid an error)
        testEnemySpawner.getNextAvailableEnemy(testEnemySpawner.typeOneEnemies);

        int unspawnedCounter = 0;
        int correctUnspawnedCount = 44;
        
        //there should now be one less unspawned enemy from all the other type (45 total, so there should be 44 unspawned)
        for (int i = 0; i < testEnemySpawner.typeTwoEnemies.Length; i++)
        {
            if (testEnemySpawner.typeTwoEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
            if (testEnemySpawner.typeThreeEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
            if (testEnemySpawner.typeFourEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
            if (testEnemySpawner.typeFiveEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
            if (testEnemySpawner.typeSixEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
            if (testEnemySpawner.typeSevenEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
            if (testEnemySpawner.typeEightEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
            if (testEnemySpawner.typeNineEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
            if (testEnemySpawner.typeTenEnemies[i].transform.position.x == testEnemySpawner.DEF_X_POSITION)
            {
                unspawnedCounter++;
            }
        }
        
        Assert.AreEqual(correctUnspawnedCount, unspawnedCounter);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EnemySpawnerTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
