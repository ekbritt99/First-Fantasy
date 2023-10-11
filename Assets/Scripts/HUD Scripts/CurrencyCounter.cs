using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyCounter : MonoBehaviour
{

    private TextMeshProUGUI counterText;
    private PlayerPersistency playerPersistency;

    // Start is called before the first frame update
    void Start()
    {
        counterText = this.GetComponent<TextMeshProUGUI>();
        playerPersistency = GameObject.Find("PlayerPersistency").GetComponent<PlayerPersistency>();
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = playerPersistency.money.getCurrency().ToString();
    }
}
