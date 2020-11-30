using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : MonoBehaviour
{
    //Determine when combat state should start
    //Player can't move
    //Enemies can't move
    //Fix position of enemy in relation to player
    //Fix rotation of player in relation to enemy

    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    //[Header("Components")]
    public Player playerStats;
    public SwipeControls swipeControls;
    public TapScreen tapScreen;
    // public variables
    //[Header("Variables")]
    private float timeHoldingDown;

    // private objects
    private GameObject _enemy;
    private Enemy _enemyStats;
    private EnemyCombat _enemyCombat;
    // private variables
    CombatSounds combatSounds;
    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Start()
    {
        combatSounds = GetComponent<CombatSounds>();   
    }
    private void FixedUpdate()
    {
        
        if (playerStats.isEnteringCombat && !playerStats.canCombat)
        {
            playerStats.isEnteringCombat = false;
            StartCombat();
        }


        if (playerStats.canCombat)
        {
            //registerTap();
            if (_enemyStats.hp <= 0)
            {
                LeaveCombat();
                Destroy(_enemy);
            }


            //Quick garbage code
            if (Input.GetKeyDown(KeyCode.T) || Input.GetMouseButtonDown(0))
            {
                attack();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
            {
                dodge();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                defendLeft();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                defendRight();
            }


            switch(swipeControls.direction)
            {
                case SwipeControls.DIRECTION.UP:
                    //attack();
                    break;

                case SwipeControls.DIRECTION.DOWN:
                    dodge();
                    break;

                case SwipeControls.DIRECTION.LEFT:
                    defendLeft();
                    break;

                case SwipeControls.DIRECTION.RIGHT:
                    defendRight();
                    break;

                default:
                    break;
            }
            if (tapScreen.isTapping)
            {
                attack();
            }
        }
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    private void attack()
    {
            _enemyStats.hp--;
            combatSounds.playSound(0);
    }

    private void defendLeft()
    {
        
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.SLASHLEFT)
            {
                Debug.Log("Defending Left");
                combatSounds.playSound(1);
                _enemyCombat.attackFailed = true;
            }
            
            else
            {
                playerStats.hp--;
                combatSounds.playSound(4);
            }
        }
    }

    private void defendRight()
    {
        
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.SLASHRIGHT)
            {
                combatSounds.playSound(1);
                Debug.Log("Defending Right");
                _enemyCombat.attackFailed = true;
            }
            
            else
            {
                playerStats.hp--;
                combatSounds.playSound(4);

            }
        }
    }

    private void dodge()
    {
        
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.BASH)
            {
                Debug.Log("Dodge");
                combatSounds.playSound(2);
                _enemyCombat.attackFailed = true;
            }
            else
            {
                playerStats.hp--;
                combatSounds.playSound(4);
            }
        }
    }
    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
    //-----------------------------------SetEnemy-----------------------------------------
    //Sets the enemy variable to the enemy that the player is fighting
    private void setEnemy()
    {
        _enemy = playerStats.GetEnemy();
        _enemyStats = _enemy.GetComponent<Enemy>();
        _enemyCombat = _enemy.GetComponent<EnemyCombat>();

    }

    public void StartCombat()
    {
        setEnemy();
        Player.canMove = false;
        playerStats.canCombat = true;

    }

    public void LeaveCombat()
    {
        Player.canMove = true;
        playerStats.canCombat = false;
        playerStats.isEnteringCombat = false;
    }



    /*
    private void registerTap()
    {
        
        isTapping = false;
        if (Input.touchCount == 0)
        {
            timeHoldingDown = 0;
        }

        else
        {
            timeHoldingDown += Time.deltaTime;
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && timeHoldingDown < maxTapTime)
            {
                isTapping = true;
                Debug.Log("Tap Registered");
                timeHoldingDown = 0;
            }
        }
    }
    */
}

