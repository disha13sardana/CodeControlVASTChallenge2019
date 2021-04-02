using System.Collections;
using System.Collections.Generic;
using CodeControl;
using JetBrains.Annotations;
using Scenes;
using UnityEngine;

public class MapPlotView : MonoBehaviour
{
    public Vector3 GetPosition()
    {
        return GetComponent<Transform>().position;
    }

    public void SetPosition(Vector3 position)
    {
        GetComponent<Transform>().localPosition = position;
    }

    public Vector3 GetScale()
    {
        return GetComponent<Transform>().localScale;
    }

    public void SetScale(Vector3 scale)
    {
        GetComponent<Transform>().localScale = scale;
    }

    public void SetRotation(Vector3 rotation)
    {
        GetComponent<Transform>().localEulerAngles = rotation;
    }

    public void RotateBy(Vector3 rotation)
    {
        Vector3 currentEulerAngles = GetComponent<Transform>().eulerAngles;
        GetComponent<Transform>().eulerAngles = new Vector3(
            currentEulerAngles.x + rotation.x,
            currentEulerAngles.y + rotation.y,
            currentEulerAngles.z + rotation.z
        );
    }

    public List<BarPlotController> PlotBarPlots(Dictionary<int, Vector3> regionIdToLocationMap, Vector3 barPlotScale)
    {
        List<BarPlotController> barPlotControllers = new List<BarPlotController>();
        foreach (KeyValuePair<int, Vector3> keyValuePair in regionIdToLocationMap)
        {
            BarPlotModel barPlotModel2 = new BarPlotModel();
            barPlotModel2.SerialId = 0;
            barPlotModel2.Data = new Dictionary<float, float>();
            barPlotModel2.Data[1] = 2;
            barPlotModel2.Data[2] = 4;
            barPlotModel2.Data[3] = 6;
            //barPlotModel2.Data[4] = 5;
            //barPlotModel2.Data[5] = 3;
            //barPlotModel2.Data[6] = 6;
            barPlotModel2.BarWidth = 0.7f;
            barPlotModel2.OriginPosition =
                Vector3.Scale(keyValuePair.Value - new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-10f, 1f, 10f)) +
                new Vector3(0, 1f, 0);
            barPlotModel2.Scale = new Vector3(barPlotScale.x, barPlotScale.y / 2, barPlotScale.z) / 4f;
            barPlotModel2.Rotation = new Vector3(0, 90, 0);
            barPlotControllers.Add(
                Controller.Instantiate<BarPlotController>(barPlotModel2.PrefabName, barPlotModel2, transform));
        }

        return barPlotControllers;
    }

    public List<DataPointController> PlotSpherePlots(Dictionary<int, Vector3> regionIdToLocationMap,
        Vector3 dataSphereScale)
    {
        List<DataPointController> dataPointControllers = new List<DataPointController>();

        foreach (KeyValuePair<int, Vector3> keyValuePair in regionIdToLocationMap)
        {
            DataPointModel dataPointModel = new DataPointModel();
            dataPointModel.Position =
                Vector3.Scale(keyValuePair.Value - new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-10f, 1f, 10f)) +
                new Vector3(0, 1f, 0);
            dataPointModel.Scale = Vector3.Scale(dataSphereScale, transform.localScale);
            dataPointModel.Color = new Color(0, 0, 255);
            dataPointControllers.Add(
                Controller.Instantiate<DataPointController>(dataPointModel.PrefabName, dataPointModel, transform));
        }

        return dataPointControllers;
    }

    public void PlotLinePlots(Dictionary<int, Vector3> regionIdToLocationMap,
        List<Dictionary<string, object>> pointList)
    {
        List<string> columnList = new List<string>(pointList[1].Keys);
        int resolution = 120;
        Dictionary<string, float> data = DataUtil.ProcessMC1Data(pointList);

        foreach (KeyValuePair<int, Vector3> keyValuePair in regionIdToLocationMap)
        {
            LinePlotModel linePlotModel = new LinePlotModel();
            linePlotModel.OriginPosition =
                Vector3.Scale(keyValuePair.Value - new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-10f, 1f, 10f)) +
                new Vector3(0, 1f, 0);
            linePlotModel.Scale = new Vector3(0.5f, 0.1f, 0.5f);
            linePlotModel.Data = data;
            linePlotModel.LineWidth = 0.05f;
            linePlotModel.Rotation = new Vector3(-90f, 0f, 0f);
            linePlotModel.DataPointScale = Vector3.one / resolution;
            Controller.Instantiate<LinePlotController>(linePlotModel.PrefabName, linePlotModel, transform);
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetPlotLabel(string plotName)
    {
        GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMesh>().text = plotName;
    }
}