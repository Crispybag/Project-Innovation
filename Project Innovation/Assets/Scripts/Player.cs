using UnityEngine;

public class Player : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public statics
    public static bool canMove = true;
    public static int keys = 0;
    public static int levers = 0;

    // public objects
    //[Header("Components")]

    // public variables
    [Header("Variables")]
    public int hp = 30;

    [HideInInspector] public bool canCombat = false;
    [HideInInspector] public bool isEnteringCombat = false;

    // private objects
    private GameObject _enemy;

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        transform.position = Checkpoint.GetPosition();  // initialize the player are the checkpoint
    }

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