using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScreen : MonoBehaviour
{
    public float maxTapTime = 0.1f;
    [HideInInspector] public bool isTapping;
    private float timeHoldingDown;
    private void Update()
    {

        isTapping = false;
        if (Input.touchCount == 0)
        {
            timeHoldingDown = 0;
        }

        else
        {
            timeHoldingDown += Time.deltaTime;
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && timeHoldingDown < maxTapTime)
            {
                isTapping = true;
                timeHoldingDown = 0;
            }
        }
    }
}
