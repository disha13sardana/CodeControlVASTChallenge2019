using System.Collections;
using System.Collections.Generic;
using CodeControl;
using UnityEngine;

public class CMVDataPointModel : Model
{
    public string PrefabName = "CMVDataPointPrefab";
    public int ParentSerialId;
    public int SerialId;
    public Color Color;
    public Color BrushColor;
    public Vector3 Position;
    public Vector3 Scale;
    public bool Visibility = true;
}
