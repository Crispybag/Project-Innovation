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
    public Joystick joystick;
    public Rigidbody rb;

    // public variables
    [Header("Variables")]
    public float movementSpeed = 10;
    public float rotationSpeed = 100;

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
        transform.Rotate(0, joystick.Horizontal * rotationSpeed * Time.deltaTime, 0);
    }

    //------------------------------------movePlayer-----------------------------------------
    //Movement of a player
    private void movePlayer()
    {
        //transform.position += transform.forward * joystick.Vertical * Time.deltaTime;
        rb.AddForce(transform.forward * joystick.Vertical * Time.deltaTime * movementSpeed, ForceMode.Impulse);
    }

}