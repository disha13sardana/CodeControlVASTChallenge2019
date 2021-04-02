using System;
using CodeControl;
using UnityEngine;

namespace Scenes
{
    public class BarLabelModel : Model
    {
        public string PrefabName = "BarLabelPrefab";
        public int SerialId = 0;
        public Vector3 Position = new Vector3();
        public Vector3 Scale = new Vector3(0.3f, 0.3f, 0.3f);
        public Vector3 Rotation = new Vector3(90f, -90f, -120f);
        public String Text = "Default";
        public Color Color = Color.black;
        public bool Visibility = true;
    }
}