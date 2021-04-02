using System.Collections;
using System.Collections.Generic;
using CodeControl;
using UnityEngine;

public class ScatterPlotModel : Model
{
    public string PrefabName = "ScatterPlotPrefab";
    public int SerialId;
    public Vector3 OriginPosition = new Vector3();
    public Vector3 Scale = new Vector3(1f, 1f, 1f);
    public List<Dictionary<string, object>> Data;
    public List<string> ColumnNames = new List<string>();
}
