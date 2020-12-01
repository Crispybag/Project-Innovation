using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    
    public enum FIGHTACTION
    {
        NOTHING = 0,
        SLASHLEFT = 1,
        SLASHRIGHT = 2,
        BASH = 3,
        TUTORIAL = 4
    };

    [Header("Components")]
    [FMODUnity.EventRef]
    public string enemyAttackEvent;
    public string attackParameter;

    public Enemy enemy;
    public FIGHTACTION[] fightActions;
    // public variables
    [Header("Variables")]
    public float timeBetweenActions = 1f;
    public float bufferTime = 0.2f;
    public float maxRandomBuffer = 0.5f;
    [HideInInspector] public bool playerHasActed = false;
    // private objects
    private GameObject _player;
    private Player _playerStats;

    // private variables
    [HideInInspector] public FIGHTACTION currentAction;
    [HideInInspector] public bool attackFailed = false;
    private int _actionIndex = 0;
    private float _timer = 0f;
    private float _currentBuffer = 0f;

    private TutorialEnemy _tutorialEnemy;
    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStats = _player.GetComponent<Player>();

        if (gameObject.GetComponent<TutorialEnemy>())
        {
            _tutorialEnemy = gameObject.GetComponent<TutorialEnemy>();
        }
    }


    private void Update()
    {
        if (enemy.inCombat && _playerStats.hp > 0)
        {
            _timer += Time.deltaTime;

            if (_tutorialEnemy == null)
            {
                if (_timer > timeBetweenActions || playerHasActed)
                {
                    concludeAction();
                }
            }
            else
            {
                if (_timer > _tutorialEnemy.timers[_actionIndex] || playerHasActed)
                {
                    concludeAction();
                }
            }
        }
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================
    

    //-----------------------------------GoToNextAction-----------------------------------------
    //Goes to the next action of the enemy
    public void goToNextAction(int jumpToAction = -1)
    {
        attackFailed = false;

        if (jumpToAction == -1)
        {
            _actionIndex++;
        }
        else
        {
            _actionIndex = jumpToAction;
        }

        if (_tutorialEnemy != null)
        {
            selectTutorialVoiceLine(_actionIndex);
        }



        currentAction = fightActions[(_actionIndex % fightActions.Length)];
        _timer = 0;

        switch (currentAction)
        {
            case FIGHTACTION.SLASHLEFT:
                setPosition(0);
                playAttackSound(0);
                break;

            case FIGHTACTION.BASH:
                setPosition(1);
                playAttackSound(1);
                break;

            case FIGHTACTION.SLASHRIGHT:
                setPosition(2);
                playAttackSound(2);
                break;
        }
    }

    //-----------------------------------PlayAttackSound----------------------------------------
    //Plays the attacksound the monster makes
    public void playAttackSound(int pAttackIndex)
    {
        FMOD.Studio.EventInstance attack = FMODUnity.RuntimeManager.CreateInstance(enemyAttackEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(attack, transform, GetComponent<Rigidbody>());
        attack.setParameterByName(attackParameter, pAttackIndex);
        attack.start();
        attack.release();
    }

    //------------------------------------SetPosition-------------------------------------------
    //Sets the position of the enemy infront of, left or right from the plyer
    public void setPosition(int index)
    {
        if (index == 0)
        {
            transform.position = _player.transform.position + -1 * _player.transform.right;
        }

        else if (index == 1)
        {
            transform.position = _player.transform.position + _player.transform.forward;
        }

        else if (index == 2)
        {
            transform.position = _player.transform.position + _player.transform.right;
        }
    }

    //-----------------------------------ÇoncludeAction------------------------------------------
    //Concludes the action of the monster, whether it damages or not
    public void concludeAction()
    {
        float randomBuffer = UnityEngine.Random.Range(0f, maxRandomBuffer);
        _currentBuffer += Time.deltaTime;

        if (_tutorialEnemy == null)
        {
            if (_currentBuffer > bufferTime + randomBuffer)
            {
                _currentBuffer = 0f;
                playerHasActed = false;
                if (currentAction != FIGHTACTION.NOTHING)
                {
                    if (!attackFailed)
                    {
                        _playerStats.hp--;
                        CombatSounds playerSounds = _player.GetComponent<CombatSounds>();

                        if (_playerStats.hp <= 0)
                        {
                            playerSounds.playSound(4);
                        }
                        else
                        {
                            playerSounds.playSound(3);
                        }
                    }
                }
                goToNextAction();
            }
        }

        //TUTORIAL ENEMY CONCLUDE ACTION

        //Tutorial, tutorial, Left, Tutorial, Right, Tutorial, Slash, Tutorial, Right, Bash, Left, Tutorial, Attack
        else if (_currentBuffer > bufferTime + randomBuffer)
        {
            _currentBuffer = 0f;
            playerHasActed = false;
            if (currentAction != FIGHTACTION.NOTHING && currentAction != FIGHTACTION.TUTORIAL)
            {
                if (!attackFailed)
                {
                    CombatSounds playerSounds = _player.GetComponent<CombatSounds>();

                    if (_playerStats.hp <= 0)
                    {
                        playerSounds.playSound(4);
                    }
                    else
                    {
                        playerSounds.playSound(3);
                    }
                }
            }

            if (!attackFailed && (_actionIndex == 2 || _actionIndex == 4 || _actionIndex == 6))
            {
                if (_actionIndex == 2) goToNextAction(1);
                if (_actionIndex == 4) goToNextAction(3);
                if (_actionIndex == 6) goToNextAction(5);
            }

            else
            {
                goToNextAction();
            }
        }

    }


    void playTutorialVoiceLine(int index)
    {
        _tutorialEnemy.playSound(index, _tutorialEnemy.platform);
    }

    void selectTutorialVoiceLine(int index)
    {
        switch (index)
        {
            case 1:
                //"Attack from the left"
                playTutorialVoiceLine(0);
                break;

            case 3:
                //"Attack from the right"
                playTutorialVoiceLine(1);
                break;

            case 5:
                //"Attack from both"
                playTutorialVoiceLine(2);
                break;

            case 7:
                //"General Advice"
                playTutorialVoiceLine(4);
                break;

            case 11:
                //"ATTACK!!!"
                playTutorialVoiceLine(3);
                break;
            default:
                break;

        }
    }
}