using System.Collections;
using System.Collections.Generic;
using CodeControl;
using UnityEngine;

public class CubeDataPointController : Controller<CubeDataPointModel>
{
    private CuberDataPointView view;

    public void Awake()
    {
        view = GetComponent<CuberDataPointView>();
    }

    protected override void OnInitialize()
    {
        view.SetPosition(model.Position);
        view.SetScale(model.Scale);
    }
}
