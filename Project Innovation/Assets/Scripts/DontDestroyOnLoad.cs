using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private GameObject thisObject;  // holds the first door instance (to keep track of game progress)

    private void Awake()
    {
        initializeFirstInstance();
    }

    //-----------------------------------initializeFirstInstance-----------------------------------------
    /// <summary>
    ///  Checks if there are no instances of this object yet and turns on dontDestroyOnLoad for the first instance.
    ///  Else it destroys any new instances of this object.
    /// </summary>
    private void initializeFirstInstance()
    {
        if (thisObject == null)
        {
            Debug.Log("Initialize first instance of object.");
            thisObject = this.gameObject;
            DontDestroyOnLoad(thisObject);
        }
        else
        {
            Debug.Log("Destroy new instances of this object after reload.");
            Destroy(this.gameObject);
        }
    }
}
