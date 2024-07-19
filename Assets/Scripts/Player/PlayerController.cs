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
    

    // Start is called before the first rendered frame update
    void Start()
    {

    }

    // Update is called once per frame rendered frame
    void Update()
    {
 
    }

    //FixedUpdate is called once per physics frame
    void FixedUpdate() {
        MovePlayer();
    }

    //Function for managing player movement
    private void MovePlayer() {
        //Add force to player rigidbody using input
        playerRb.MovePosition(
            playerRb.position + new Vector2(
                Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime,  //X axis movement
                Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime     //Y axis movement
            )
        );
    }

}




