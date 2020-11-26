using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFromStart : MonoBehaviour
{
    public TapScreen tap;
    public float maxTimeBetweenTaps;
    private int nTaps;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Start Timer
        if (tap.isTapping)
        {
            nTaps++;
            timer = 0;
            Debug.Log("tappy");
        }

        //Too slow with tapping
        if (timer > maxTimeBetweenTaps)
        {
            nTaps = 0;
        }

        //Meaning player doubletapped
        if (nTaps > 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
