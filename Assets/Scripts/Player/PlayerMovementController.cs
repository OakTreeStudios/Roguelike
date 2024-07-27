using UnityEngine;

/*
* This script is used to control the player's actions/inputs
*/

public class PlayerMovementController : MonoBehaviour
{

    #region Variables
    //Player Rigidbody
    public Rigidbody2D playerRb;

    //Gravity Scale
    private float originalGravityScale;

    //Player input variables
    private Vector2 inputMovement;
    
    [Header("Player Run")]
    public float movementSpeed  = 9.0f;
    public float acceleration   = 9.0f;
    public float deceleration   = 9.0f;
    public float velocityPower  = 1.2f;
    public float frictionFactor = 0.2f;

    private bool stopFriction   = false;

    [Header("Player Jump")]
    public float jumpForce              = 12.0f;
    public float jumpCutMultiplier      = 0.1f;
    public float fallGravityMultiplier  = 1.9f;
    public float jumpCoyoteTime         = 0.35f;
    public int numberOfJumps            = 1;
    public float jumpWaitTime           = 0.15f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private int currentJumps            = 0;
    private float lastJumpTime          = 0.0f;
    private float lastGroundedTime      = 0.0f;
    private bool isGrounded;
    private bool jumpInput              = false;
    private bool jumpInputReleased      = false;
    private bool isJumping              = false;

    #endregion

    #region Unity Functions
    // Start is called before the first rendered frame update
    void Start()
    {
        //Get our starting gravity scale
        originalGravityScale = playerRb.gravityScale;
    }

    // Update is called once per frame rendered frame
    void Update()
    {
        //Timers
        lastJumpTime += Time.deltaTime;
        lastGroundedTime += Time.deltaTime;
        
        //Get our player's inputs for movement
        inputMovement = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );

        //Check if the player is grounded
        isGrounded = Physics2D.OverlapCapsule( 
            groundCheck.position, 
            new Vector2( 1.0f, 0.1f ), 
            CapsuleDirection2D.Horizontal, 
            0.0f, 
            groundLayer 
        );

        //Set Jump Input if the player presses the jump button and is grounded or within the coyote time
        if( Input.GetButtonDown( "Jump" ) && (isGrounded  || currentJumps < numberOfJumps) ) {
            jumpInput = true;
        }

        if( Input.GetButtonUp( "Jump" ) ) {
            jumpInputReleased = true;
        }

        //If we are not moving and grounded, stop friction
        if( ( playerRb.velocity == Vector2.zero || Input.GetAxisRaw( "Horizontal" ) != 0.0f ) && isGrounded) {
            stopFriction = false;
        }
    }
    
    //FixedUpdate is called once per physics frame
    void FixedUpdate() {

        GroundedCheck();
        MovePlayer();
    }

    #endregion

    #region Player Movement Physics Functions
    //Function for managing player movement
    private void MovePlayer() {
        PlayerRun();

        StopFriction();

        PlayerJump();

        JumpCut();

        FallGravity();
    }
    #endregion

    #region Run Functions
    //Function for managing player running (horizontal movement)
    private void PlayerRun() {
        //Following Dawnosaur's calculations for more responsive physics movement: https://www.youtube.com/watch?v=KbtcEVCM7bw

        //Calculate our intended velocity
        float targetSpeed = inputMovement.x * movementSpeed;

        //Calculate the difference in our current velocity and desired velocity
        float speedDifference = targetSpeed - playerRb.velocityX;

        //Find our acceleration rate depending on the situation (Accelerating or decelerating)
        float accelerationRate = ( Mathf.Abs( targetSpeed ) > 0.01f ) ? acceleration : deceleration;

        //Applies acceleration to speed difference, then raises to a set power so acceleration increases with higher speeds
        //Reapply the velocities direction using sign
        float movement = Mathf.Pow( Mathf.Abs( speedDifference ) * accelerationRate, velocityPower ) * Mathf.Sign (speedDifference );

        //Add movement forces to player rigidbody
        playerRb.AddForce( movement * Vector2.right );
    }

    private void StopFriction() {
        //Following Dawnosaur's calculations for more responsive physics movement: https://www.youtube.com/watch?v=KbtcEVCM7bw

        //If we are applying stop friction, calculate the amount of friction to apply
        if( stopFriction ) {
            float amount = Mathf.Min( Mathf.Abs( playerRb.velocityX ), frictionFactor );

            amount *= Mathf.Sign( playerRb.velocityX );

            playerRb.AddForce( -amount * Vector2.right, ForceMode2D.Impulse);
        }
    }

    #endregion

    #region Jump Functions
    //Function for managing player jumping
    private void PlayerJump() {

        //Apply a force if we are jumping
        if( CanJump() ) {
            float force = jumpForce;

            //Check if we are falling, if we are then we will adjust the force to make the jump be the same height
            if( playerRb.velocityY < 0 ) {
                force -= playerRb.velocityY;
            }

            playerRb.AddForce( Vector2.up * force, ForceMode2D.Impulse );

            jumpInput = false;
            lastJumpTime = 0.0f;
            isJumping = true;
            jumpInputReleased = false;
            currentJumps++;
        }
    }

    private void JumpCut() {
        //Check if we are jumping
        if( playerRb.velocityY > 0 && jumpInputReleased) {
            playerRb.AddForce( Vector2.down * ( 1 - jumpCutMultiplier ) * playerRb.velocityY, ForceMode2D.Impulse );
        }
    }

    private void FallGravity() {
        if(playerRb.velocityY < 0) {
            playerRb.gravityScale = originalGravityScale * fallGravityMultiplier;
        } else {
            playerRb.gravityScale = originalGravityScale;
        }
    }

    private void GroundedCheck() {
        if( isGrounded ) {
            lastGroundedTime = 0.0f;
            isJumping = false;
            currentJumps = 0;
        }
    }

    #endregion

    #region Ability Checks

    //Function that returns wether or not the jumping condition is met.
    bool CanJump() {
        return jumpInput 
        //Check if we have jumps left
        && currentJumps < numberOfJumps 
        //Check if it has been enough time between jumps
        && lastJumpTime > jumpWaitTime;
    }

    #endregion


}




