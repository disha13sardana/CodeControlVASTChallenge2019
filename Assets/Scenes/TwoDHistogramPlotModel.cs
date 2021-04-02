using System.Collections.Generic;
using CodeControl;
using UnityEngine;

namespace Scenes
{
    public class TwoDHistogramPlotModel : Model
    {
        public const string PrefabName = "2DHistogramPlotPrefab";
        public int SerialId = 0;
        public Vector3 CenterPosition = new Vector3();
        public Vector3 Scale = new Vector3(10f, 0.1f, 10f);
        public Vector3 Rotation = Vector3.zero;
        public bool ShouldListenToSlicingPlaneEvents = false;
        public Mc1Data Mc1Data;
        public List<int> RegionIndex = new List<int> { };
        public float SlicingPlanePosition = 0.5f;
        public float BarThickness = 0.001f;
        public string PlotName = "";
        public bool visibility = true;
    }
}