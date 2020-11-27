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
    public HitWallSound sound;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (collision.gameObject.GetComponent<WallMaterialSetter>())
            {
                bonkSound.transform.position = collision.contacts[0].point;


                WallMaterialSetter setter = collision.gameObject.GetComponent<WallMaterialSetter>();
                sound.playSound((int)setter.wallMaterial);
            }
            else
            {
                sound.playSound(0);
            }
        }
    }
}