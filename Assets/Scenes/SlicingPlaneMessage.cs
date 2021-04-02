using System.Collections;
using System.Collections.Generic;
using CodeControl;
using UnityEngine;

public class SlicingPlaneMessage : Message
{
    public float slicingPlanePosition;

    public SlicingPlaneMessage(float slicingPlanePosition)
    {
        this.slicingPlanePosition = slicingPlanePosition;
    }
}
