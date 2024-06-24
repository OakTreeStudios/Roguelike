using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        
        
    }

    //Function for managing player movement
    private void MovePlayer() {
        //Move player Left
        if(Input.GetKey(KeyCode.A)) {
            Debug.Log("-- A key was pressed");
            playerRb.velocity = new Vector2(-speed, playerRb.velocity.y);
        } 
        //Move player Right
        if(Input.GetKey(KeyCode.D)) {
            Debug.Log("-- D key was pressed");
            playerRb.velocity = new Vector2(speed, playerRb.velocity.y);
        }

        //Move player Up
        //Move player Down

    }

}




