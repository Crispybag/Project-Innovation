using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideWaiting : MonoBehaviour
{
    [FMODUnity.EventRef]
    //Get path to the event
    public string eventPath;

    public string parameterName;
    public string parameterOcclusion;
    FMOD.Studio.EventInstance audioPath;
    private GameObject _player;


    public MoveTrailGuide moveGuide;
    private float timer;
    FMOD.Studio.EventInstance sound;
    public LayerMask occlusionLayer = 1;
    public float repeatTimer = 10;
    public void playSound(int pIndex)
    {
        //Do this because FMOD sucks
        sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, GetComponent<Rigidbody>());

        //Do this to set a certain parameter
        sound.setParameterByName(parameterName, pIndex);

        //Start thing
        sound.start();

        //Make sure it doesnt loop
        sound.release();
    }

    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {

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



        if (moveGuide.isWaiting)
        {
            timer += Time.deltaTime;
            if (timer > repeatTimer)
            {
                playSound(0);
                timer = 0;
            }
        }
        else
        {
            sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void notOccluded()
    {
        audioPath.setParameterByName(parameterOcclusion, 0f);
    }

    private void occluded()
    {
        audioPath.setParameterByName(parameterOcclusion, 0.7f);
    }
}

