using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public Joystick joystick;

    // public variables
    [Header("Variables")]
    public float moveVelocity = 1f;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Update()
    {
        movePlayer();
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    //------------------------------------movePlayer-----------------------------------------
    //Movement of a player
    private void movePlayer()
    {
        transform.position += transform.forward * joystick.Vertical * Time.deltaTime;
        transform.position += transform.right * joystick.Horizontal * Time.deltaTime;
    }
}