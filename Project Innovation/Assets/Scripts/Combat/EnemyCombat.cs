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
    public Enemy enemy;
    public FIGHTACTION[] fightActions;
    // public variables
    [Header("Variables")]
    public float timeBetweenActions = 1f;

    // private objects
    [HideInInspector] public FIGHTACTION currentAction;
    private int _actionIndex = 0;
    private float _timer = 0;
    // private variables
    private GameObject _player;
    private MergeWPlayer _playerStats;
    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStats = _player.GetComponent<MergeWPlayer>();
    }


    private void Update()
    {
        if (enemy.inCombat)
        {
            _timer += Time.deltaTime;
            if (_timer > timeBetweenActions)
            {
                if (currentAction == FIGHTACTION.BASH || currentAction == FIGHTACTION.SLASHLEFT || currentAction == FIGHTACTION.SLASHRIGHT)
                {
                    _playerStats.hp--;
                }
                goToNextAction();
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
        _actionIndex++;
        currentAction = fightActions[(_actionIndex % fightActions.Length)];
        _timer = 0;
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}