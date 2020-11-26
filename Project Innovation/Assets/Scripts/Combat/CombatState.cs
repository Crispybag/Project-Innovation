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
    public MergeWPlayer playerStats;
    public SwipeControls swipeControls;
    // public variables
    //[Header("Variables")]

    // private objects
    private GameObject _enemy;
    private Enemy _enemyStats;
    private EnemyCombat _enemyCombat;
    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void FixedUpdate()
    {
        if (playerStats.isEnteringCombat && !playerStats.canCombat)
        {
            Debug.Log("it reached combat state");
            playerStats.isEnteringCombat = false;
            StartCombat();
        }


        if (playerStats.canCombat)
        {
            if (_enemyStats.hp <= 0)
            {
                LeaveCombat();
                Destroy(_enemy);
            }

            //Quick garbage code
            if (Input.GetKeyDown(KeyCode.T))
            {
                _enemyStats.hp--;
                Debug.Log("Enemy took 1 damage");
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                dodge();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                defendLeft();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                defendRight();
            }


            switch(swipeControls.direction)
            {
                case SwipeControls.DIRECTION.UP:
                    attack();
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
        }
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    private void attack()
    {
        if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.NOTHING)
        {
            _enemyStats.hp--;
        }
        Debug.Log("Player Attacks!");
    }

    private void defendLeft()
    {
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.SLASHLEFT)
            {
                _enemyCombat.goToNextAction();
            }
            
            else
            {
                playerStats.hp--;
            }
            Debug.Log("Player Defends Left!");
        }
    }

    private void defendRight()
    {
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.SLASHRIGHT)
            {
                _enemyCombat.goToNextAction();
            }
            
            else
            {
                playerStats.hp--;

            }
            Debug.Log("Player Defends Right!");
        }
    }

    private void dodge()
    {
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.BASH)
            {
                _enemyCombat.goToNextAction();
            }
            else
            {
                playerStats.hp--;
            }
            Debug.Log("Player Dodges!");
        }
    }
    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
    //-----------------------------------SetEnemy-----------------------------------------
    //Sets the enemy variable to the enemy that the player is fighting
    private void setEnemy()
    {
        _enemy = playerStats.getEnemy();
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


}

