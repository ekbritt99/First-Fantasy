using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CopyCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Test Village Scene")
        {
            Camera thisCamera = this.GetComponent<Camera>();
            thisCamera.CopyFrom(Camera.main);
            thisCamera.depth = -2;

            RectTransform mainTransform = Camera.main.GetComponent<RectTransform>();
            RectTransform thisTransform = thisCamera.GetComponent<RectTransform>();
            thisTransform = mainTransform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
