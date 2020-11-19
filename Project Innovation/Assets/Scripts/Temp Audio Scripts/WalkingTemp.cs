using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WalkingTemp : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource audioS;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0.05f && !audioS.isPlaying)
        {
            audioS.Play();
        }
        else if (rb.velocity.magnitude < 0.05f)
        {
            audioS.Stop();
        }
    }
}
