using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LimitEnemyLifetime : MonoBehaviour
{
    //variable which will store the random lifetime of an enemy
    public float randomLifeTime;
    //boool to check if the player has collided with this enemy
    bool hasCollidedWithPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //get a random lifetime for this enemy that ranges from 2 to 10 seconds
        randomLifeTime = Random.Range(2.0f, 10.0f);
        //initial state of the enemy is that it has not collided with the player
        hasCollidedWithPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player is in a scene where enemies should be spawning
        if (SceneManager.GetActiveScene().name == "Test World Scene" || SceneManager.GetActiveScene().name == "Top Left Castle Room Scene" || SceneManager.GetActiveScene().name == "Top Right Castle Room Scene" 
        || SceneManager.GetActiveScene().name == "Bottom Left Castle Room Scene" || SceneManager.GetActiveScene().name == "Bottom Right Castle Room Scene" || SceneManager.GetActiveScene().name == "World Scene 2")
        {
            //only carry out the countdown of the enemies lifetime if it has been spawned
            if (transform.position.x != -11.0f)
            {
                //count down the enemies lifetime
                randomLifeTime -= Time.deltaTime;
            }

            //if the enemy has reached the end of its lifetime
            if (randomLifeTime <= 0.0f)
            {
                //check if the player has collided with the enemy
                if (hasCollidedWithPlayer == false)
                {
                    //despawn the the enemy and get a new random lifetime
                    transform.position = new Vector3(-11.0f, 0.0f, 1.0f);
                    randomLifeTime = Random.Range(5.0f, 10.0f);
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the player has collided with this enemy, update the bool which is used to check it
        if (collision.gameObject.tag == "Player")
        {
            hasCollidedWithPlayer = true;
        }
    }
}
