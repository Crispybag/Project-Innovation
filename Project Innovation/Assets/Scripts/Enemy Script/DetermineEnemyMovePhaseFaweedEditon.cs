using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineEnemyMovePhaseFaweedEditon : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    public enum ENEMYPHASE
    {
        PATROL = 0,
        ALERT = 1,
        CHASE = 2,
        COMBAT = 3,
        PAUSE = 4
    }


    [Header("Components")]
    public Enemy enemy;
    public GameObject enemyHolder;
    public ChasePlayer chasePlayer;
    public MoveTrail moveTrail;
    public PatrolGroanSound sound;
    [HideInInspector] public GameObject[] _enemies;

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


    [HideInInspector] public ENEMYPHASE enemyPhaseCurrent;
    private ENEMYPHASE enemyPhasePrevious;


    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        determineDistanceToPlayer();
        determinePlayerSpeed();

        enemyPhasePrevious = enemyPhaseCurrent;
        determinePhase();


        if (enemyPhasePrevious != enemyPhaseCurrent)
        {
            switch (enemyPhaseCurrent)
            {
                case ENEMYPHASE.ALERT:
                    sound.playSound(1);
                    chasePlayer.enabled = false;
                    moveTrail.enabled = false;
                    break;

                case ENEMYPHASE.COMBAT:
                    //Necessary value switches
                    Player playerStats = _player.GetComponent<Player>();
                    enemy.inCombat = true;
                    playerStats.SetEnemy(gameObject);
                    playerStats.isEnteringCombat = true;

                    chasePlayer.enabled = false;
                    moveTrail.enabled = false;
                    foreach (GameObject enemy in _enemies)
                    {
                        if (enemy != null)
                        {
                            if (enemy.GetComponent<DetermineEnemyMovePhaseFaweedEditon>())
                            {
                                DetermineEnemyMovePhaseFaweedEditon phase = enemy.GetComponent<DetermineEnemyMovePhaseFaweedEditon>();
                                phase.chasePlayer.enabled = false;
                                phase.moveTrail.enabled = false;
                            }
                        }
                        
                    }
            
                    break;

                case ENEMYPHASE.CHASE:
                    sound.playSound(2);
                    chasePlayer.enabled = true;
                    moveTrail.enabled = false;
                    break;

                case ENEMYPHASE.PATROL:
                    Debug.Log("HI");
                    chasePlayer.enabled = false;
                    moveTrail.enabled = true;
                    moveTrail.GoToNextWavePoint();
                    break;

                case ENEMYPHASE.PAUSE:
                    chasePlayer.enabled = false;
                    moveTrail.enabled = false;
                    break;

            }
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

    //-----------------------------------determinePhase--------------------------------------
    //Determines the phase the enemy is in at the moment
    private void determinePhase()
    {


            if (_distanceToPlayer < detectRadius && _distanceToPlayer > chaseRadius)
            {
                enemyPhaseCurrent = ENEMYPHASE.ALERT;
            }
            else if (_distanceToPlayer < combatRadius && !enemy.isDying)
            {
                enemyPhaseCurrent = ENEMYPHASE.COMBAT;
            }
            else if (_distanceToPlayer < chaseRadius)
            {
                enemyPhaseCurrent = ENEMYPHASE.CHASE;
            }
            else
            {
                enemyPhaseCurrent = ENEMYPHASE.PATROL;
            }
        
        
    }
}
