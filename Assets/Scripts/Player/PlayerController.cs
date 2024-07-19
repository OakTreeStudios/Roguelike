using UnityEngine;

/*
* This script is used to control the player's actions/inputs
*/

// const float GRAVITY = 9.8f;

public class PlayerController : MonoBehaviour
{

    
    //Public script variables
    public float movementSpeed  = 9.0f;
    public float acceleration   = 9.0f;
    public float deceleration   = 9.0f;
    public float  velocityPower  = 1.2f;
    
    //public float jumpForce = 5.0f;
    public Rigidbody2D playerRb;

    //Private script variabes
    private Vector2 inputMovement;
    

    // Start is called before the first rendered frame update
    void Start()
    {

    }

    // Update is called once per frame rendered frame
    void Update()
    {
        //Get our player's inputs
        inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
 
    }

    //FixedUpdate is called once per physics frame
    void FixedUpdate() {
        MovePlayer();
    }

    //Function for managing player movement
    private void MovePlayer() {
        //Horizontal movements
        PlayerRun();

        //Vertical movements
    }

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

}




