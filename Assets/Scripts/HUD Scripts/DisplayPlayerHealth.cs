using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerHealth : MonoBehaviour
{
    public TMP_Text hpDisplay;
    GameObject playerStatsObj;
    PlayerPersistency playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStatsObj = GameObject.Find("PlayerPersistency");
        playerStats = playerStatsObj.GetComponent<PlayerPersistency>();
    }

    // Update is called once per frame
    void Update()
    {
        hpDisplay.text = playerStats.currentHP.ToString();
    }
}
