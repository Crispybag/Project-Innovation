using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public static GameObject checkpoint;

    private void Awake()
    {
        if (checkpoint == null)
        {
            Debug.Log("Initialize first checkpoint");
            checkpoint = this.gameObject;
            DontDestroyOnLoad(checkpoint);
        }
        else
        {
            Debug.Log("Destroy(this)");
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Maikel");
        }
    }
}
