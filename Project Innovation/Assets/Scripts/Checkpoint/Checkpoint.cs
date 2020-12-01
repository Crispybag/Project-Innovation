using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    //[Header("Components")]
    public static GameObject checkpoint;

    // public variables
    //[Header("Variables")]

    // private objects

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Awake()
    {
        initializeCheckpoint();
    }

    private void Start()
    {

    }

    private void Update()
    {
        // temporary reload level button WIP: remove later
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Maikel");
        }
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------initializeCheckpoint-----------------------------------------
    /// <summary>
    ///  Checks if there is no checkpoint object yet and turns on dontDestroyOnLoad for the first checkpoint object.
    ///  Else it destroys any new instances of the checkpoint object.
    /// </summary>
    private void initializeCheckpoint()
    {
        if (checkpoint == null)
        {
            Debug.Log("Initialize first checkpoint.");
            checkpoint = this.gameObject;
            DontDestroyOnLoad(checkpoint);
        }
        else
        {
            Debug.Log("Destroy new checkpoints after reload.");
            Destroy(this.gameObject);
        }
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================

    //-----------------------------------Move-----------------------------------------
    /// <summary>
    ///  Moves the checkpoint to the specified new position.
    /// </summary>
    public static void Move(Vector3 newPosition)
    {
        checkpoint.transform.position = newPosition;
    }

    //-----------------------------------GetPosition-----------------------------------------
    /// <summary>
    ///  Gets the position of the checkpoint.
    /// </summary>
    public static Vector3 GetPosition()
    {
        return checkpoint.transform.position;
    }
}
