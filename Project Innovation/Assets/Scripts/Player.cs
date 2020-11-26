
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool canMove = true;
    public static int keys = 0;

    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================
    // private objects

    [HideInInspector] public bool canCombat = false;

    private GameObject _enemy;
    [HideInInspector] public bool isEnteringCombat = false;
    public int hp = 30;


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