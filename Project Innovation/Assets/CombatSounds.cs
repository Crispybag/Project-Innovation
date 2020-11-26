using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSounds : MonoBehaviour
{
    [FMODUnity.EventRef]
    //Get path to the event
    public string eventPath;


    public void playSound(int pIndex)
    {
        //Do this because FMOD sucks
        FMOD.Studio.EventInstance sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, GetComponent<Rigidbody>());

        //Do this to set a certain parameter
        sound.setParameterByName("Attack - Player", pIndex);

        //Start thing
        sound.start();

        //Make sure it doesnt loop
        sound.release();
    }
}
