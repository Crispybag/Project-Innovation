using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public Joystick joystick;

    // public variables
    [Header("Variables")]
    public float rotationSpeed = 5;


    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Update()
    {
        rotatePlayer();
    }

    //=======================================================================================
    //                               >  Update Functions  <
    //=======================================================================================

    //----------------------------------RotatePlayer-----------------------------------------
    //Rotates the player
    private void rotatePlayer()
    {
        transform.Rotate(0, joystick.Horizontal * rotationSpeed * Time.deltaTime, 0);
    }

}