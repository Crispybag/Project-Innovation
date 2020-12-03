using UnityEngine;

public class PlaySound : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public variables
    [Header("Variables")]
    [Min(0)]
    public float soundLength = 4f;

    // FMOD Stuff
    [FMODUnity.EventRef] //Get path to the event
    public string soundFile;
    public string parameterName;
    public int index;

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================

    public void Play()
    {
        playSound();
    }

    private void playSound()
    {
        //Do this because FMOD sucks
        FMOD.Studio.EventInstance sound = FMODUnity.RuntimeManager.CreateInstance(soundFile);

        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, GetComponent<Rigidbody>());

        //Do this to set a certain parameter
        sound.setParameterByName(parameterName, index);

        //Start thing
        sound.start();

        //Make sure it doesnt loop
        sound.release();
    }
}