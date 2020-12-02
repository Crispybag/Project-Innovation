using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadcrumbs : MonoBehaviour
{
    [FMODUnity.EventRef]
    //Get path to the event
    public string eventPath;

    public string parameterOcclusion;
    [Range(0f, 1f)]
    public float volume = 0.5f;

    [HideInInspector]public int breadcrumbsCollected;
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
        sound.getParameterByName(parameterOcclusion, out volume);
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

           hasStarted = true;
        }
        //Do this to attach the sound to a gameobject for 3D effect
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, children[breadcrumbsCollected].transform, children[breadcrumbsCollected].GetComponent<Rigidbody>());
        checkForPlayerVicinity();

        //Check if there is a wall inbetween the player and breadcrumb
        RaycastHit hit;
        Physics.Linecast(children[breadcrumbsCollected].transform.position, player.transform.position, out hit);

        if (hit.collider.tag == "Player")
        {
            notOccluded();
            Debug.DrawLine(children[breadcrumbsCollected].transform.position, player.transform.position, Color.blue);
        }
        else
        {
            occluded();
            Debug.DrawLine(children[breadcrumbsCollected].transform.position, player.transform.position, Color.red);

        }
    }


    private void checkForPlayerVicinity()
    {
        if ((player.transform.position - positions[breadcrumbsCollected]).magnitude < collectRadius)
        {
            if (breadcrumbsCollected < children.Count - 1) breadcrumbsCollected++;
        }
    }

    private void notOccluded()
    {
        sound.setParameterByName(parameterOcclusion, 1f);
    }
    private void occluded()
    {
        sound.setParameterByName(parameterOcclusion, volume);
    }
}
