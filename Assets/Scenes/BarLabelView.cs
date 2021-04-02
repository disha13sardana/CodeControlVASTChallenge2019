using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes
{
    public class BarLabelView : MonoBehaviour
    {
        public void SetScale(Vector3 scale)
        {
            GetComponent<Transform>().localScale = scale;
        }

        public void SetPosition(Vector3 position)
        {
            GetComponent<Transform>().localPosition = position;
        }

        public void SetRotation(Vector3 rotation)
        {
            GetComponent<Transform>().localEulerAngles = rotation;
        }

        public void SetText(String text)
        {
            GetComponent<TextMesh>().text = text;
        }

        public void SetColor(Color color)
        {
            GetComponent<TextMesh>().color = color;
        }

        public void SetActive(bool active)
        {
            GetComponent<Renderer>().enabled = active;
        }
    }
}