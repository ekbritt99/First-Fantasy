using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class InventorySystemTests
{

    [Test]
    public void TestAddItemToInventory()
    {
        var inventory = ScriptableObject.Instantiate((InventoryObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Scriptable Objects/Inventory/Player Inventory.asset", typeof(InventoryObject)));
        var database = ScriptableObject.Instantiate((ItemsDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Items/ItemsDatabaseObject.asset", typeof(ItemsDatabaseObject)));
        var random = Random.Range(0, database.itemObjects.Length);

        var item = database.GetItem[random];

        int amt = 1;
        Item item1 = item.CreateItem();

        inventory.AddItem(item1, amt);

        Assert.AreEqual(item1.Name, inventory.container.Items[0].item.Name);
        Assert.AreEqual(amt, inventory.container.Items[0].amount);
    }

    [Test]
    public void TestFindItemInInventory()
    {
        var inventory = ScriptableObject.Instantiate((InventoryObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Scriptable Objects/Inventory/Player Inventory.asset", typeof(InventoryObject)));
        var database = ScriptableObject.Instantiate((ItemsDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Items/ItemsDatabaseObject.asset", typeof(ItemsDatabaseObject)));
        var random = Random.Range(0, database.itemObjects.Length);

        var item = database.GetItem[random];

        int amt = 1;
        Item item1 = item.CreateItem();

        inventory.AddItem(item1, amt);

        InventorySlot foundSlot = inventory.FindItemInInventory(item1);

        Assert.AreEqual(item1.Name, foundSlot.item.Name);
        Assert.AreEqual(amt, foundSlot.amount);
    }
    

    [TearDown]
    public void TearDown() 
    {
        var inventory = (InventoryObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Scriptable Objects/Inventory/Player Inventory.asset", typeof(InventoryObject));
        inventory.container.Items = new InventorySlot[29];
    }

}