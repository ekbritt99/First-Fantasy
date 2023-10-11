using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores item data, not to be confused with ItemObject
[System.Serializable]
public class Item
{
    public string Name;
    public int ID;
    public ItemBuff[] buffs;

    public Item()
    {
        Name = "";
        ID = -1;
    }
    
    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.ID;
        buffs = new ItemBuff[item.data.buffs.Length];
        for(int i = 0; i < buffs.Length; i++) 
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].Min, item.data.buffs [i].Max);
            buffs[i].stat = item.data.buffs[i].stat;
        }
        
    }

}
