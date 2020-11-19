using UnityEngine;
using System;

public class PickupList : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public Canvas victoryCanvasPrefab;

    // public variables
    [Header("Variables")]
    public int keys = 0;

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
        PickUpKey(collision);
        OpenDoor(collision);
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------PickUpKey-----------------------------------------
    //increases the keys count and destroys the key object on collision
    private void PickUpKey(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Key") || collision.gameObject.name.StartsWith("key"))
        {
            keys++;
            Destroy(collision.gameObject);
        }
    }

    //-----------------------------------OpenDoor-----------------------------------------
    //opens the door if you have atleast 1 key, removes 1 key from the count
    private void OpenDoor(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Door") || collision.gameObject.name.StartsWith("door"))
        {
            if (keys > 0)
            {
                keys--;
                Instantiate(victoryCanvasPrefab);
            }
            else if (keys == 0)
            {
                Debug.Log("You need at least 1 key to open the door");
            }
            else if (keys < 0)
            {
                Debug.LogError("You somehow have less than 0 keys, check the code dummy");
            }
        }
    }

    //-----------------------------------PublicFunctionName-----------------------------------------
    //Description of function 
    private void VerbNoun(int pVarName) { }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}