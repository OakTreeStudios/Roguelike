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
    public Transform groundCheck;
    public LayerMask groundLayer;
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

        if( isGrounded ) {
            lastGroundedTime = 0.0f;
            isJumping = false;
        }

        //Set Jump Input if the player presses the jump button and is grounded or within the coyote time
        if( Input.GetButtonDown( "Jump" ) && (isGrounded || lastGroundedTime < jumpCoyoteTime) ) {
            jumpInput = true;
            lastJumpTime = 0.0f;
        }

        if( Input.GetButtonUp( "Jump" ) ) {
            jumpInputReleased = true;
        }

        //Stop friction for player movement (No input and grounded)
        if ( isGrounded 
            && Input.GetAxisRaw( "Horizontal" ) == 0.0f 
            && Input.GetAxisRaw( "Vertical" ) == 0.0f 
        ) {
            stopFriction = true;
        }
    }
    
    //FixedUpdate is called once per physics frame
    void FixedUpdate() {
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

        //Check if we should stop friction (Velocity is 0 and grounded)
        if(playerRb.velocity == Vector2.zero && isGrounded) {
            stopFriction = false;
        }

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
        //If we are not jumping, return
        if(!jumpInput) return;

        //Apply a force if we are jumping
        playerRb.AddForce( Vector2.up * jumpForce, ForceMode2D.Impulse );
        jumpInput = false;
        lastJumpTime = 0.0f;
        isJumping = true;
        jumpInputReleased = false;
        
    }

    private void JumpCut() {
        //Check if we are jumping
        if( playerRb.velocityY > 0 /*&& isJumping*/ && jumpInputReleased) {
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

    #endregion


}




