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
    [Header("Components")]
    public Player playerStats;
    public SwipeControls swipeControls;
    public TapScreen tapScreen;

    // private objects
    private GameObject _enemy;
    private Enemy _enemyStats;
    private EnemyCombat _enemyCombat;
    private CombatSounds _combatSounds;

    //private variables
    private float timeHoldingDown;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Start()
    {
        _combatSounds = GetComponent<CombatSounds>();
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
            if (_enemyStats.hp <= 0)
            {
                LeaveCombat();
                Destroy(_enemy);
            }


            keyboardControls();
            switch (swipeControls.direction)
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
    //-----------------------------------Attack-----------------------------------------
    //Attack enemy
    private void attack()
    {
        _enemyStats.hp--;
        _combatSounds.playSound(0);
    }

    //---------------------------------DefendLeft---------------------------------------
    //Defend left when enemy attacks left
    private void defendLeft()
    {

        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.SLASHLEFT)
            {
                Debug.Log("Defending Left");
                _combatSounds.playSound(1);
                _enemyCombat.attackFailed = true;
            }

            else
            {
                playerStats.hp--;
                _combatSounds.playSound(4);
            }
        }
    }

    //---------------------------------DefendRight--------------------------------------
    //Defend right when the enemy attacks right
    private void defendRight()
    {

        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.SLASHRIGHT)
            {
                _combatSounds.playSound(1);
                Debug.Log("Defending Right");
                _enemyCombat.attackFailed = true;
            }

            else
            {
                playerStats.hp--;
                _combatSounds.playSound(4);

            }
        }
    }

    //------------------------------------Dodge-------------------------------------------
    //Dodge the attack when enemy attacks from the front
    private void dodge()
    {

        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING)
        {
            if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.BASH)
            {
                Debug.Log("Dodge");
                _combatSounds.playSound(2);
                _enemyCombat.attackFailed = true;
            }
            else
            {
                playerStats.hp--;
                _combatSounds.playSound(4);
            }
        }
    }

    //-------------------------------KeyboardControls------------------------------------
    //Keyboard Controls in combat
    private void keyboardControls()
    {
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

    //----------------------------------StartCombat---------------------------------------
    //Sets the enemy variable to the enemy that the player is fighting
    public void StartCombat()
    {
        setEnemy();
        Player.canMove = false;
        playerStats.canCombat = true;

    }

    //----------------------------------LeaveCombat---------------------------------------
    //Sets the enemy variable to the enemy that the player is fighting
    public void LeaveCombat()
    {
        Player.canMove = true;
        playerStats.canCombat = false;
        playerStats.isEnteringCombat = false;
    }
}

