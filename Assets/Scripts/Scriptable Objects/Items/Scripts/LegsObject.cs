using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Legs")]
public class LegsObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Legs;
    }

}