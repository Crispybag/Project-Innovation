using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCombatSounds : MonoBehaviour
{
    [FMODUnity.EventRef]
    //Get path to the event
    public string eventPath;
    public string parameterName;

    public void playSound(int pIndex)
    {
        //Do this because FMOD sucks
        FMOD.Studio.EventInstance sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, GetComponent<Rigidbody>());

        //Do this to set a certain parameter
        sound.setParameterByName(parameterName, pIndex);

        //Start thing
        sound.start();

        //Make sure it doesnt loop
        sound.release();
    }
}
