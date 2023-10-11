using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Displays the player's inventory
public class DisplayInventory : InventoryInterface
{
    public GameObject inventoryPrefab;
    private GameObject grid;
    public Button backButton;

    public override void CreateDisplay()
    {
        grid = GameObject.Find("InventoryGrid");

        // Check if the grid has children, if so, return.
        if (grid.transform.childCount > 0)
            return;

        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        // Apply event listeners for each item slot displayed.
        for (int i = 0; i < inventory.container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, grid.transform);
            var text = obj.transform.GetChild(1);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, e => OnPointerDown(obj, e));
        
        
            itemsDisplayed.Add(obj, inventory.container.Items[i]);
        }
    }

    // Right click to equip or use items.
    public void OnPointerDown(GameObject obj, BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;

        // Equip item if right clicked on in inventory
        if(!(pointerEventData.button == PointerEventData.InputButton.Right && itemsDisplayed[obj].item.ID >= 0)) {
            return;
        } else {
            if(itemsDisplayed[obj].ItemObject.type == ItemType.Food) {
                if (healPlayer(itemsDisplayed[obj].ItemObject.data.buffs[0].value) == true)
                {
                    itemsDisplayed[obj].RemoveAmount(1);
                }
            } else {
                InventoryInterface equipment = GameObject.Find("EquipmentScreen").GetComponent<InventoryInterface>();
                // Ensure the right click was not on equipment panel
                if(this.GetComponent<InventoryInterface>() == equipment)
                {
                    return;
                }
                
                // Equip item to appropriate slot
                if(itemsDisplayed[obj].ItemObject.type == ItemType.Helmet) {
                    inventory.MoveItem(itemsDisplayed[obj], equipment.inventory.container.Items[0]);
                } else if(itemsDisplayed[obj].ItemObject.type == ItemType.Chest) {
                    inventory.MoveItem(itemsDisplayed[obj], equipment.inventory.container.Items[1]);
                } else if(itemsDisplayed[obj].ItemObject.type == ItemType.Legs) {
                    inventory.MoveItem(itemsDisplayed[obj], equipment.inventory.container.Items[2]);
                } else if(itemsDisplayed[obj].ItemObject.type == ItemType.Boots) {
                    inventory.MoveItem(itemsDisplayed[obj], equipment.inventory.container.Items[3]);
                } else if(itemsDisplayed[obj].ItemObject.type == ItemType.Weapon) {
                    inventory.MoveItem(itemsDisplayed[obj], equipment.inventory.container.Items[4]);
                } else if(itemsDisplayed[obj].ItemObject.type == ItemType.Offhand) {
                    inventory.MoveItem(itemsDisplayed[obj], equipment.inventory.container.Items[5]);
                }

                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.itemsDisplayed[MouseData.slotHovered];
                inventory.MoveItem(itemsDisplayed[obj], mouseHoverSlotData);
            }
        }
    }
}

