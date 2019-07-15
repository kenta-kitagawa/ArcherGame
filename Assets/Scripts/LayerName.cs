using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerName : MonoBehaviour
{
    public enum Layer_Name
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        Floor = 8,
        Character = 9,
        CharacterThroughtFloor = 10
    }


    public static void LayerChange(GameObject gObj, int layerNum)
    {
        gObj.layer = layerNum;
    }
}
