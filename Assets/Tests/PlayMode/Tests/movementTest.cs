
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class movement
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestPlayerLeftMovement()
    {
        //create player object for test
        GameObject player = MonoBehaviour.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Test Prefabs/Player.prefab"));

        //save inital x
        float x = player.transform.position.x;

        //send command to move
        player.SendMessage("moveCommand", new Vector3(1,0,0),SendMessageOptions.RequireReceiver);

        //new x
        float updatedX = player.transform.position.x;

        //test if moved
        Assert.AreNotEqual(x, updatedX);

        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator movementWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
