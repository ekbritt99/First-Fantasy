using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum ItemType
{
    Helmet,
    Chest,
    Legs,
    Boots,
    Weapon,
    Offhand,
    Food,
    Default
}

// Item Scriptable Objects for the inventory system, holds item data
public abstract class ItemObject : ScriptableObject
{
    public int ID;
    public Sprite uiDisplay;
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public bool stackable = false;
    public Item data = new Item();
    
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}
