using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineEnemyMovePhase : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public GameObject enemyHolder;
    public ChasePlayer chasePlayer;
    public MoveTrail moveTrail;

    // public variables
    [Header("Variables")]
    public float chaseRadius = 5f;
    public float speedTreshold = 5f;

    // private objects
    private GameObject _player;

    //private variables
    private float _distanceToPlayer;
    private float _playerSpeed;
    private Vector3 _previousLocation;
    

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        determineDistanceToPlayer();
        determinePlayerSpeed();
        
        if (_distanceToPlayer < chaseRadius && _playerSpeed > speedTreshold)
        {
            chasePlayer.enabled = true;
            moveTrail.enabled = false;
        }

        else if (_distanceToPlayer > chaseRadius && chasePlayer.enabled)
        {
            chasePlayer.enabled = false;
            moveTrail.enabled = true;
            moveTrail.GoToNextWavePoint();
        }
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    //-------------------------------determinePlayerSpeed------------------------------------
    //Determine the speed of the player, comparing with previous frame
    private void determinePlayerSpeed()
    {
        if (_previousLocation != new Vector3(0, 0, 0))
        {
            _playerSpeed = (_player.transform.position - _previousLocation).magnitude;
        }
        _previousLocation = _player.transform.position;


    }

    //-----------------------------determineDistanceToPlayer---------------------------------
    //Determines distance between enemy and player
    private void determineDistanceToPlayer()
    {
        _distanceToPlayer = (_player.transform.position - transform.position).magnitude;
        
    }
}