using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    //=======================================================================================
    //                            >  Components & Variables  <
    //=======================================================================================

    // public objects
    [Header("Components")]
    public Canvas victoryCanvasPrefab;

    // public variables
    //[Header("Variables")]

    // private objects

    // private variables

    //=======================================================================================
    //                              >  Start And Update  <
    //=======================================================================================

    private void Start()
    {

    }

    private void Update()
    {
        WinIfKeyDown(KeyCode.Space);
    }

    //=======================================================================================
    //                              >  Start Functions  <
    //=======================================================================================

    //-----------------------------------privateFunctionName-----------------------------------------
    //Description of function 
    private void verbNoun(int pVarName) { }

    //-----------------------------------WinIfKeyDown-----------------------------------------
    //Changes to the victory screen when you press the specified key 
    public void WinIfKeyDown(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            Instantiate(victoryCanvasPrefab);
        }
    }

    //=======================================================================================
    //                              >  Update Functions <
    //=======================================================================================

    //=======================================================================================
    //                              >  Tool Functions  <
    //=======================================================================================
}