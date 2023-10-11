using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        //sets the camera for a canvas to be the main camera to render and scale correctly 
        Canvas thisCanvas = this.GetComponent<Canvas>();
        //Camera sceneCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        thisCanvas.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
