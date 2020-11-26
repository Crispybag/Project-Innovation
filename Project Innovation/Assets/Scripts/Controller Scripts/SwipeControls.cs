using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwipeControls : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    //[Header("Components")]

    // public variables
    [Header("Variables")]
    [Tooltip("Amount of frames, before the start position changes, the higher the number, the slower you can swipe")]
    [Range(0,60)]
    public int frameTreshold;

    public enum DIRECTION
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
        NONE = 4
    }

    [HideInInspector]
    public DIRECTION direction;

    [Tooltip("Minimal distance you need to swipe to see if it is registered")]
    public int minimalDistance;

    // private objects
    private int _framesHeldDown;
    private Vector2 _startPosition;
    private bool hasSwiped;

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void FixedUpdate()
    {
        //Reset Direction swipe
        direction = DIRECTION.NONE;

        //If screen is touched
        if (Input.touchCount > 0)
        {
            //Add frame for how long it is held
            _framesHeldDown++;
            Touch touch = Input.GetTouch(0);


            //Reset Startposition when screen is just touched, held for too long, or there is a direction input
            if (touch.phase == TouchPhase.Began || _framesHeldDown > frameTreshold || direction != DIRECTION.NONE)
            {
                hasSwiped = false;
                _startPosition = touch.position;
                _framesHeldDown = 0;
            }
            
            //Detect a swipe
            if ((touch.position - _startPosition).magnitude > minimalDistance && !hasSwiped)
            {
                float x = touch.position.x - _startPosition.x;
                float y = touch.position.y - _startPosition.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0) { direction = DIRECTION.RIGHT; hasSwiped = true; }
                    else       { direction = DIRECTION.LEFT; hasSwiped = true; }
                }
                else
                {
                    if (y > 0) { direction = DIRECTION.UP; hasSwiped = true; }
                    else       { direction = DIRECTION.DOWN; hasSwiped = true; }
                }
                Debug.Log(direction);
            }
        }

        //Reset touch
        else
        {
            _framesHeldDown = 0;
        }
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

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}