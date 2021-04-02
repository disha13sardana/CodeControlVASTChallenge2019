using CodeControl;
using UnityEngine;

namespace Scenes
{
    public class BarModel : Model
    {
        public string PrefabName = "BarPrefab";
        public int ParentSerialId = 0;
        public int SerialId = 0;
        public Color Color = Color.black;
        public Vector3 Position = Vector3.zero;
        public Vector3 Scale = Vector3.one;
        public Vector3 Rotation = Vector3.zero;
        public bool Visibility = true;
    }
}