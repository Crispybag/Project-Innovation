using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSounds : MonoBehaviour
{
    [FMODUnity.EventRef]
    //Get path to the event
    public string eventPath;
    public string parameterVoiceLine;
    public string parameterPlatform;
    public int platform;

    [HideInInspector] public FMOD.Studio.EventInstance sound;
    public void Awake()
    {
        sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);
    }
    public void playSound(int pIndex)
    {
        //Do this because FMOD sucks
        

        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, GetComponent<Rigidbody>());

        sound.setParameterByName(parameterPlatform, platform);
        //Do this to set a certain parameter
        sound.setParameterByName(parameterVoiceLine, pIndex);

        //Start thing
        sound.start();

        //Make sure it doesnt loop
        //sound.release();
    }

    
}
