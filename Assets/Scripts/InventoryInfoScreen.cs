using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfoScreen : MonoBehaviour
{
    public void Setup() {
        gameObject.SetActive(true);
    }
    public void Shutdown()
    {
        gameObject.SetActive(false);
    }
}
