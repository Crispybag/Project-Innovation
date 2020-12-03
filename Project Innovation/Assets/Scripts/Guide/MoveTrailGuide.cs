using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrailGuide : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================
    public GameObject guideHolder;

    [Header("Variables")]
    [Tooltip("Speed at whwich the enemy moves")]
    public float speed;
    

    // private variables
    public int detectDistance = 3;

    [HideInInspector] public bool isWaiting = false;
    private GameObject _player;

    private List<GameObject> children;
    [HideInInspector] public int checkpointsCollected;
    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Start()
    {
        children = new List<GameObject>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
        for (int i = 0; i < guideHolder.transform.childCount; i++)
        {
            if (guideHolder.transform.GetChild(i).tag != "Guide")
            {
                children.Add(guideHolder.transform.GetChild(i).gameObject);
            }
        }

        GoToNextWavePoint();
        isWaiting = false;
    }
    private void Update()
    {
        checkpointDist();
        getDistToPlayer();
        if (!isWaiting) moveForward();
    }

    private void checkpointDist()
    {
        if ((transform.position - children[checkpointsCollected].transform.position).magnitude < 0.2f)
        {
            GoToNextWavePoint();
            //transform.position = children[checkpointsCollected].transform.position;
        }
    }

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
        if (checkpointsCollected + 1 < children.Count)
        {
            checkpointsCollected++;
        }
        else
        {
            checkpointsCollected = 0;
        }
        isWaiting = true;

        //Change rotation to that waypoint
        transform.LookAt(children[checkpointsCollected].transform);
    }

    private void getDistToPlayer()
    {
        if ((_player.transform.position - transform.position).magnitude < detectDistance)
        {
            isWaiting = false;
        }
    }

}
