using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public Canvas victoryCanvasPrefab;
    public GameObject doorMiddle;

    // public variables
    [Header("Variables")]
    public int keysNeededToOpen = 1;
    [Min(0)]
    public float soundLength = 4.5f; // TODO: get this from the audio source itself (google audioClip.length)

    // private objects
    private Vector3 _targetPos;
    private Vector3 _targetVector;
    private Vector3 _targetDirection;

    // private variables
    private int _soundLengthInFrames;

    private float _distanceToTarget;
    private float _travelDistance;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        _targetPos = transform.Find("Back").position;   // sets the target position
        _soundLengthInFrames = (int)(soundLength * Application.targetFrameRate);    // calculates the sound length in frames
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
                ChangeRoom(collision);
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
    private void ChangeRoom(Collision collision)
    {
        //Instantiate(victoryCanvasPrefab); // WIP, uncomment for simple testing

        Player.canMove = false; // stops the player from using controls to move

        //Transition the player
        StartCoroutine(Transition(collision));
    }

    //-----------------------------------Transition-----------------------------------------
    //Smoothly transitions the player through a door
    private IEnumerator Transition(Collision collision)
    {
        Destroy(GetComponent<BoxCollider>());   // destroys the collider so that you can move through the door
        CalculateDistances(collision);  // calculates travel distance and direction

        // moves the player towards the target
        for (int i = 0; i < _soundLengthInFrames; i++)
        {
            collision.transform.position += _targetDirection * _travelDistance; // direction unit vector * speed (travel distance)
            yield return null;
        }

        Destroy(GetComponentInChildren<AudioSource>()); // destroys the looping audio clip WIP (might mess up stuff later)
        doorMiddle.GetComponent<BoxCollider>().enabled = true; // turns on the box Collider for the middle of the door (which is tagged as Wall)
        Player.canMove = true;  // the player can move again after the transition
    }

    //-----------------------------------CalculateDistances-----------------------------------------
    //Calculates the distances needed to travel and the direction
    private void CalculateDistances(Collision collision)
    {
        _targetVector = _targetPos - collision.transform.position;  // the vector from the player to the target
        _distanceToTarget = _targetVector.magnitude;                // the total distance from the player to the target (at the beginning of the transition)
        _targetDirection = _targetVector.normalized;                // the direction from the player to the target (normalized)
        _travelDistance = _distanceToTarget / _soundLengthInFrames; // the distance the player needs to travel each frame (to line up with the soundclip)
    }
}