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
        BASH = 3
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


    // private objects
    [HideInInspector] public FIGHTACTION currentAction;
    [HideInInspector] public bool attackFailed = false;
    private int _actionIndex = 0;
    private float _timer = 0f;
    private float _currentBuffer = 0f;
    // private variables
    private GameObject _player;
    private Player _playerStats;
    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStats = _player.GetComponent<Player>();
    }


    private void Update()
    {
        if (enemy.inCombat && _playerStats.hp > 0)
        {
            _timer += Time.deltaTime;

            if (_timer > timeBetweenActions)
            {
                concludeAction();
            }
        }
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================
    

    //-----------------------------------GoToNextAction-----------------------------------------
    //GoToNextAction
    public void goToNextAction()
    {
        attackFailed = false;
        _actionIndex++;
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



    public void playAttackSound(int pAttackIndex)
    {
        FMOD.Studio.EventInstance attack = FMODUnity.RuntimeManager.CreateInstance(enemyAttackEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(attack, transform, GetComponent<Rigidbody>());
        attack.setParameterByName(attackParameter, pAttackIndex);
        attack.start();
        attack.release();
    }

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

    public void concludeAction()
    {
        _currentBuffer += Time.deltaTime;

        if (_currentBuffer > bufferTime)
        {
            _currentBuffer = 0f;

            if (currentAction != FIGHTACTION.NOTHING)
            {
                if (!attackFailed)
                {
                    _playerStats.hp--;
                   
                }
            }
            goToNextAction();
        }
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}