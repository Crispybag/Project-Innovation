using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusic : MonoBehaviour
{
    [FMODUnity.EventRef]
    //Get path to the event
    public string eventPath;
    private string parameterName;
    FMOD.Studio.EventInstance sound;
    public void playSound(int pIndex = 0)
    {
        //Determine which value should be switched to 1
        switch (pIndex)
        {
            case 0:
                parameterName = "Silence";
                break;
            case 1:
                parameterName = "Adventure music";
                break;
            case 2:
                parameterName = "Ominous music";
                break;
            case 3:
                parameterName = "Combat music";
                break;
            default:
                Debug.LogWarning("The ID: " + pIndex + " you put in, is not valid");
                break;
        }


        //Do this because FMOD sucks
        sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        //Do this to attach the sound to a gameobject for 3D effect
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, GetComponent<Rigidbody>());

        //Do this to set a certain parameter
        //THIS WILL BREAK VERY EASILY MAKE SURE TO COME BACK ON THIS IF SCRIPT DOESN'T WORK
        sound.setParameterByName("Silence", 0);
        sound.setParameterByName("Aadventure music", 0);
        sound.setParameterByName("Ominous music", 0);
        sound.setParameterByName("Combat music", 0);
        sound.setParameterByName(parameterName, 1f);

        //Start thing
        sound.start();
    }

    public void stopSound()
    {
        sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
