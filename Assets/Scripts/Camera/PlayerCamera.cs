using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    #region Camera Variables
    public Transform player;

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
        //Update the camera's position
        targetPoint = player.position;

        //Set the camera's position
        transform.position = new Vector3(targetPoint.x, targetPoint.y, -1);
    }
    #endregion
}
