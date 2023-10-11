using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inventory slot that can hold an item
[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];

    [System.NonSerialized]
    public InventoryInterface parent;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    public Item item;
    public int amount;

    // Returns the item object from the database
    public ItemObject ItemObject
    {
        get
        {
            if(item.ID >= 0)
                return parent.inventory.database.GetItem[item.ID];
            return null;
        }
    }

    public InventorySlot()
    {
        item = new Item();
        amount = 0;
    }

    public InventorySlot(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int val)
    {
        amount += val;
    }

    public void RemoveAmount(int val)
    {
        if (amount - val <= 0)
        {
            RemoveItem();
        }
        else
        {
            amount -= val;
        }
    }

    // Directly set the item and amount of the slot
    // Invokes On_Update to trigger the attributes to recalculate in Player.cs
    public void UpdateSlot(Item _item, int _amount)
    {
        OnBeforeUpdate?.Invoke(this);
        item = _item;
        amount = _amount;
        OnAfterUpdate?.Invoke(this);
    }

    public void RemoveItem()
    {
        item = new Item();
        amount = 0;
    }

    // Check if the item can be placed in the slot
    public bool CanPlaceInSlot(ItemObject _item)
    {
        if(AllowedItems.Length <=0 || _item == null)
        {
            return true;
        }
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if(_item.type == AllowedItems[i])
                return true;
        }
        return false; 
    }
}

public delegate void SlotUpdated(InventorySlot _slot);
