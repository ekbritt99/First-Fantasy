using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Base class for all inventory interfaces
public abstract class InventoryInterface : MonoBehaviour
{
    public InventoryObject inventory;
    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    public GameObject hoverPanel;

    public bool active = false;

    // Run only if inventory has not been activated before to prevent duplicate event listeners
    public void OnEnable()
    {
        if(active)
            return;

        active = true;
        for(int i = 0; i < inventory.container.Items.Length; i++)
        {
            inventory.container.Items[i].parent = this;
        }
        // Create the display and add hover event listeners to the interface
        CreateDisplay();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });

    }

    public abstract void CreateDisplay();


    // Update the inventory slot display once per frame
    public virtual void Update()
    {
        // Update each sprite and item count once per frame
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

    // Programmatically add event listeners to objects --  See DisplayInventory.cs and DisplayEquipSlots.cs
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    /* Event - When cursor enters, starts timer to display item tool tip and sets current slotHovered in MouseData */
    public void OnEnter(GameObject obj)
    {
        MouseData.slotHovered = obj;

        if(itemsDisplayed[obj].item.ID >= 0 && MouseData.tempItemBeingDragged == null) 
        {
            StopAllCoroutines();
            StartCoroutine(StartTimer());
        }
    }

    /* Event - Tracks which inventory interface is active with cursor */
    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<InventoryInterface>();
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }

    /* Event - When cursor exits, the timer for tool tip is stopped and existing tool tip is deactivated */
    public void OnExit(GameObject obj)
    {
        if(MouseData.tempItemBeingDragged == null)
        {
            StopAllCoroutines();
        }

        MouseData.slotHovered = null;
        hoverPanel.SetActive(false);
    }

    /* Event - On start of click drag, creates an image copy of the item */
    public void OnDragStart(GameObject obj)
    {
        StopAllCoroutines();
        GameObject tempItem = null;
        if(itemsDisplayed[obj].item.ID >= 0) 
        {
            // Create a temporary item image to follow the cursor
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(1, 1);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<SpriteRenderer>();
            img.sprite = itemsDisplayed[obj].ItemObject.uiDisplay;
            img.sortingOrder = 100;
            img.sortingLayerName = "Top";
            img.size = new Vector2(1, 1);
            
        }

        MouseData.tempItemBeingDragged = tempItem;
    }
    
    /* Event - Destroys the dragged item image and moves the original item to the slot hovered */
    public void OnDragEnd(GameObject obj)
    {  
        Destroy(MouseData.tempItemBeingDragged);
        if(MouseData.slotHovered)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.itemsDisplayed[MouseData.slotHovered];
            inventory.MoveItem(itemsDisplayed[obj], mouseHoverSlotData);
        }
    }

    /* Event - Keeps the dragged item image on the mouse location */
    public void OnDrag(GameObject obj)
    {
        if(MouseData.tempItemBeingDragged != null)
        {
            // Calculate mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 100;
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = mousePos;
        }
    }

    // Timer used for the tool tip popup
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1);
        ShowToolTip();
    }

    // Displays a tool tip with item information next to the cursor
    private void ShowToolTip()
    {
        ItemObject item = inventory.database.GetItem[itemsDisplayed[MouseData.slotHovered].item.ID];
        Item item1 = MouseData.interfaceMouseIsOver.itemsDisplayed[MouseData.slotHovered].item;
        hoverPanel.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.name;
        hoverPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = item.description;

        string buffs = "";

        for (int i = 0; i < item1.buffs.Length; i++)
        {
            buffs += "- " + item1.buffs[i].stat + " +" + item1.buffs[i].value;
            if(i < item1.buffs.Length-1)
                buffs += "\n";
        }

        hoverPanel.transform.Find("Buffs").GetComponent<TextMeshProUGUI>().text = buffs;


        hoverPanel.SetActive(true);

        RectTransform hoverTransform = hoverPanel.GetComponent<RectTransform>();

        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Clamp the position to the screen size
        mousePosition.x = Mathf.Clamp(mousePosition.x + hoverTransform.sizeDelta.x / 2, 0 + hoverTransform.rect.width / 2, Camera.main.pixelWidth - hoverTransform.rect.width / 2);
        mousePosition.y = Mathf.Clamp(mousePosition.y - hoverTransform.sizeDelta.y / 2, 0 + hoverTransform.rect.height / 2, Camera.main.pixelHeight - hoverTransform.rect.height / 2);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        hoverTransform.transform.position = mousePosition;
    }

    // Called when the player clicks on a food/potion in the inventory
    public bool healPlayer(int healAmount)
    {
        GameObject playerObj = GameObject.Find("PlayerPersistency");
        PlayerPersistency playerStats = playerObj.GetComponent<PlayerPersistency>();
        if(playerStats.currentHP < playerStats.maxHP)
        {
            playerStats.Heal(healAmount);
            return true;
        }
        else
        {
            return false;
        }
        
    }


}

// Contains data about the mouse for dragging items between inventory interfaces
public static class MouseData
{
    public static InventoryInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHovered;
}