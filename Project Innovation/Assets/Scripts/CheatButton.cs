using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject child;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (child.activeSelf)
            {
                child.SetActive(false);
                Player.isUndetectable = true;
            }
            else
            {
                child.SetActive(true);
                Player.isUndetectable = false;
            }
        }
    }
}
