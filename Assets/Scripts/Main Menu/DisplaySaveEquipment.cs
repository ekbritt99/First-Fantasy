using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Displays the equipment in the save slot
public class DisplaySaveEquipment : MonoBehaviour
{
    public GameObject[] slots;
    public InventoryObject inventory;
    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    public void Display()
    {
        itemsDisplayed = new();

        // Add each slot to the dictionary
        for(int i = 0; i < inventory.container.Items.Length; i++)
        {
            var obj = slots[i];
            itemsDisplayed.Add(obj, inventory.container.Items[i]);
        }

        // Update each sprite and item count
        // Does not need to be done every frame since it is not interactive
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if(_slot.Value.item.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.ID].uiDisplay;
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

    }
}
