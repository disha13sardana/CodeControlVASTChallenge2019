using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberDataPointView : MonoBehaviour
{

    public void SetPosition(Vector3 position)
    {
        GetComponent<Transform>().localPosition = position;

    }

    public void SetScale(Vector3 scale)
    {
        GetComponent<Transform>().localScale = scale;
    }
}
