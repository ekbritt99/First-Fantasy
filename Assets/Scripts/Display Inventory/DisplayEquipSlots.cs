using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

// Displays the player's equipment slots
public class DisplayEquipSlots : InventoryInterface
{
    public GameObject[] slots;


    // Update the inventory slot display once per frame
    public override void Update()
    {
        // Update each sprite and item count once per frame
        // If there is no item equipped, display the default sprite
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if(_slot.Value.item.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.ID].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().enabled = false;
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().enabled = true;
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

        }
    }


    // Create the display and add hover event listeners to the interface
    public override void CreateDisplay()
    {
        itemsDisplayed = new();

        // Apply event listeners for each item slot displayed
        for(int i = 0; i < inventory.container.Items.Length; i++)
        {
            var obj = slots[i];
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, e => OnPointerDown(obj, e));

            itemsDisplayed.Add(obj, inventory.container.Items[i]);
        }
    }

    // Handles right clicking on an item in the equipment inventory, moves item from equipment to inventory slot
    public void OnPointerDown(GameObject obj, BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;

        // Equip item if right clicked on in inventory
        if(!(pointerEventData.button == PointerEventData.InputButton.Right && itemsDisplayed[obj].item.ID > 0)) {
            return;
        } else {
            InventoryInterface playerInventory = GameObject.Find("InventoryScreen").GetComponent<InventoryInterface>();
            Debug.Log("Player inventory: " + playerInventory);
            if(playerInventory == null)
                return;
            int emptySlot = playerInventory.inventory.FindEmptySlot();
            if(emptySlot > -1)
            {
                inventory.MoveItem(itemsDisplayed[obj], playerInventory.inventory.container.Items[emptySlot]);
            }
            Debug.Log("Empty slot: " + emptySlot);
            return;
        }
    }
}
