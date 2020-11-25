using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleJoyStickMovement : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    [Tooltip("Joystick that moves vertically and rotates horizontally")]
    public Joystick joystick;

    public GameObject player;
    public Rigidbody rb;
    
    // public variables
    [Header("Variables")]
    [Tooltip("Movement Speed of the player")]
    public float movementSpeed = 10;

    [Tooltip("Rotation Speed of the player")]
    public float rotationSpeed = 100;

    [Tooltip("Amount the joystick needs to lean horizontally before it starts moving")]
    [Range(0f, 1f)]
    public float horizontalTreshold = 0.2f;
    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Update()
    {
        rotatePlayer();
        movePlayer();
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    //----------------------------------RotatePlayer-----------------------------------------
    //Rotates the player

    private void rotatePlayer()
    {
        if (Mathf.Abs(joystick.Horizontal) > horizontalTreshold)
        player.transform.Rotate(0, joystick.Horizontal * rotationSpeed * Time.deltaTime, 0);
    }

    //------------------------------------movePlayer-----------------------------------------
    //Movement of a player
    private void movePlayer()
    {
        //transform.position += transform.forward * joystick.Vertical * Time.deltaTime;
        rb.AddForce(transform.forward * joystick.Vertical * Time.deltaTime * movementSpeed, ForceMode.Impulse);
    }

}