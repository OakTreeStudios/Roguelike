using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    #region Camera Variables
    public Transform player;
    public float cameraSpeed = 0.1f;
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

        //Set the camera's position by linearly interpolating between the two positions
        transform.position = Vector3.Lerp(transform.position, targetPoint, cameraSpeed * Time.deltaTime);
    }
    #endregion
}
