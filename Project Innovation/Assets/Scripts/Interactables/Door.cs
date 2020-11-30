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
    [Min(0)]
    public float soundLength = 4.5f;

    // private objects
    private Vector3 _targetPos;         // the red bean child (red capsule child object)
    private Vector3 _targetVector;      // to be calculated
    private Vector3 _targetDirection;   // to be calculated

    // private variables
    private int _soundLengthInFrames;

    private float _distanceToTarget;
    private float _travelDistance;

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
            }
            else if (Player.keys < keysNeededToOpen && Player.keys >= 0) // NOT enough keys
            {
                Debug.Log("You need at least " + keysNeededToOpen + " key(s) to open the door"); // can be changed into dialogue sound WIP
            }
            else if (Player.levers < leversNeededToOpen && Player.levers >= 0) // NOT enough levers
            {
                Debug.Log("You need at least " + leversNeededToOpen + " lever(s) to open the door"); // can be changed into dialogue sound WIP
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
        StartCoroutine(transition(collision));
    }

    //-----------------------------------transition-----------------------------------------
    /// <summary> Smoothly transitions the player through a door. </summary>
    private IEnumerator transition(Collision collision)
    {
        Destroy(GetComponent<BoxCollider>());   // destroys the collider so that you can move through the door
        calculateDistances(collision);  // calculates travel distance and direction

        // moves the player towards the target
        for (int i = 0; i < _soundLengthInFrames; i++)
        {
            //      player.position      += direction (unit vector) * speed (travel distance)
            collision.transform.position += _targetDirection        * _travelDistance;
            yield return null;
        }

        postTransition();
    }

    //-----------------------------------calculateDistances-----------------------------------------
    /// <summary> Calculates the distances needed to travel and the direction. </summary>
    private void calculateDistances(Collision collision)
    {
        _targetVector = _targetPos - collision.transform.position;  // the vector from the player to the target
        _distanceToTarget = _targetVector.magnitude;                // the total distance from the player to the target (at the beginning of the transition)
        _targetDirection = _targetVector.normalized;                // the direction from the player to the target (normalized)
        _travelDistance = _distanceToTarget / _soundLengthInFrames; // the distance the player needs to travel each frame (to line up with the soundclip)
    }

    //-----------------------------------postTransition-----------------------------------------
    /// <summary> Everything that needs to happen after the transition is completed. </summary>
    private void postTransition()
    {
        Destroy(GetComponentInChildren<AudioSource>());         // destroys the looping audio clip WIP (might mess up stuff later)
        doorMiddle.GetComponent<BoxCollider>().enabled = true;  // turns the middle of the door into a wall
        Player.canMove = true;                                  // the player can move again after the transition
        Checkpoint.Move(_targetPos);                            // moves the checkpoint to the target
    }
}