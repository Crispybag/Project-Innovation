using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public GameObject doorMiddle;

    // public variables
    [Header("Variables")]
    public int keysNeededToOpen = 1;
    public int leversNeededToOpen = 0;

    // private objects
    private Vector3 _targetPos;         // the red bean child (red capsule child object)
    private Vector3 _targetVector;      // to be calculated
    private Vector3 _targetDirection;   // to be calculated

    // private variables
    private int _soundLengthInFrames;
    private float soundLength = 3f;

    private float _distanceToTarget;
    private float _travelDistance;

    // FMOD Stuff
    [Header("FMOD Sound")]
    [FMODUnity.EventRef] //Get path to the event
    public string doorOpeningSound;
    public string doorParameterName;
    private int doorIndex = 3;

    [FMODUnity.EventRef] //Get path to the event
    public string innerThoughts;
    public string thoughtParameterName;


    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        initialize();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OpenDoor(collision);
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------initialize-----------------------------------------
    /// <summary> Initializes objects and variables. </summary>
    private void initialize()
    {
        _targetPos = transform.Find("Back").position;                               // sets the target position
        _soundLengthInFrames = (int)(soundLength * Application.targetFrameRate);    // calculates the sound length in frames
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //-----------------------------------OpenDoor-----------------------------------------
    /// <summary>
    /// Opens the door if you have enough keys, removes the keys from the counter.
    /// Plays sound effect of door opening and closing while transitioning through the door.
    /// </summary>
    private void OpenDoor(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Player.keys >= keysNeededToOpen && Player.levers >= leversNeededToOpen) // enough keys and levers
            {
                Player.keys -= keysNeededToOpen;
                Player.levers -= leversNeededToOpen;
                ChangeRoom(collision);
            }
            else if (Player.keys < keysNeededToOpen && Player.keys > 0 && Player.levers < leversNeededToOpen && Player.levers > 0) // NOT enough keys and levers
            {
                Debug.Log("You need at least " + keysNeededToOpen + " key(s) and " + leversNeededToOpen + " lever(s) pulled to open the door"); // can be changed into dialogue sound WIP
                playThoughtSound(5);    // "I require a key" dialogue
            }
            else if (Player.keys < keysNeededToOpen && Player.keys >= 0) // NOT enough keys
            {
                Debug.Log("You need at least " + keysNeededToOpen + " key(s) to open the door"); // can be changed into dialogue sound WIP
                playThoughtSound(5);    // "I require a key" dialogue
            }
            else if (Player.levers < leversNeededToOpen && Player.levers >= 0) // NOT enough levers
            {
                Debug.Log("You need at least " + leversNeededToOpen + " lever(s) to open the door"); // can be changed into dialogue sound WIP
                playThoughtSound(8);    // "I need to pull down a lever" dialogue
            }
            else if (Player.keys < 0 || Player.levers < 0) // minus keys or levers... somehow
            {
                Debug.LogError("You somehow have less than 0 keys, check the code dummy");
            }
            else
            {
                Debug.LogError("I have no idea how you got to this line, check the code...");
            }
        }
    }

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================

    //-----------------------------------ChangeRoom-----------------------------------------
    /// <summary> Changes the game to the new room. </summary>
    private void ChangeRoom(Collision collision)
    {
        Player.canMove = false; // stops the player from using controls to move

        //Transition the player
        StartCoroutine(Transition(collision));
    }

    //-----------------------------------Transition-----------------------------------------
    /// <summary> Smoothly transitions the player through a door. </summary>
    private IEnumerator Transition(Collision collision)
    {
        if (keysNeededToOpen > 0)
        {
            doorIndex = 4;
        }
        playDoorSound();                        // start playing the sound
        Destroy(GetComponent<BoxCollider>());   // destroys the collider so that you can move through the door
        CalculateDistances(collision);          // calculates travel distance and direction

        // moves the player towards the target
        for (int i = 0; i < _soundLengthInFrames; i++)
        {
            //      player.position      += direction (unit vector) * speed (travel distance)
            collision.transform.position += _targetDirection        * _travelDistance;
            yield return null;
        }

        PostTransition();
    }

    //-----------------------------------CalculateDistances-----------------------------------------
    /// <summary> Calculates the distances needed to travel and the direction. </summary>
    private void CalculateDistances(Collision collision)
    {
        _targetVector = _targetPos - collision.transform.position;  // the vector from the player to the target
        _distanceToTarget = _targetVector.magnitude;                // the total distance from the player to the target (at the beginning of the transition)
        _targetDirection = _targetVector.normalized;                // the direction from the player to the target (normalized)
        _travelDistance = _distanceToTarget / _soundLengthInFrames; // the distance the player needs to travel each frame (to line up with the soundclip)
    }

    //-----------------------------------PostTransition-----------------------------------------
    /// <summary> Everything that needs to happen after the transition is completed. </summary>
    private void PostTransition()
    {
        doorMiddle.GetComponent<BoxCollider>().enabled = true;  // turns the middle of the door into a wall
        Player.canMove = true;                                  // the player can move again after the transition
        Checkpoint.Move(_targetPos);                            // moves the checkpoint to the target
    }

    public void playDoorSound()
    {
        //Do this because FMOD sucks
        FMOD.Studio.EventInstance sound = FMODUnity.RuntimeManager.CreateInstance(doorOpeningSound);

        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, GetComponent<Rigidbody>());

        //Do this to set a certain parameter
        sound.setParameterByName(doorParameterName, doorIndex);

        //Start thing
        sound.start();

        //Make sure it doesnt loop
        sound.release();
    }

    public void playThoughtSound(int thoughtIndex)
    {
        //Do this because FMOD sucks
        FMOD.Studio.EventInstance sound = FMODUnity.RuntimeManager.CreateInstance(innerThoughts);

        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, GetComponent<Rigidbody>());

        //Do this to set a certain parameter
        sound.setParameterByName(thoughtParameterName, thoughtIndex);

        //Start thing
        sound.start();

        //Make sure it doesnt loop
        sound.release();
    }
}