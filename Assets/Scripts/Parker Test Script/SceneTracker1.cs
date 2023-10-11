using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker1 : MonoBehaviour
{

    public List<int> sceneHistory = new List<int>();
    public List<string> enemyHistory = new List<string>();
    public List<Vector3> positionHistory = new List<Vector3>();

    private void Awake()
    {
        int numSceneTrackers = FindObjectsOfType<SceneTracker>().Length;
        if (numSceneTrackers != 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rememberScene()
    {
        sceneHistory.Add(SceneManager.GetActiveScene().buildIndex);
    }

    public void rememberEnemy(string enemyType)
    {
        enemyHistory.Add(enemyType);
    }

    public void rememberPosition(Vector3 playerPosition)
    {
        positionHistory.Add(playerPosition);
    }
}
