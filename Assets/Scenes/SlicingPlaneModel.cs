using System.Collections;
using System.Collections.Generic;
using CodeControl;
using Scenes;
using UnityEngine;

public class SlicingPlaneModel : Model
{
    public string PrefabName = "SlicingPlanePrefab";
    public int ParentSerialId;
    public int SerialId;
    public Color Color;
    public Vector3 Position;
    public Vector3 Scale;
    public Vector3 Rotation = Vector3.zero;
    public float lowerZBound;
    public float upperZBound;
    public Mc1Data Mc1Data;
}
