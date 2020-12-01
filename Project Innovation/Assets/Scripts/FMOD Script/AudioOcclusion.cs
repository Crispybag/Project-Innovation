using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class AudioOcclusion : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectAudio;
    FMOD.Studio.EventInstance audio;
    public string parameterOcclusion;
    Transform source;

    [Range(0f,1f)]
    public float volume = 0.5f;
    public LayerMask occlusionLayer = 1;

    private void Awake()
    {
        source = GameObject.FindObjectOfType<StudioListener>().transform;
        audio = RuntimeManager.CreateInstance(selectAudio);
        audio.getParameterByName(parameterOcclusion, out volume);
    }

    private void Start()
    {
        FMOD.Studio.PLAYBACK_STATE pbState;
        audio.getPlaybackState(out pbState);
        if (pbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            audio.start();
        }
    }

    private void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(audio, GetComponent<Transform>(), GetComponent<Rigidbody>());
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

    public string parameterName;
    public void playSound(int pIndex)
    {
        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(audio, transform, GetComponent<Rigidbody>());

        //Do this to set a certain parameter
        audio.setParameterByName(parameterName, pIndex);

        //Start thing
        audio.start();

        //Make sure it doesnt loop
        audio.release();
    }

    private void notOccluded()
    {
        audio.setParameterByName(parameterOcclusion, 0f);
    }
    private void occluded() 
    {
        audio.setParameterByName(parameterOcclusion, volume);
    }
}
