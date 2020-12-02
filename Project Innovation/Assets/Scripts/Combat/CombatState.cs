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
    public float cooldownAttack = 0.5f;
    // private objects
    private GameObject _enemy;
    private Enemy _enemyStats;
    private EnemyCombat _enemyCombat;
    private CombatSounds _combatSounds;
    private float lastAttack;
    private float timer;
    //private variables
    private float timeHoldingDown;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Start()
    {
        _combatSounds = GetComponent<CombatSounds>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

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
            if (tapScreen.isTapping && timer - lastAttack > cooldownAttack)
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
        if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.NOTHING)
        {
            _enemyStats.hp--;
            _combatSounds.playSound(0);
            lastAttack = timer;
        }
        else
        {

        }
    }

    //---------------------------------DefendLeft---------------------------------------
    //Defend left when enemy attacks left
    private void defendLeft()
    {
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.TUTORIAL)
        {
            if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING && !_enemyCombat.playerHasActed)
            {
                if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.SLASHLEFT)
                {
                    _combatSounds.playSound(1);
                    _enemyCombat.attackFailed = true;
                    _enemyCombat.playerHasActed = true;

                }

                else
                {
                    _combatSounds.playSound(3);
                    _enemyCombat.playerHasActed = true;
                }
            }
        }
    }

    //---------------------------------DefendRight--------------------------------------
    //Defend right when the enemy attacks right
    private void defendRight()
    {
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.TUTORIAL)
        {
            if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING && !_enemyCombat.playerHasActed)
            {
                if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.SLASHRIGHT)
                {
                    _combatSounds.playSound(1);
                    _enemyCombat.attackFailed = true;
                    _enemyCombat.playerHasActed = true;

                }

                else
                {
                    _combatSounds.playSound(3);
                    _enemyCombat.playerHasActed = true;


                }
            }
        }
    }

    //------------------------------------Dodge-------------------------------------------
    //Dodge the attack when enemy attacks from the front
    private void dodge()
    {
        if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.TUTORIAL)
        {
            if (_enemyCombat.currentAction != EnemyCombat.FIGHTACTION.NOTHING && !_enemyCombat.playerHasActed)
            {
                if (_enemyCombat.currentAction == EnemyCombat.FIGHTACTION.BASH)
                {
                    _combatSounds.playSound(2);
                    _enemyCombat.attackFailed = true;
                    _enemyCombat.playerHasActed = true;

                }
                else
                {
                    _combatSounds.playSound(3);
                    _enemyCombat.playerHasActed = true;

                }
            }
        }
    }

    //-------------------------------KeyboardControls------------------------------------
    //Keyboard Controls in combat
    private void keyboardControls()
    {
        if ((Input.GetKeyDown(KeyCode.T) || Input.GetMouseButtonDown(0)) && timer - lastAttack > cooldownAttack)
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

