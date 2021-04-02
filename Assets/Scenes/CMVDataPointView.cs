using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMVDataPointView : MonoBehaviour
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
        return GetComponent<Transform>().position;
    }

    public void SetPosition(Vector3 position)
    {
        GetComponent<Transform>().localPosition = position;
    }

    public void SetScale(Vector3 scale)
    {
        GetComponent<Transform>().localScale = scale;
    }

    public void SetActive(bool active)
    {
        GetComponent<Renderer>().enabled = active;
    }
}