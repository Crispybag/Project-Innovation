using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    // #1 Play Soundclip
    // #2 Left Attack

    // #3 Play Soundclip
    // #4 Right Attack

    // #5 Play Soundclip
    // #6 Dodge
    // #7 Advice
    // #8 3 Practice Attacks
    // #9 Attack
    [FMODUnity.EventRef]
    public string eventPath;
    public string voiceLineParameter;
    public string platformParameter;
    public int platform;

    [HideInInspector] public int tutorialPhase;
    public float[] timers;

    public void playSound(int pIndexVoiceLine, int pIndexPlatform)
    {
        //Do this because FMOD sucks
        FMOD.Studio.EventInstance sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        //Do this to set a certain parameter
        sound.setParameterByName(voiceLineParameter, pIndexVoiceLine);
        sound.setParameterByName(platformParameter, pIndexPlatform);

        //Start thing
        sound.start();

        //Make sure it doesnt loop
        sound.release();
    }
}