using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    #region Camera Variables
    public Transform player;
    public float cameraSpeed = 4.0f;
    public float lookAheadDistance = 5.0f;
    [Range(0.01f, 1.0f)] public float lookAheadLerp = 0.8f;
    private Vector3 targetPoint = Vector3.zero;

    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        //Set the starting Camera position
        targetPoint = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the target point position
        targetPoint = player.position;
        targetPoint.z = 0;

        //Use the players's input to gauge which direction we are moving in
        float moveDirection = Input.GetAxis("Horizontal");

        //Now we want to add the look ahead distance to the target point based on the direction we are moving
        targetPoint = Vector3.Lerp( targetPoint, new Vector3( targetPoint.x + ( lookAheadDistance * moveDirection ), targetPoint.y, targetPoint.z ), lookAheadLerp );

        //Set the camera's position by linearly interpolating between the two positions
        transform.position = Vector3.Lerp(transform.position, targetPoint, cameraSpeed * Time.deltaTime);
    }
    #endregion
}
