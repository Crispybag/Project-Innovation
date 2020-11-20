using UnityEngine;
using UnityEngine.Audio;

public class PlaySound : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public AudioSource audioSource;
    public Transform soundSource;

    // public variables
    [Header("Variables")]
    public string triggerObjectName;
    [Min(0)]
    public float soundLength = 4f;

    // private objects

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == triggerObjectName)
        {
            audioSource.Play();
        }
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------privateFunctionName-----------------------------------------
    //Description of function 
    private void verbNoun(int pVarName) { }

    //-----------------------------------PublicFunctionName-----------------------------------------
    //Description of function 
    private void VerbNoun(int pVarName) { }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}