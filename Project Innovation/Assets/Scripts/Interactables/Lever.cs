using UnityEngine;
using FMODUnity;

public class Lever : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public TapScreen tapScreenScript;
    public StudioEventEmitter attractionNoise;
    public PlaySound useSound;

    // private variables
    private float _soundLength;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        _soundLength = GetComponent<PlaySound>().soundLength; // gets the sound length
    }

    private void OnCollisionStay(Collision collision)
    {
        UseLever(collision);
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //-----------------------------------UseLever-----------------------------------------
    /// <summary> Increases the levers count and destroys the lever object after playing the use lever sound. </summary>
    private void UseLever(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && (Input.GetKeyDown(KeyCode.E) || tapScreenScript.isTapping))
        {
            attractionNoise.Stop();                 // stops the loops sound
            useSound.Play();                        // plays the use sound
            Player.levers++;                        // increases the levers count
            Destroy(GetComponent<AudioSource>());   // destroys the looping sound
            Destroy(GetComponent<BoxCollider>());   // destroys the box collider
            Destroy(this, _soundLength);            // destroys the object after the sound is done playing
        }
    }
}