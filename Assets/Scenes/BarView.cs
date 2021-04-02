using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarView : MonoBehaviour
{
    public Color GetColor()
    {
        return GetComponent<Renderer>().material.color;
    }

    public void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public Vector3 GetPosition()
    {
        return GetComponent<Transform>().localPosition;
    }

    public void SetPosition(Vector3 position)
    {
        GetComponent<Transform>().localPosition = position;
    }

    public void SetScale(Vector3 scale)
    {
        GetComponent<Transform>().localScale = scale;
    }

    public void SetRotation(Vector3 rotation)
    {
        GetComponent<Transform>().localEulerAngles = rotation;
    }
}