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
    private float _soundLength;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        _soundLength = GetComponent<PlaySound>().soundLength; // gets the sound length
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

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //-----------------------------------PickUpKey-----------------------------------------
    /// <summary> Increases the keys count and destroys the key object after playing the pickup sound. </summary>
    private void PickUpKey(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<PlaySound>().Play();       // plays the key pickup sound
            Player.keys++;                          // increases the keys count
            Destroy(GetComponent<AudioSource>());   // destroys the looping sound
            Destroy(GetComponent<BoxCollider>());   // destroys the box collider
            Destroy(this, _soundLength);            // destroys the object after the sound is done playing
        }
    }

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}