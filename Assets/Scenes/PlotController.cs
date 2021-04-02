using System;
using System.Collections;
using System.Collections.Generic;
using CodeControl;
using UnityEngine;

public class PlotController : Controller<ScatterPlotModel>
{
    private ScatterPlotView scatterPlotView;

    private void Awake()
    {
        scatterPlotView = GetComponent<ScatterPlotView>();
    }

    protected override void OnInitialize()
    {
        scatterPlotView.PlotData(model.OriginPosition, model.Data, model.ColumnNames, model.SerialId);
        scatterPlotView.SetScale(model.Scale);
    }
}
