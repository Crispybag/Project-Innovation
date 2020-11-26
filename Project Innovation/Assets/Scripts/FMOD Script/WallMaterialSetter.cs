using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaterialSetter : MonoBehaviour
{
    public enum MATERIALS
    {
        DEFAULT = 0,
        WOOD = 1,
        STONE = 2,
        METAL = 3
    }

    public MATERIALS wallMaterial;
}
