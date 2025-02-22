﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class AudioOcclusion : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectAudio;
    FMOD.Studio.EventInstance audioPath;
    public string parameterOcclusion;
    Transform source;

    [Range(0f,1f)]
    public float volume = 0.5f;
    public LayerMask occlusionLayer = 1;

    private void Awake()
    {
        source = GameObject.FindObjectOfType<StudioListener>().transform;
        audioPath = RuntimeManager.CreateInstance(selectAudio);
        audioPath.getParameterByName(parameterOcclusion, out volume);
    }

    private void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(audioPath, GetComponent<Transform>(), GetComponent<Rigidbody>());
        RaycastHit hit;
        Physics.Linecast(transform.position, source.position, out hit, occlusionLayer);

        if (hit.collider.name == "Player")
        {
            notOccluded();
            Debug.DrawLine(transform.position, source.position, Color.blue);
        }
        else
        {
            occluded();
            Debug.DrawLine(transform.position, source.position, Color.red);

        }
    }   
    public void playSound(int pIndex)
    {
        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(audioPath, transform, GetComponent<Rigidbody>());

        //Start thing
        audioPath.start();

        //Make sure it doesnt loop
        audioPath.release();
    }

    private void notOccluded()
    {
        audioPath.setParameterByName(parameterOcclusion, 0f);
    }
    private void occluded() 
    {
        audioPath.setParameterByName(parameterOcclusion, volume);
    }
}
