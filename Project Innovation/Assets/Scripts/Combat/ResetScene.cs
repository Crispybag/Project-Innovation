using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public Player playerStats;
    public float respawnTimer = 1f;

    private float timePassed;

    private void FixedUpdate()
    {
        if (playerStats.hp <= 0)
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed > respawnTimer)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
