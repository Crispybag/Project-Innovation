using UnityEngine;
using UnityEngine.Audio;

public class playBonkSound : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================
    // public objects
    [Header("Components")]
    public GameObject bonkSound;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            bonkSound.transform.position = collision.contacts[0].point;
        }
    }
}