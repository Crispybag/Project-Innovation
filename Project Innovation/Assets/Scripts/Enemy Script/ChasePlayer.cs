using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    //[Header("Components")]

    // public variables
    [Header("Variables")]
    public float chaseSpeed = 2f;
    // private objects
    private GameObject _player;


    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        moveTowardsPlayer();
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    private void moveTowardsPlayer()
    {
        transform.LookAt(_player.transform);
        transform.position += transform.forward * Time.deltaTime * chaseSpeed; 
    }
}