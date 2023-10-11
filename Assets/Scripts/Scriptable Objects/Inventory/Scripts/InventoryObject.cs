using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public enum InventoryType
{
    Inventory,
    Equipment,
    Chest
}

// Inventory Scriptable Object
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public InventoryType type;
    public ItemsDatabaseObject database; // Reference to the database of items that this inventory contains
    public Inventory container; // Holds the inventory slots
    public InventorySlot[] GetSlots { get { return container.Items; } }


    // Add an item to the inventory.
    public bool AddItem(Item _item, int _amt)
    {
        InventorySlot slot = FindItemInInventory(_item);
        // If the item is not stackable, add it to the first empty slot.
        if(slot == null || !database.itemObjects[_item.ID].stackable)
        {
            for(int i = 0; i < container.Items.Length; i++)
            {
                if(container.Items[i].item.ID <= -1)
                {
                    SetEmptySlot(_item, _amt);
                    return true;
                }
            }
            return false;
        }
        // If the item is stackable, add it to the first slot that has the same item
        slot.AddAmount(_amt);
        return true;
    }

    // Get the number of empty slots in the inventory
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for(int i = 0; i < container.Items.Length; i++)
            {
                if(container.Items[i].item.ID <= -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    // Find an item in the inventory. Returns the slot.
    public InventorySlot FindItemInInventory(Item item)
    {
        for (int i = 0; i < container.Items.Length; i++)
        {
            if(container.Items[i].item.ID == item.ID)
            {
                return container.Items[i];
            }
        }
        return null;
    }

    // Set an empty slot to an item. Returns the slot if successful.
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < container.Items.Length; i++)
        {
            if (container.Items[i].item.ID <= -1)
            {
                container.Items[i].UpdateSlot(_item, _amount);
                return container.Items[i];
            }
        }

        // Setup func for when inventory is full
        return null;
    }

    public int FindEmptySlot()
    {
        for (int i = 0; i < container.Items.Length; i++)
        {
            if (container.Items[i].item.ID <= -1)
            {
                return i;
            }
        }
        return -1;
    }

    // Swap two items in the inventory. Reminder: an empty slot is an item with ID -1.
    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        if(item2.CanPlaceInSlot(item1.ItemObject))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    // Remove an item from the inventory.
    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < container.Items.Length; i++)
        {
            if (container.Items[i].item == _item)
            {
                container.Items[i].UpdateSlot(null, 0);
            }
        }
    }

    // Save the inventory. Uses the profileID in DataPersistenceManager to save the inventory in the correct profile folder.
    // The savePath is the name of the inventory file. i.e. inventory or equipment or chest.
    [ContextMenu("Save")]
    public void Save()
    {   
        if(DataPersistenceManager.instance.GetSelectedProfileID() == null)
        {
            Debug.LogWarning("No profile selected. Game likely started from a scene other than the main menu.");
            return;
        }
        string fullPath = Path.Combine(Application.persistentDataPath, DataPersistenceManager.instance.GetSelectedProfileID(), savePath);
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, container);
        stream.Close();
        Debug.Log("Saved inventory" + type);
    }

    // Load the inventory. Uses the profileID in DataPersistenceManager to load the inventory from the correct profile folder.
    [ContextMenu("Load")]
    public void Load(string path = null)
    {
        if(DataPersistenceManager.instance.GetSelectedProfileID() == null && path == null)
        {
            Debug.LogWarning("No profile selected. Game likely started from a scene other than the main menu.");
            return;
        }

        if(path == null)
        {
            path = DataPersistenceManager.instance.GetSelectedProfileID();
        }

        string fullPath = Path.Combine(Application.persistentDataPath, path, savePath);
        if(File.Exists(fullPath))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < container.Items.Length; i++)
            {
                container.Items[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
                // GetSlots[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
            }

            // container.gold = newContainer.gold;
            stream.Close();
            Debug.Log("Loaded inventory " + type);
        }
        else 
        {
            Debug.Log("No file to load inventory " + type);
        }
    }

    // Clear the inventory.
    [ContextMenu("Clear")]
    public void Clear()
    {
        Debug.Log("Clear called");
        container.Clear();
    }

}

// This is the actual inventory container. It contains the gold and the inventory slots.
[System.Serializable]
public class Inventory
{
    // public Currency gold;
    public InventorySlot[] Items = new InventorySlot[29];
    public void Clear()
    {
        // gold = new Currency();
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(new Item(), 0);
        }
    }

    public bool ContainsItem(ItemObject itemObject)
    {
        return Array.Find(Items, i => i.item.ID == itemObject.data.ID) != null;
    }

    public bool ContainsItem(int id)
    {
        return Items.FirstOrDefault(i => i.item.ID == id) != null;
    }
}

