using UnityEngine;

/*
* This script is used to control the player's actions/inputs
*/

public class PlayerMovementController : MonoBehaviour
{

    //Player Rigidbody
    public Rigidbody2D playerRb;

    //Player input variables
    private Vector2 inputMovement;
    
    //Player Run Variables
    public float movementSpeed  = 9.0f;
    public float acceleration   = 9.0f;
    public float deceleration   = 9.0f;
    public float  velocityPower  = 1.2f;

    //Player Jump Variables
    public float jumpForce = 12.0f;
    
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    private bool isGrounded;

    private bool jumping = false;



    // Start is called before the first rendered frame update
    void Start()
    {

    }

    // Update is called once per frame rendered frame
    void Update()
    {
        //Get our player's inputs
        inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal, 0.0f, groundLayer);

        //Check if the player has pressed the jump button and the player is grounded
        if(Input.GetButtonDown("Jump") && isGrounded) {
            jumping = true;
        }
    }

    //FixedUpdate is called once per physics frame
    void FixedUpdate() {
        MovePlayer();
    }

    //Function for managing player movement
    private void MovePlayer() {
        PlayerRun();

        PlayerJump();
    }


    //Function for managing player running (horizontal movement)
    private void PlayerRun() {
        //Following Dawnosaur's calculations for more responsive physics movement: https://www.youtube.com/watch?v=KbtcEVCM7bw

        //Calculate our intended velocity
        float targetSpeed = inputMovement.x * movementSpeed;

        //Calculate the difference in our current velocity and desired velocity
        float speedDifference = targetSpeed - playerRb.velocity.x;

        //Find our acceleration rate depending on the situation (Accelerating or decelerating)
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        //Applies acceleration to speed difference, then raises to a set power so acceleration increases with higher speeds
        //Reapply the velocities direction using sign
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, velocityPower) * Mathf.Sign(speedDifference);

        //Add movement forces to player rigidbody
        playerRb.AddForce(movement * Vector2.right);
    }

    //Function for managing player jumping
    private void PlayerJump() {
        //If we are not jumping, return
        if(!jumping) return;

        //Apply a force if we are jumping
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumping = false;
        
    }

}




