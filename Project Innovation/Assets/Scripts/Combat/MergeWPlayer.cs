using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeWPlayer : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================
    // private objects

    [HideInInspector] public bool canCombat = false;

    private GameObject _enemy;
    [HideInInspector] public bool isEnteringCombat = false;
   

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
    //-----------------------------------SetEnemy-----------------------------------------
    //Sets the enemy variable to the enemy that the player is fighting
    public void SetEnemy(GameObject pEnemy)
    {
        _enemy = pEnemy;
    }

    public GameObject getEnemy()
    {
        return _enemy;
    }
}
