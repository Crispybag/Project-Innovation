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
    [Tooltip("Joystick that only controls the movement of the player")]
    public Joystick joystick;

    public Rigidbody rb;

    // public variables
    [Header("Variables")]
    [Tooltip("Velocity at which the player moves around")]
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
        rb.AddForce(transform.forward * joystick.Vertical * Time.deltaTime * moveVelocity, ForceMode.Impulse);
        rb.AddForce(transform.right * joystick.Horizontal * Time.deltaTime * moveVelocity, ForceMode.Impulse);
       

    }
}