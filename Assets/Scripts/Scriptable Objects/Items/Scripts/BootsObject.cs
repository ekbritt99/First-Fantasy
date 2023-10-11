using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Boots")]
public class BootsObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Boots;
    }

}
