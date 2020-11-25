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
    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void FixedUpdate()
    {
        if (playerStats.isEnteringCombat)
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

            if (Input.GetKeyDown(KeyCode.G))
            {
                _enemyStats.hp--;
                Debug.Log("Enemy took 1 damage");
            }

            switch(swipeControls.direction)
            {
                case SwipeControls.DIRECTION.UP:
                    attack();
                    break;

                case SwipeControls.DIRECTION.DOWN:
                    defend();
                    break;

                case SwipeControls.DIRECTION.LEFT:
                    dodge();
                    break;

                case SwipeControls.DIRECTION.RIGHT:
                    dodge();
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
        Debug.Log("Player Attacks!");
    }

    private void defend()
    {
        Debug.Log("Player Defends!");
    }

    private void dodge()
    {
        Debug.Log("Player Dodges!");
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
    }


}

