using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, RAN }

public class BattleSystem : MonoBehaviour, IDataPersistence
{
    public GameObject gameManager;
    public GameObject playerObj;
    public Animator playerAnimator;
    public GameObject enemyPrefab;
    public GameObject inventoryOverlay;

    public InventoryObject playerEquipment;
    public InventoryObject playerInventory;
    int playerDefense;
    int playerIntellect;
    int playerAgility;
    int playerStrength;

    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject enemyThreePrefab;
    public GameObject enemyFourPrefab;
    public GameObject enemyFivePrefab;
    public GameObject enemySixPrefab;
    public GameObject enemySevenPrefab;
    public GameObject enemyEightPrefab;
    public GameObject enemyNinePrefab;
    public GameObject enemyTenPrefab;
    public GameObject enemyBossPrefab;
    public GameObject enemyBoss2Prefab;
    GameObject enemyGO;

    public GameObject sceneTrackerObj;

    public GameOverScreen GameOverScreen;

    [SerializeField] AudioClip battleTheme;
    [SerializeField] AudioClip bossTheme;
    [SerializeField] AudioClip gameOverTheme;
    AudioSource _audio;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource defendSound;
    [SerializeField] private AudioSource attackSuccessSound;
    [SerializeField] private AudioSource battleWonSound;

    [SerializeField] private PlayerPersistency playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public int currencyReward;
    public TMP_Text currencyRewardDisplay;
    public GameObject wholeCurrencyDisplay;

    public BattleState state;

    string enemyEncountered;
    // Start is called before the first frame update

    //OnEnable initializes the player unit and sets the background of the battle scene depending on where the user entered battle from.
    void OnEnable()
    {
        playerUnit = GameObject.Find("PlayerPersistency").GetComponent<PlayerPersistency>();
        //check if coming from castle area
        if (GameManager.Instance.prevScene == Scenes.TR_CASTLE_DOOR || GameManager.Instance.prevScene == Scenes.TL_CASTLE_DOOR || GameManager.Instance.prevScene == Scenes.BL_CASTLE_DOOR || GameManager.Instance.prevScene == Scenes.BR_CASTLE_DOOR || GameManager.Instance.prevScene == Scenes.WORLD2) {
            GameObject props = GameObject.FindGameObjectWithTag("props");
            props.SetActive(false);
        }
        GameObject castlebg = GameObject.FindGameObjectWithTag("castle bg");
        castlebg.SetActive(false);
        if (GameManager.Instance.prevScene != Scenes.WORLD2) {
            GameObject world2bg = GameObject.FindGameObjectWithTag("world2bg");
            world2bg.SetActive(false);
        }
            if (GameManager.Instance.prevScene == Scenes.TR_CASTLE_DOOR || GameManager.Instance.prevScene == Scenes.TL_CASTLE_DOOR || GameManager.Instance.prevScene == Scenes.BL_CASTLE_DOOR || GameManager.Instance.prevScene == Scenes.BR_CASTLE_DOOR) {
            castlebg.SetActive(true);
        }
    }
    //Start initializes the fight by setting the correct music to play, setting the state of battle, and starting the next routine setupBattle.
    void Start()
    {
        playerAnimator = playerObj.GetComponentInChildren<Animator>();
        _audio = GetComponent<AudioSource>();
        if(GameManager.Instance.enemyHistory[GameManager.Instance.enemyHistory.Count - 1] == "boss1"
            || GameManager.Instance.enemyHistory[GameManager.Instance.enemyHistory.Count - 1] == "boss2") {
            _audio.clip = bossTheme;
        } else {
            _audio.clip = battleTheme;
        }
        PlayMusic();
        state = BattleState.START;
        sceneTrackerObj = GameObject.FindGameObjectWithTag("Scene Tracker");
        StartCoroutine(SetupBattle());  
    }
    //SetupBattle initializes the player's stats, sets up the correct enemy on screen, sets up the battle HUD, and starts the player's turn.
    IEnumerator SetupBattle()
    {
        // Loop through player attributes and set each attribute value
        for(int i = 0; i < playerUnit.attributes.Length; i++)
        {
            if(playerUnit.attributes[i].type == Attributes.Defense) {
                playerDefense = playerUnit.attributes[i].value;
            }
            if(playerUnit.attributes[i].type == Attributes.Agility) {
                playerAgility = playerUnit.attributes[i].value;
            }
            if(playerUnit.attributes[i].type == Attributes.Strength) {
                playerStrength = playerUnit.attributes[i].value;
            }
            if(playerUnit.attributes[i].type == Attributes.Intellect) {
                playerIntellect = playerUnit.attributes[i].value;
            }
        }

        //Set player in correct position
        playerObj.transform.position = new Vector3(-5.45f, -0.63f, 0f);

        //Spawn correct enemy and initialize stats and reward values
        if(sceneTrackerObj == null) {
            enemyGO = Instantiate(enemyBoss2Prefab, new Vector3(4f, -.2f, -4.85f), Quaternion.identity);
            enemyUnit = enemyGO.GetComponent<Unit>();
            currencyReward = Random.Range(3, 5);
        } else {

            int numOfEnemiesEncountered = GameManager.Instance.enemyHistory.Count - 1;
            enemyEncountered = GameManager.Instance.enemyHistory[numOfEnemiesEncountered];
            if (enemyEncountered == "One")
            {
                enemyOnePrefab.SetActive(true);
                enemyGO = enemyOnePrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(7, 10);
            }
            if (enemyEncountered == "Two")
            {
                enemyTwoPrefab.SetActive(true);
                enemyGO = enemyTwoPrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(7, 10);
            }
            if (enemyEncountered == "Three")
            {
                enemyThreePrefab.SetActive(true);
                enemyGO = enemyThreePrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(7, 10);
            }
            if (enemyEncountered == "Four")
            {
                enemyFourPrefab.SetActive(true);
                enemyGO = enemyFourPrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(3, 5);
            }
            if (enemyEncountered == "Five")
            {
                enemyFivePrefab.SetActive(true);
                enemyGO = enemyFivePrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(5, 7);
            }
            if (enemyEncountered == "Six")
            {
                enemySixPrefab.SetActive(true);
                enemyGO = enemySixPrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(7, 10);
            }
            if (enemyEncountered == "Seven")
            {
                enemySevenPrefab.SetActive(true);
                enemyGO = enemySevenPrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(1, 3);
            }
            if (enemyEncountered == "Eight")
            {
                enemyEightPrefab.SetActive(true);
                enemyGO = enemyEightPrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(3, 5);
            }
            if (enemyEncountered == "Nine")
            {
                enemyNinePrefab.SetActive(true);
                enemyGO = enemyNinePrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(3, 5);
            }
            if (enemyEncountered == "Ten")
            {
                enemyTenPrefab.SetActive(true);
                enemyGO = enemyTenPrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = Random.Range(3, 5);
            }
            if (enemyEncountered == "boss1")
            {
                enemyBossPrefab.SetActive(true);
                enemyGO = enemyBossPrefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = 50;
            }
            if (enemyEncountered == "boss2")
            {
                enemyBoss2Prefab.SetActive(true);
                enemyGO = enemyBoss2Prefab;
                enemyUnit = enemyGO.GetComponent<Unit>();
                currencyReward = 50;
            }
        }

        Vector3 newEnemyScale = new Vector3(3.0f, 3.0f, 3.0f);
        enemyGO.transform.localScale += newEnemyScale;

        //Set up battle HUD
        dialogueText.text = "Enemy approaches!";
        playerHUD.setHUD(playerUnit.maxHP, playerUnit.currentHP);
        enemyHUD.setHUD(enemyUnit.maxHP, enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    
    //PlayerAttack handles the player's attack function. If the attack depletes theenemy's health to 0, the EndBattle coroutine is started. If not, the enemy's turn begins.
    IEnumerator PlayerAttack()
    {

        playerAnimator.SetTrigger("Attack");
        attackSound.PlayDelayed(0.4f);
        state = BattleState.ENEMYTURN;
        int weaponBuff = 0;
        if (playerEquipment.container.Items[4].item.ID != -1)
        {
            weaponBuff = playerEquipment.container.Items[4].item.buffs[0].value;
        }
        int newPlayerDamage = playerUnit.damage + weaponBuff;
        bool isDead = enemyUnit.TakeDamage(newPlayerDamage);
        enemyHUD.setHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";
        attackSuccessSound.Play();
        yield return new WaitForSeconds(2f);
        if(isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        } else
        {
            //state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    /*In EnemyTurn, the enemy will play a certain animation for attacking, and deal a certain amount of damage to the player. If the amount of damage the player takes is enough to defeat them, 
    they lose the battle. If not, the player's turn begtins again.       
    */
    IEnumerator EnemyTurn()
    {
        //Play correct animation for each enemy
        if (enemyEncountered == "One")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy1Attack");
        }
        if (enemyEncountered == "Two")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy2Attack");
        }
        if (enemyEncountered == "Three")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy3Attack");
        }
        if (enemyEncountered == "Four")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy4Attack");
        }
        if (enemyEncountered == "Five")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy5Attack");
        }
        if (enemyEncountered == "Six")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy6Attack");
        }
        if (enemyEncountered == "Seven")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy7Attack");
        }
        if (enemyEncountered == "Eight")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy8Attack");
        }
        if (enemyEncountered == "Nine")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy9Attack");
        }
        if (enemyEncountered == "Ten")
        {
            enemyGO.GetComponent<Animator>().Play("Enemy10Attack");
        }
        if (enemyEncountered == "boss1")
        {
            enemyGO.GetComponent<Animator>().Play("Boss1Attack");
        }
        if (enemyEncountered == "boss2")
        {
            enemyGO.GetComponent<Animator>().Play("Boss2Attack");
        }

        playerAnimator.SetTrigger("Defend");
        defendSound.Play();
        dialogueText.text = "Enemy attacks!";
        yield return new WaitForSeconds(1f);
        int newEnemyDamage = enemyUnit.damage - playerDefense;

        //If statements for handling the amount of damage the player takes
        bool isDead = false;
        if(newEnemyDamage <= 0)
        {
            dialogueText.text = "You took no damage from the attack!";
        }
        if(newEnemyDamage > 0)
        {
            isDead = playerUnit.TakeDamage(newEnemyDamage);
        }
        playerHUD.setHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    //If the user wins, the player will be given currency equal to the enemy's currency value, and the player will be taken to the scene the user was in previoulsy in. If they lost, 
    //the GameOver screen will be shown to the player.
    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            battleWonSound.Play();
            playerUnit.money.addCurrency(currencyReward);
            currencyRewardDisplay.text = currencyReward.ToString();
            wholeCurrencyDisplay.SetActive(true);
            sceneTrackerObj.GetComponent<SceneTracker>().rememberScene();
            yield return new WaitForSeconds(3);
            GameManager.Instance.GoToPreviousScene();
        } 
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated...";
            yield return new WaitForSeconds(1);
            GameOver();
        }
        else
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.GoToPreviousScene();
        }

        DataPersistenceManager.instance.SaveGame();

    }
    //GameOver sets up the game over screen and plays the game over music.
    void GameOver() 
    {
        StopMusic();
        _audio.clip = gameOverTheme;
        PlayMusic();
        GameOverScreen.Setup();
    }
    public void PlayerTurn()
    {
        dialogueText.text = "Choose an Action:";
    }
    
    
    //The ability to heal is old battle functionality no longer used after the addition of inventory use during battle.
    IEnumerator PlayerHeal()
    {
        state = BattleState.ENEMYTURN;
        if(playerUnit.currentHP == playerUnit.maxHP)
        {
            dialogueText.text = "You are already at max health!";
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
        else
        {
            playerUnit.Heal(5);
            playerHUD.setHP(playerUnit.currentHP);
            dialogueText.text = "You heal some HP!";
            yield return new WaitForSeconds(1f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }  
    }
    public void onHealButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;
        //Start player heal method
        StartCoroutine(PlayerHeal());
    }
    
    public void onAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;
        //Start player attack method
        StartCoroutine(PlayerAttack());
    }

    
    //After the user opens their inventory, the user's hp will be changed depending on if they used a healing item. After they close their inventory, 
    //it will be the enemy's turn to prevent the user from using their inventory and attacking in the same turn.
    public void onBattleInventoryBackButton()
    {
        playerHUD.setHP(playerUnit.currentHP);
        if(state != BattleState.PLAYERTURN)
            return;

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    
    public void onBattleInventoryButton()
    {
        //Make sure the user is not entering their inventory during the enemy's turn
        if(state != BattleState.PLAYERTURN)
            return;
    }
    //When the user decides to run, a number will be rolled, giving the user a 65% to run and end the battle. If they are unsuccessful, it becomes the enemy's turn again.
    public void OnRunButton()
    {
        //Make sure the user is running only on their turn
        if(state != BattleState.PLAYERTURN)
            return;

        state = BattleState.ENEMYTURN;

        int runChance = Random.Range(0, 100);
        if(runChance < 65)
        {
            state = BattleState.RAN;
            StartCoroutine(DisplayText("You got away safely!", EndBattle()));
        }
        else
        {
            StartCoroutine(DisplayText("You failed to run away!", EnemyTurn()));
        }
    }
    //Allows the HUD to communicate to the player if their attempt to run was successful
    IEnumerator DisplayText(string text, IEnumerator next = null)
    {
        dialogueText.text = text;
        yield return new WaitForSeconds(2f);
        if(next != null)
            StartCoroutine(next);
    }
    //When the player is defeated, they have the option to respawn in the village. The button they press will allow the player to spawn in the village with half of their health.
    public void onReturnButton() 
    {
        playerUnit.currentHP = 15;
        GameManager.Instance.GoToGameScene(Scenes.VILLAGE);
    }
    public static int returnZero()
    {
        int number = 0;
        return number;
    }

    public void hideEnemy()
    {
        enemyGO.SetActive(false);
    }

    public void showEnemy()
    {
        enemyGO.SetActive(true);    
    }

    public void showInventory()
    {
        if(BattleState.PLAYERTURN == state)
            inventoryOverlay.SetActive(true);
    }

    public void hideInventory()
    {
        inventoryOverlay.SetActive(false);
    }

    //MUSIC INTERFACE
    public void PlayPauseMusic()
    {
        // Check if the music is currently playing.
        if(_audio.isPlaying)
            _audio.Pause(); // Pause if it is
        else
            _audio.Play(); // Play if it isn't
    }
 
    public void PlayStop()
    {
        if(_audio.isPlaying)
            _audio.Stop();
        else
            _audio.Play();
    }
 
    public void PlayMusic()
    {  
        _audio.Play();
    }
 
    public void StopMusic()
    {
        _audio.Stop();
    }
 
    public void PauseMusic()
    {
        _audio.Pause();
    }

    public void LoadData(GameData data)
    {
        // Debug.Log(playerUnit.currentHP);
    }

    public void SaveData(GameData data)
    {
        // data.playerHealth = playerUnit.currentHP;
        // Debug.Log("Save data called with " + playerUnit.currentHP);

    }
}
