using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Offhand Object", menuName = "Inventory System/Items/Offhand")]
public class OffhandObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Offhand;
    }
}
