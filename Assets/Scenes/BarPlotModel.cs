using System.Collections;
using System.Collections.Generic;
using CodeControl;
using UnityEngine;

public class BarPlotModel : Model
{
    public string PrefabName = "BarPlotPrefab";
    public int SerialId = 0;
    public Vector3 OriginPosition = new Vector3();
    public Vector3 Scale = new Vector3(1f, 1f, 1f);
    public Vector3 Rotation = new Vector3(0f, -90f, 90f);
    public Dictionary<float, float> Data;
    public List<Vector3> BarScales = new List<Vector3>();
    public List<Color> BarColors = new List<Color>();
    public List<Color> BrushedBarColors = new List<Color>();
    public float BarWidth = 0.1f;
    public float BarThickness = 0.01f;
    public float InterBarSpacing = 0.1f;
    public bool Visibility = true;
}