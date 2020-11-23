using UnityEngine;

public class KeyboardMouseMovement : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public Transform player;

    // public variables
    [Header("Variables")]
    [Min(0)]
    public float playerSpeed = 5f;

    // private objects

    // private variables
    private float _horizontal;
    private float _vertical;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {

    }

    private void Update()
    {
        movement();
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------privateFunctionName-----------------------------------------
    //Description of function 
    private void verbNoun() { }

    //-----------------------------------PublicFunctionName-----------------------------------------
    //Description of function 
    private void VerbNoun() { }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //-----------------------------------movement-----------------------------------------
    //Movement of player in World Space
    private void movement()
    {
        // get input
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        // move
        if (_horizontal != 0)
        {
            player.transform.Translate(Vector3.right * _horizontal * playerSpeed * Time.deltaTime, Space.World);
        }
        if (_vertical != 0)
        {
            player.transform.Translate(Vector3.forward * _vertical * playerSpeed * Time.deltaTime, Space.World);
        }
    }

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}