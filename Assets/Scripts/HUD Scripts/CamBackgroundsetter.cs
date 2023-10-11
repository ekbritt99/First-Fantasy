using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamBackgroundsetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera mainCam = Camera.main;
        if (SceneManager.GetActiveScene().name == "Battle Scene")
        {
            mainCam.backgroundColor = Color.white;
        }
        else
        {
            mainCam.backgroundColor = Color.black;
        }
    }
}
