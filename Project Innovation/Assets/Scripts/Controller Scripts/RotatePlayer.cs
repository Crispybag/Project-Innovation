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
    [Tooltip("Joystick that only controls rotation")]
    public Joystick joystick;
    public GameObject player;

    // public variables
    [Header("Variables")]
    [Tooltip("Value for rotating speed")]
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
        player.transform.Rotate(0, joystick.Horizontal * rotationSpeed * Time.deltaTime, 0);
    }

}