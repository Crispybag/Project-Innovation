using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveFootSteps : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    [FMODUnity.EventRef] public string footStepsEventPath;
    public string materialParameter;

    public float stepDistance = 2.0f;
    public float rayDistance = 1.2f;
    public float startRunningTime = 0.3f;
    public string[] materialTypes;
    [HideInInspector] public int defaultMaterial;
    // public variables
    //[Header("Variables")]

    // private objects

    // private variables
    private float _stepRandom;
    private Vector3 _prevPos;
    private float _distanceTravelled;
    private float _timeSinceStep;
    private RaycastHit hit;
    private int _materialValue;

    private bool isRunning;

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {
        _stepRandom = Random.Range(0f, 0.5f);
        _prevPos = transform.position;
        _materialValue = 0;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.blue);

        _timeSinceStep += Time.deltaTime;
        _distanceTravelled += (transform.position - _prevPos).magnitude;
        if (_distanceTravelled >= stepDistance + _stepRandom)
        {
            //MaterialCheck();
            PlayFootStep();
            _stepRandom = Random.Range(0f, 0.5f);
            _distanceTravelled = 0f;
        }
        _prevPos = transform.position;
    }
    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================
    //-------------------------------------MaterialCheck-----------------------------------------
    //Description of function 
    public void MaterialCheck()
    {
        if (hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>())
        {
            _materialValue = hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>().materialValue;
        }
        else _materialValue = defaultMaterial;

    }
    //--------------------------------------PlayFootStep-----------------------------------------
    //Description of function 
    public void PlayFootStep()
    {
        FMOD.Studio.EventInstance footstep = FMODUnity.RuntimeManager.CreateInstance(footStepsEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstep, transform, GetComponent<Rigidbody>());
        footstep.setParameterByName(materialParameter, _materialValue);
        footstep.start();
        footstep.release();
    }
}