using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplaySwipe : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public SwipeControls swipeControls;
    public Text text;
    // public variables
    //[Header("Variables")]

    // private objects

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void Update()
    {
        if (swipeControls.direction == SwipeControls.DIRECTION.LEFT)
            text.text = "LEFT";
        else if (swipeControls.direction == SwipeControls.DIRECTION.RIGHT)
            text.text = "RIGHT";
        else if (swipeControls.direction == SwipeControls.DIRECTION.UP)
            text.text = "UP";
        else if (swipeControls.direction == SwipeControls.DIRECTION.DOWN)
            text.text = "DOWN";
    }
}