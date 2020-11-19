using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public GameObject[] wayPoints;
    


    [Header("Variables")]
    [Tooltip("Speed at whwich the enemy moves")]
    public float speed;

    // private variables
    private int _currentWaypoint;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Start()
    {
        GoToNextWavePoint();
    }
    private void Update()
    {
        moveForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WaypointMarker")
        {
            GoToNextWavePoint();
        }
    }
    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------privateFunctionName---------------------------------
    //Description of function 
    private void verbNoun(int pVarName) { }

    //-----------------------------------PublicFunctionName-----------------------------------------
    //Description of function 
    private void VerbNoun(int pVarName) { }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    //------------------------------------moveForward----------------------------------------
    //Move enemy forward with current rotation
    private void moveForward() 
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    //----------------------------------goToNextWaypoint-------------------------------------
    //Rotate towards next waypoint
    public void GoToNextWavePoint()
    {
        //Get next waypoint
        if (_currentWaypoint + 1 < wayPoints.Length)
        {
            _currentWaypoint++;
        }
        else
        {
            _currentWaypoint = 0;
        }

        //Change rotation to that waypoint
        transform.LookAt(wayPoints[_currentWaypoint].transform);
    }

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}