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
    

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void FixedUpdate()
    {
        direction = DIRECTION.NONE;
        if (Input.touchCount > 0)
        {
            _framesHeldDown++;
            Touch touch = Input.GetTouch(0);

            //Reset Startposition
            if (touch.phase == TouchPhase.Began || _framesHeldDown > frameTreshold || direction != DIRECTION.NONE)
            {
                _startPosition = touch.position;
                _framesHeldDown = 0;
                
                Debug.Log(direction);
            }
            
            //Detect a swipe
            if ((touch.position - _startPosition).magnitude > minimalDistance)
            {
                float x = touch.position.x - _startPosition.x;
                float y = touch.position.y - _startPosition.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0) { direction = DIRECTION.RIGHT; }
                    else       { direction = DIRECTION.LEFT;  }
                }
                else
                {
                    if (y > 0) { direction = DIRECTION.UP;    }
                    else       { direction = DIRECTION.DOWN;  }
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