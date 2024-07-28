using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    #region Player Stats

    [Header("Physics Components")]
    public Rigidbody2D rb;

    [Header("Health Stats")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Movement Stats")]

    public float movementSpeed = 9.0f;
    public float jumpForce = 5.0f;
    public float wallJumpForce = 5.0f;
    public float wallJumpLerp = 0.15f;
    public float numberOfJumps = 1;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
