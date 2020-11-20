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
    [Min(0)]
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
            float _soundLength = collision.gameObject.GetComponent<PlaySound>().soundLength; // gets the sound length from the other object
            keys++;
            Destroy(collision.gameObject.GetComponent<AudioSource>());
            Destroy(collision.gameObject, _soundLength); // destroys the object after the sound is done playing
        }
    }

    //-----------------------------------OpenDoor-----------------------------------------
    //opens the door if you have enough keys, removes the keys from the counter
    private void OpenDoor(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("Door") || collision.gameObject.name.StartsWith("door")) // if you collide with an object starting with "Door" or "door"
        {
            int KeysNeededToOpen = collision.gameObject.GetComponent<Door>().KeysNeededToOpen; // getting the amount of keys needed to open the door from the door object

            if (keys >= KeysNeededToOpen) // enough keys
            {
                keys -= KeysNeededToOpen;
                ChangeRoom();
            }
            else if (keys < KeysNeededToOpen && keys > 0) // not enough keys
            {
                Debug.Log("You need at least " + KeysNeededToOpen + " key(s) to open the door");
            }
            else if (keys < 0) // minus keys... somehow
            {
                Debug.LogError("You somehow have less than 0 keys, check the code dummy");
            }
        }
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================

    //-----------------------------------ChangeRoom-----------------------------------------
    //changes the game to the new room
    private void ChangeRoom()
    {
        Instantiate(victoryCanvasPrefab); // WIP
    }
}