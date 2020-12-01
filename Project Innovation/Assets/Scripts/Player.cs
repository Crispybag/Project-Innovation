using UnityEngine;

public class Player : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    //[Header("Components")]

    // public variables
    [Header("Variables")]
    public bool defaultCanMove = true;
    public int defaultKeys = 0;
    public int defaultLevers = 0;
    public int hp = 30;

    [HideInInspector] public bool canCombat = false;
    [HideInInspector] public bool isEnteringCombat = false;

    // public statics
    public static bool canMove;
    public static int keys;
    public static int levers;

    // private objects
    private GameObject _enemy;

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        initializePosition();
        initializeDefaults();
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------initializePosition-----------------------------------------
    /// <summary> Initializes the player position at the checkpoint. </summary>
    private void initializePosition()
    {
        transform.position = Checkpoint.GetPosition();
    }

    //-----------------------------------initializeDefaults-----------------------------------------
    /// <summary> Initializes the default values. </summary>
    private void initializeDefaults()
    {
        canMove = defaultCanMove;
        keys = defaultKeys;
        levers = defaultLevers;
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

    public GameObject GetEnemy()
    {
        return _enemy;
    }
}