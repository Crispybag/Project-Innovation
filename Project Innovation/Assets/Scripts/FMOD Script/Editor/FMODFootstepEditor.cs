using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(FMODStudioMaterialSetter))]
public class FMODFootstepEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var MS = target as FMODStudioMaterialSetter;
        var FPF = FindObjectOfType<AdaptiveFootSteps>();

        MS.materialValue = EditorGUILayout.Popup("Set Material As", MS.materialValue, FPF.materialTypes);
    }
}


[CustomEditor(typeof(AdaptiveFootSteps))]
public class FMODFootstepEditorTwo : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var FPF = target as AdaptiveFootSteps;

        FPF.defaultMaterial = EditorGUILayout.Popup("Set Default Material As", FPF.defaultMaterial, FPF.materialTypes);
    }
}

