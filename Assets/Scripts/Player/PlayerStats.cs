using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    #region Player Stats

    public Rigidbody2D rb;
    public int maxHealth = 100;
    public int currentHealth;

    public float movementSpeed = 9.0f;
    public float jumpForce = 5.0f;
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
