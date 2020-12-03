using UnityEngine;

public class Key : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public PlaySound keyPickupSound;

    // private variables
    private float _soundLength;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        _soundLength = keyPickupSound.soundLength;   // gets the sound length
    }

    private void OnCollisionEnter(Collision collision)
    {
        PickUpKey(collision);
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //-----------------------------------PickUpKey-----------------------------------------
    /// <summary> Increases the keys count and destroys the key object after playing the pickup sound. </summary>
    private void PickUpKey(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            keyPickupSound.Play();                  // plays the key pickup sound
            Player.keys++;                          // increases the keys count
            Destroy(GetComponent<AudioSource>());   // destroys the looping sound
            Destroy(GetComponent<BoxCollider>());   // destroys the box collider
            Destroy(this.gameObject, _soundLength); // destroys the object after the sound is done playing
        }
    }
}