using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // private static Player _instance;
    // public static Player Instance { get { return _instance; } }

    public InventoryObject inventory;
    public InventoryObject equipment;

    // public Attribute[] attributes;

    public PlayerCollisions collisions;
    public BoundaryManager boundaryManager;

    public bool isControllable = true;

    public bool isHovering;

    [SerializeField] GameObject hpAndEquipmentDisplay;


    // public PersistentEntityUnit playerUnit;

    // private void Awake()
    // {
    //     if(_instance != null && _instance != this)
    //     {
    //         Destroy(this.gameObject);
    //         return;
    //     }
    //     _instance = this;
    //     DontDestroyOnLoad(this.gameObject);

    // }

    private void Start() 
    {
        // Handle temporary spawn position - useful for returning to scenes or entering scenes in custom positions
        if(PlayerPersistency.Instance.spawnPosition != null && PlayerPersistency.Instance.spawnPosition != Vector3.zero) {
            
            this.gameObject.transform.position = PlayerPersistency.Instance.spawnPosition;
            // Reset the spawnPosition to zero so that the player spawns in default position in scenes unless otherwise specified
            PlayerPersistency.Instance.spawnPosition = Vector3.zero;
        }

        // Collect attribute stats 
        for(int i = 0; i < PlayerPersistency.Instance.attributes.Length; i++)
        {
            PlayerPersistency.Instance.attributes[i].SetParent(this);
        }


        // This adds on equip event listeners to the equipment slots so that attributes update on equip
        for(int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnRemoveItem;
            equipment.GetSlots[i].OnAfterUpdate += OnAddItem;
        }

        isHovering = false;
    }

    private void OnDisable()
    {
        // Remove event handlers when the player is unloaded to prevent duplicate handlers
        for(int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate -= OnRemoveItem;
            equipment.GetSlots[i].OnAfterUpdate -= OnAddItem;
        }
    }


    public void OnRemoveItem(InventorySlot _slot)
    {
        Debug.Log("OnRemoveItem called");
        if(_slot.ItemObject == null)
            return;
        switch(_slot.parent.inventory.type)
        {
            case InventoryType.Inventory:
                break;
            case InventoryType.Equipment:
                Debug.Log("Removed " + _slot.ItemObject + " on " + _slot.parent.inventory.type);
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for(int j = 0; j < PlayerPersistency.Instance.attributes.Length; j++)
                    {
                        if(PlayerPersistency.Instance.attributes[j].type == _slot.item.buffs[i].stat)
                            PlayerPersistency.Instance.attributes[j].value -= _slot.item.buffs[i].value;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void OnAddItem(InventorySlot _slot)
    {
        if(_slot.ItemObject == null)
            return;
        switch(_slot.parent.inventory.type)
        {
            case InventoryType.Inventory:
                break;
            case InventoryType.Equipment:
                Debug.Log("Equipped " + _slot.ItemObject + " on " + _slot.parent.inventory.type);
                Debug.Log("Num Attributes on Item: " + _slot.item.buffs.Length);
                for(int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for(int j = 0; j < PlayerPersistency.Instance.attributes.Length; j++)
                    {
                        if(PlayerPersistency.Instance.attributes[j].type == _slot.item.buffs[i].stat)
                            PlayerPersistency.Instance.attributes[j].value += _slot.item.buffs[i].value;
                    }
                }
                break;
            default:
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // var item = other.GetComponent<Item>();
        // if (item)
        // {
        //     inventory.AddItem(item, 1);
        //     Destroy(other.gameObject);
        // }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && this.isControllable)
        {
            GameManager.Instance.GoToGameScene(Scenes.INVENTORY);
        }

        // if(Input.GetKeyDown(KeyCode.Equals))
        // {
        //     inventory.Save();
        //     equipment.Save();
        // }

        // if(Input.GetKeyDown(KeyCode.Backslash))
        // {
        //     inventory.Load();
        //     equipment.Load();
        // }

        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     ItemObject item = inventory.database.GetItem[21];
        //     inventory.AddItem(item.CreateItem(), 1);

        //     // item = inventory.database.GetItem[1];
        //     // inventory.AddItem(item.CreateItem(), 1);
        // }
        /*
        if (SceneManager.GetActiveScene().name != "Test Village Scene")
        {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log(inventory.container.Items[0].item.ID);
                inventory.container.Items[0].item.buffs[0].UpgradeStat(5);
            }

        }
        */
    }

    [ContextMenu("EquipItem")]
    public void EquipItem(int fromSlot, int toSlot)
    {
        inventory.MoveItem(inventory.container.Items[fromSlot], equipment.container.Items[toSlot]);
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute));
    }

    /*
    private void OnMouseOver()
    {
        StartCoroutine(StillHovering());
        isHovering = true;
    }

    private void OnMouseExit()
    {
        isHovering = false;
        hpAndEquipmentDisplay.SetActive(false);
    }

    
    private IEnumerator StillHovering()
    {
        yield return new WaitForSeconds(1f);
        if (isHovering)
        {
            hpAndEquipmentDisplay.SetActive(true);
        }
    }
    */
}

[System.Serializable]
public class Attribute
{   
    public Player parent;
    public Attributes type;
    public int value;

    public void SetParent(Player player)
    {
        parent = player;
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}