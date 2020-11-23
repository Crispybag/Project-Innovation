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
    [Tooltip("How fast the player moves")]
    public float playerSpeed = 5f;
    [Min(0)]
    [Tooltip("How fast the player rotates")]
    public float mouseSensitivity = 100f;

    // private objects

    // private variables
    private float _horizontal;
    private float _vertical;
    private float _mouseX;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        CursorSettings(CursorLockMode.Locked, false);
    }

    private void Update()
    {
        keyboardMovement();
        mouseMovement();
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

    //-----------------------------------CursorSettings-----------------------------------------
    //Sets various settings of the cursor 
    private void CursorSettings(CursorLockMode lockState, bool visible)
    {
        Cursor.lockState = lockState;
        Cursor.visible = visible;
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //-----------------------------------keyboardMovement-----------------------------------------
    //Movement of player in Local Space using keyboard inputs
    private void keyboardMovement()
    {
        // get input
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        // move
        if (_horizontal != 0)
        {
            player.transform.Translate(Vector3.right * _horizontal * playerSpeed * Time.deltaTime, Space.Self);
        }
        if (_vertical != 0)
        {
            player.transform.Translate(Vector3.forward * _vertical * playerSpeed * Time.deltaTime, Space.Self);
        }
    }

    //-----------------------------------mouseMovement-----------------------------------------
    //Rotation of the player using mouse movement
    private void mouseMovement()
    {
        // get input
        _mouseX = Input.GetAxis("Mouse X");

        // move
        if (_mouseX != 0)
        {
            player.Rotate(Vector3.up * _mouseX * mouseSensitivity * Time.deltaTime);
        }
    }

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}