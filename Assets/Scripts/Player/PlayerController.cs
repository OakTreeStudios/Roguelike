using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Callbacks;
using UnityEngine;

/*
* This script is used to control the player's actions/inputs
*/

// const float GRAVITY = 9.8f;

public class PlayerController : MonoBehaviour
{

    
    //Public script variables
    public float speed = 5.0f;
    //public float jumpForce = 5.0f;
    public Rigidbody2D playerRb;

    //Private script variabes
    private Vector2 playerMovement;
    

    // Start is called before the first rendered frame update
    void Start()
    {

    }

    // Update is called once per frame rendered frame
    void Update()
    {
        //Get our player's inputs
        playerMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
 
    }

    //FixedUpdate is called once per physics frame
    void FixedUpdate() {
        MovePlayer();
    }

    //Function for managing player movement
    private void MovePlayer() {
        //Add movement forces to player rigidbody
        playerRb.MovePosition(
            playerRb.position + new Vector2(
                playerMovement.x * speed * Time.fixedDeltaTime,  //X axis movement
                playerMovement.y * speed * Time.fixedDeltaTime     //Y axis movement
            )
        );
    }

}




