using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInfoScreen : MonoBehaviour
{
    public void Setup() {
        gameObject.SetActive(true);
    }
    public void Shutdown()
    {
        gameObject.SetActive(false);
    }
}
