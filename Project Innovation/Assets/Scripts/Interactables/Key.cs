using UnityEngine;

public class Key : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    //[Header("Components")]

    // public variables
    //[Header("Variables")]

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

    //-----------------------------------PickUpKey-----------------------------------------
    //increases the keys count and destroys the key object after playing the pickup sound
    private void PickUpKey(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float _soundLength = GetComponent<PlaySound>().soundLength; // gets the sound length
            Player.keys++;
            Destroy(GetComponent<AudioSource>());   // destroys the looping sound
            Destroy(GetComponent<BoxCollider>());   // destroys the box collider
            Destroy(this, _soundLength);            // destroys the object after the sound is done playing
        }
    }

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}