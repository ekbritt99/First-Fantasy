using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//this class handles the player's collisions with other objects, mostly scene triggers

public class PlayerCollisions : MonoBehaviour
{
    public PlayerPersistency playerPersistency;
    
    public GameObject gameManager;

    public GameObject shopButton;

    public GameObject sceneTrackerObj;

    public GameObject playerObj;

    public NPCBubbleManager bubbleManager;

    // Start is called before the first frame update
    void Start()
    {
        sceneTrackerObj = GameObject.FindGameObjectWithTag("Scene Tracker");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //opens world scene on contact with hitbox at right of village scene
        if (collision.gameObject.tag == "Scene Trigger")
        {
            // does not work playerPersistency.Heal(5);
            GameManager.Instance.GoToGameScene(Scenes.WORLD);
        }
        //opens village scene on contact with village hitbox of world scene
        if (collision.gameObject.tag == "Scene Trigger 2")
        {
            GameManager.Instance.GoToGameScene(Scenes.VILLAGE);
        }
        //opens world scene on contact with bottom of world 2 scene
        if (collision.gameObject.tag == "Scene Trigger 3")
        {
            GameManager.Instance.GoToGameScene(Scenes.WORLD);
        }

        //opens village scene on contact with door hitbox in house
        if (collision.gameObject.tag == "leave house")
        {
            GameManager.Instance.GoToGameScene(Scenes.VILLAGE);
        }
        //opens world 2 scene with boss on contact with top of world scene
        if (collision.gameObject.tag == "World 2 Trigger") 
        {
            GameManager.Instance.updateObjectiveSave(3);
            GameManager.Instance.GoToGameScene(Scenes.WORLD2);
        }
        //opens castle scene on contact with castle trigger in world scene
        if (collision.gameObject.tag == "Castle Trigger") 
        {
            GameManager.Instance.GoToGameScene(Scenes.CASTLE);
        }
        /*
        if (collision.gameObject.tag == "Top Left Castle Door Trigger") 
        {
            GameManager.Instance.GoToGameScene(Scenes.TL_CASTLE_DOOR);
        }
        if (collision.gameObject.tag == "Top Right Castle Door Trigger") 
        {
            GameManager.Instance.GoToGameScene(Scenes.TR_CASTLE_DOOR);
        }
        if (collision.gameObject.tag == "Bottom Left Castle Door Trigger") 
        {
            GameManager.Instance.GoToGameScene(Scenes.BL_CASTLE_DOOR);
        }
        if (collision.gameObject.tag == "Bottom Right Castle Door Trigger") 
        {
            GameManager.Instance.GoToGameScene(Scenes.BR_CASTLE_DOOR);
        }
        */
        //these are for contact with enemies 
        if (collision.gameObject.tag == "One")
        {
            GameManager.Instance.enemyHistory.Add("One");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("One");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Two")
        {
            GameManager.Instance.enemyHistory.Add("Two");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Two");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Three")
        {
            GameManager.Instance.enemyHistory.Add("Three");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Three");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Four")
        {
            GameManager.Instance.enemyHistory.Add("Four");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Four");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Five")
        {
            GameManager.Instance.enemyHistory.Add("Five");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Five");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Six")
        {
            GameManager.Instance.enemyHistory.Add("Six");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Six");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Seven")
        {
            GameManager.Instance.enemyHistory.Add("Seven");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Seven");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Eight")
        {
            GameManager.Instance.enemyHistory.Add("Eight");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Eight");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Nine")
        {
            GameManager.Instance.enemyHistory.Add("Nine");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Nine");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        if (collision.gameObject.tag == "Ten")
        {
            GameManager.Instance.enemyHistory.Add("Ten");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Ten");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        
        if (collision.gameObject.tag == "boss1")
        {
            GameManager.Instance.enemyHistory.Add("boss1");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Ten");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }

            if (collision.gameObject.tag == "boss2")
        {
            GameManager.Instance.enemyHistory.Add("boss2");
            GameManager.Instance.GoToGameScene(Scenes.BATTLE);

            //GameObject.GetComponent<enemyBoss2Prefab>

            // sceneTrackerObj.GetComponent<SceneTracker>().rememberEnemy("Ten");
            // sceneTrackerObj.GetComponent<SceneTracker>().rememberPosition(playerObj.transform.position);
            // gameManager.SendMessage("GoToBattle");
        }
        
        //shows dialogue box when player touches npc
        if (collision.gameObject.tag == "NPCRat2")
        {
            bubbleManager.SendMessage("showShopBubble");
            bubbleManager.SendMessage("startDialogue");
            GameManager.Instance.updateObjectiveSave(0);
        }
        //shows dialogue box when player moves towards boss in world 2 scene
        if (collision.gameObject.tag == "boss2text")
        {
            Debug.Log("touched npc");
            NPCBubbleManager bubbleManager = GameObject.Find("NPC Bubble Manager").GetComponent<NPCBubbleManager>();
            bubbleManager.SendMessage("showDialogueBox");
            bubbleManager.SendMessage("startDialogue");
            GameManager.Instance.updateObjectiveSave(6);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //opens shop when player touches shop trigger
        if (collision.gameObject.tag == "Shop Trigger")
        {
            GameManager.Instance.GoToGameScene(Scenes.SHOP);
        }
        if (collision.gameObject.tag == "Scene Trigger")
        {
            GameManager.Instance.updateObjectiveSave(1);
            GameManager.Instance.GoToGameScene(Scenes.WORLD);
        }
        //returns to village scene from shop scene
        if (collision.gameObject.tag == "Scene Trigger 2")
        {
            GameManager.Instance.GoToGameScene(Scenes.VILLAGE);
        }
        //goes to house scene from village scene
        if (collision.gameObject.tag == "house")
        {
            GameManager.Instance.GoToGameScene(Scenes.HOUSE);
        }
        //shows dialogue box when player touches rat in village
        if (collision.gameObject.tag == "NPC Rat")
        {
            NPCBubbleManager bubbleManager = GameObject.Find("NPC Bubble Manager").GetComponent<NPCBubbleManager>();
            bubbleManager.SendMessage("showDialogueBox");
            bubbleManager.SendMessage("startDialogue");
            //GameManager.Instance.SendMessage("DisplayDialogueBox", "Don't forget to stop by the shop to the west of town and buy some gear...");
            // gameManager.SendMessage("DisplayDialogueBox", "Don't forget to stop by the shop to the west of town and buy some gear...");
        }
        //shows dialogue with boss in world 2
        if (collision.gameObject.tag == "boss1text")
        {
            NPCBubbleManager bubbleManager = GameObject.Find("NPCBubbleManager").GetComponent<NPCBubbleManager>();
            bubbleManager.SendMessage("showDialogueBox");
            bubbleManager.SendMessage("startDialogue");
        }

        //opens castle scene from world scene
        if (collision.gameObject.tag == "Castle Trigger") 
        {
            GameManager.Instance.updateObjectiveSave(2);
            GameManager.Instance.GoToGameScene(Scenes.CASTLE);
        }
        //the next 4 open their respective castle rooms on contact with doors
        if (collision.gameObject.tag == "Top Left Castle Door Trigger") 
        {
            GameManager.Instance.updateObjectiveSave(5);
            GameManager.Instance.GoToGameScene(Scenes.TL_CASTLE_DOOR);
        }
        if (collision.gameObject.tag == "Top Right Castle Door Trigger") 
        {
            GameManager.Instance.updateObjectiveSave(5);
            GameManager.Instance.GoToGameScene(Scenes.TR_CASTLE_DOOR);
        }
        if (collision.gameObject.tag == "Bottom Left Castle Door Trigger") 
        {
            GameManager.Instance.updateObjectiveSave(5);
            GameManager.Instance.GoToGameScene(Scenes.BL_CASTLE_DOOR);
        }
        if (collision.gameObject.tag == "Bottom Right Castle Door Trigger") 
        {
            GameManager.Instance.updateObjectiveSave(5);
            GameManager.Instance.GoToGameScene(Scenes.BR_CASTLE_DOOR);
        }
        //chest triggers open chests found in castle and add currency to player
        if (collision.gameObject.tag == "TL Castle Chest 1 Trigger") 
        {
            playerPersistency = PlayerPersistency.Instance;
            playerPersistency.openedChests[collision.gameObject.GetComponent<Chest>().chestIndex] = 1;
            playerPersistency.money.addCurrency(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "TL Castle Chest 2 Trigger") 
        {
            playerPersistency = PlayerPersistency.Instance;
            playerPersistency.openedChests[collision.gameObject.GetComponent<Chest>().chestIndex] = 1;
            playerPersistency.money.addCurrency(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "TR Castle Chest 1 Trigger") 
        {
            playerPersistency = PlayerPersistency.Instance;
            playerPersistency.openedChests[collision.gameObject.GetComponent<Chest>().chestIndex] = 1;
            playerPersistency.money.addCurrency(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "TR Castle Chest 2 Trigger") 
        {
            playerPersistency = PlayerPersistency.Instance;
            playerPersistency.openedChests[collision.gameObject.GetComponent<Chest>().chestIndex] = 1;
            playerPersistency.money.addCurrency(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BL Castle Chest 1 Trigger") 
        {
            playerPersistency = PlayerPersistency.Instance;
            playerPersistency.openedChests[collision.gameObject.GetComponent<Chest>().chestIndex] = 1;
            playerPersistency.money.addCurrency(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BL Castle Chest 2 Trigger") 
        {
            playerPersistency = PlayerPersistency.Instance;
            playerPersistency.openedChests[collision.gameObject.GetComponent<Chest>().chestIndex] = 1;
            playerPersistency.money.addCurrency(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BR Castle Chest 1 Trigger") 
        {
            playerPersistency = PlayerPersistency.Instance;
            playerPersistency.openedChests[collision.gameObject.GetComponent<Chest>().chestIndex] = 1;
            playerPersistency.money.addCurrency(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BR Castle Chest 2 Trigger") 
        {
            Debug.Log("touched chest");
            playerPersistency = PlayerPersistency.Instance;
            playerPersistency.openedChests[collision.gameObject.GetComponent<Chest>().chestIndex] = 1;
            playerPersistency.money.addCurrency(5);
            Destroy(collision.gameObject);
        }
    }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Shop Trigger")
    //     {
    //         shopButton.SetActive(false);
    //     }
        
    // }
}
