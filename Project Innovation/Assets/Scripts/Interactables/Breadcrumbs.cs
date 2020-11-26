using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadcrumbs : MonoBehaviour
{
    [FMODUnity.EventRef]
    //Get path to the event
    public string eventPath;

    public int breadcrumbsCollected;
    public float collectRadius = 1f;
    public float soundTimer = 0.2f;
    
    private List<Vector3> positions;
    private List<GameObject> children;
    private GameObject player;
    private FMOD.Studio.EventInstance sound;
    private float _timer;
    private bool hasStarted = false;

    private void Start()
    {
        sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);
       
        player = GameObject.FindGameObjectWithTag("Player");
        
        children = new List<GameObject>();
        positions = new List<Vector3>();

        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
            positions.Add(transform.GetChild(i).position);
        }
        
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > soundTimer && !hasStarted)
        {
           sound.start();
           // _timer = 0;

           hasStarted = true;
        }
        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, children[breadcrumbsCollected].transform, children[breadcrumbsCollected].GetComponent<Rigidbody>());
        checkForPlayerVicinity();
    }


    private void checkForPlayerVicinity()
    {
        if ((player.transform.position - positions[breadcrumbsCollected]).magnitude < collectRadius)
        {
            if (breadcrumbsCollected < children.Count - 1) breadcrumbsCollected++;
        }
    }
}
