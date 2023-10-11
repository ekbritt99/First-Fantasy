using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// public class DisplayBattleInventory : InventoryInterface
// {
    // public GameObject inventoryPrefab;
    // private GameObject grid;

    // public override void CreateDisplay()
    // {
    //     grid = GameObject.Find("InventoryGrid");

    //     // Check if the grid has children, if so, return.
    //     if (grid.transform.childCount > 0)
    //         return;

    //     itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    //     // Apply event listeners for each item slot displayed.
    //     for (int i = 0; i < inventory.container.Items.Length; i++)
    //     {
    //         var obj = Instantiate(inventoryPrefab, grid.transform);
    //         var text = obj.transform.GetChild(1);

    //         AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
    //         AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
    //         AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
    //         AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
    //         AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
    //         AddEvent(obj, EventTriggerType.PointerDown, e => OnPointerDown(obj, e));
        
        
    //         itemsDisplayed.Add(obj, inventory.container.Items[i]);
    //     }
    // }

    // public void OnPointerDown(GameObject obj, BaseEventData eventData)
    // {
    //     PointerEventData pointerEventData = (PointerEventData)eventData;

    //     // Equip item if right clicked on in inventory
    //     if(!(pointerEventData.button == PointerEventData.InputButton.Right && itemsDisplayed[obj].item.ID > 0)) 
    //     {
    //         return;
    //     } 
    //     else
    //     {
    //         // If food item, use the food item then skip turn
    //         if(itemsDisplayed[obj].ItemObject.type == ItemType.Food)
    //         {
    //             Debug.Log("Food item used");
    //             BattleSystem battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
    //             if(!battleSystem.EndTurn())
    //                 return;
    //             // Use the food item
    //             if (healPlayer(itemsDisplayed[obj].ItemObject.data.buffs[0].value) == true)
    //             {
    //                 itemsDisplayed[obj].RemoveAmount(1);
                    
    //             }
                
    //             return;
    //         }
    //     }
    // }
// }
