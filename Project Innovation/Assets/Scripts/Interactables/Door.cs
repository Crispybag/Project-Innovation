using UnityEngine;

public class Door : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    //[Header("Components")]
    public Canvas victoryCanvasPrefab;

    // public variables
    [Header("Variables")]
    public int keysNeededToOpen = 1;

    // private objects

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        OpenDoor(collision);
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------privateFunctionName-----------------------------------------
    //Description of function 
    private void verbNoun(int pVarName) { }

    //-----------------------------------PublicFunctionName-----------------------------------------
    //Description of function 
    private void VerbNoun(int pVarName) { }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //-----------------------------------OpenDoor-----------------------------------------
    //opens the door if you have enough keys, removes the keys from the counter
    //plays sound effect of door opening and closing while transitioning through the door
    private void OpenDoor(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Player.keys >= keysNeededToOpen) // enough keys
            {
                Player.keys -= keysNeededToOpen;
                ChangeRoom();
            }
            else if (Player.keys < keysNeededToOpen && Player.keys > 0) // not enough keys
            {
                Debug.Log("You need at least " + keysNeededToOpen + " key(s) to open the door"); // can be changed into dialogue sound WIP
            }
            else if (Player.keys < 0) // minus keys... somehow
            {
                Debug.LogError("You somehow have less than 0 keys, check the code dummy");
            }
        }
    }

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================

    //-----------------------------------ChangeRoom-----------------------------------------
    //changes the game to the new room
    private void ChangeRoom()
    {
        Instantiate(victoryCanvasPrefab); // WIP
        Player.canMove = false;
    }
}
