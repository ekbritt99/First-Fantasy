using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


/* player controls written with tutorial: 
https://youtu.be/7iYWpzL9GkM
*/

//takes input and handles movement
public class PlayerController : MonoBehaviour
{
    // public GameObject gameManager;

    // public float moveSpeed = 1f;
    // public float collisionOffset = 0.05f;
    // public ContactFilter2D movementFilter;


    // Vector2 movementInput; //2 floats, x and y
    // Rigidbody2D rb;
    // List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // // Start is called before the first frame update
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // //called certain number of times per second instead of once a frame
    // private void FixedUpdate() 
    // {
    //     //if movementInput not 0, try to move
    //     if (movementInput != Vector2.zero) 
    //     {
    //         int count = rb.Cast(
    //             movementInput, //X and Y movement inputted
    //             movementFilter, // settings for collisions (layers etc)
    //             castCollisions, // list of collisions to store found collisions after cast is finished
    //             moveSpeed * Time.fixedDeltaTime + collisionOffset); // amount to cast equal to movement + offset

    //         if (count == 0)
    //         {
    //             //fixedDeltaTime makes sure character moves same speed regardless of fps
    //             rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime); 
    //         } 
            
    //     }

    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         gameManager.SendMessage("GoToInventory");
    //     }
    // }


    // void OnMove(InputValue movementValue) //recieves InputValue (direction) when key is pressed
    // {
    //     movementInput = movementValue.Get<Vector2>();
    // }
}
