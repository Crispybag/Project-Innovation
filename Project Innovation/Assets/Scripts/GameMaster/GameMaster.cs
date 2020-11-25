using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public int targetFramerate = 60;

    private void Awake()
    {
        Application.targetFrameRate = targetFramerate;
    }
}
