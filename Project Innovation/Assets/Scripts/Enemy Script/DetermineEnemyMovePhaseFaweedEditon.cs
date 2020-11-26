using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineEnemyMovePhaseFaweedEditon : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public Enemy enemy;
    public GameObject enemyHolder;
    public ChasePlayer chasePlayer;
    public MoveTrail moveTrail;
    //public AudioSource aSource;
    //public AudioSource aSourcefs;

    // public variables
    [Header("Variables")]
    [Tooltip("Radius at which the enemy stops and looks at the player")]
    [Range(0f, 10f)]
    public float detectRadius = 6f;

    [Tooltip("Radius at which the enemy starts chasing the player")]
    [Range(0f, 10f)]
    public float chaseRadius = 5f;

    [Tooltip("Radius at which the enemy starts chasing the player")]
    [Range(0f, 10f)]
    public float combatRadius = 2f;

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



        if (_distanceToPlayer < detectRadius && _distanceToPlayer > chaseRadius)
        {
            chasePlayer.enabled = false;
            moveTrail.enabled = false;
        }

        //Initiate combat
        else if (_distanceToPlayer < combatRadius)
        {
            //Necessary value switches
            MergeWPlayer playerStats = _player.GetComponent<MergeWPlayer>();
            enemy.inCombat = true;
            playerStats.SetEnemy(gameObject);
            playerStats.isEnteringCombat = true;

            chasePlayer.enabled = false;
            moveTrail.enabled = false;
         
        }

        else if (_distanceToPlayer < chaseRadius)
        {
            chasePlayer.enabled = true;
            moveTrail.enabled = false;
        }

        else if (_distanceToPlayer > detectRadius && !moveTrail.enabled)
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
