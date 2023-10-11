using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    
    private Animator animator;
    private BoundaryManager boundaryManager;

    [SerializeField] private float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    

    void Start()
    {
        Camera mainCamera = Camera.main;
        if(mainCamera != null)
        {
            boundaryManager = mainCamera.GetComponent<BoundaryManager>();
        }

        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // void moveCommand(Vector3 moveVect)
    //     {
    //         //MOVE
    //         transform.Translate(moveVect);
    //         animator.SetBool("Walk", true);
    //     }

    // // Update is called once per frame
    // void Update()
    // {
    //     Vector3 scale = this.gameObject.transform.localScale;
    //     float inputX = Input.GetAxis("Horizontal");
    //     float inputY = Input.GetAxis("Vertical");
    //     Vector3 moveVect = new Vector3(inputX, inputY, 0);
    //     moveVect *= (moveSpeed * Time.deltaTime);

    //     // If moving right, make sprite face right
    //     if(moveVect.x > 0)
    //     {
    //         this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
    //     } else if(moveVect.x < 0){
    //         this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
    //     }
        
    //     if(moveVect != Vector3.zero)
    //         moveCommand(moveVect);
    //     else
    //         animator.SetBool("Walk", false);

    //     //moved to a separate method to call in testing
    //     // transform.Translate(moveVect);

    // }

    // Using FixedUpdate for physics calculations, movement in this case.
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        // If moving right, make sprite face right
        if(movement.x > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        } else if(movement.x < 0){
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        // If moving, set walk animation to true
        if(movement != Vector2.zero)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);

        rb.velocity = movement * moveSpeed;

        // Clamp the player's position to the camera's view if boundaryManager is set
        if(boundaryManager != null && boundaryManager.isActiveAndEnabled)
        {
            ClampPosition();
        }
    }

    private void ClampPosition()
    {
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, boundaryManager.xMin, boundaryManager.xMax),
            Mathf.Clamp(transform.position.y, boundaryManager.yMin, boundaryManager.yMax)
        );
        transform.position = clampedPosition;
    }
    
}
