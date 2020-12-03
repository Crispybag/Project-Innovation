using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class OcclusionContinuous : MonoBehaviour
{
    [FMODUnity.EventRef]
    //FMOD Pathing
    public string selectAudio;

    //FMOD Event
    FMOD.Studio.EventInstance audioPath;

    //Parameter Occlusion
    public string parameterOcclusion;

    //Add Extra Parameters here
    //public string parameterExtra;

    //Power of occlusion
    [Range(0f, 1f)]
    public float volume = 0.7f;

    //Layermask to hide certain objects
    public LayerMask occlusionLayer = 1;


    private GameObject _player;


    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    //-------------------------------------Awake-----------------------------------------
    //Before Start
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        audioPath = RuntimeManager.CreateInstance(selectAudio);
    }

    //-------------------------------------Start-----------------------------------------
    //Start
    private void Start()
    {
        audioPath.start();
    }

    //-------------------------------------Update----------------------------------------
    //Update
    private void Update()
    {
        //Relocate 3d space of audio source
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(audioPath, GetComponent<Transform>(), GetComponent<Rigidbody>());

        //Determine if it hits the player
        RaycastHit hit;
        Physics.Linecast(transform.position, _player.transform.position, out hit, occlusionLayer);

        if (hit.collider.tag == "Player")
        {
            notOccluded();
            Debug.DrawLine(transform.position, _player.transform.position, Color.blue);
        }
        else
        {
            occluded();
            Debug.DrawLine(transform.position, _player.transform.position, Color.red);

        }
    }
    //=======================================================================================
    //                                     >  Tools  <
    //=======================================================================================

    //-------------------------------------PlaySound-----------------------------------------
    //Starts playing the sound
    public void playSound(int pIndex)
    {
        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(audioPath, transform, GetComponent<Rigidbody>());

        //Start thing
        audioPath.start();

        //Make sure it doesnt loop
        audioPath.release();
    }

    //-------------------------------------stopSound-----------------------------------------
    //Stops Playing the sound (Allowing fadeout)
    public void stopSound()
    {
        audioPath.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    //------------------------------------notOccluded----------------------------------------
    //Turn filters off
    private void notOccluded()
    {
        audioPath.setParameterByName(parameterOcclusion, 1f);
    }

    //---------------------------------------Update------------------------------------------
    //Turn filters on
    private void occluded()
    {
        audioPath.setParameterByName(parameterOcclusion, volume);
    }
}

