using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeControl;
using JetBrains.Annotations;
using Scenes;
using UnityEngine;

public class MapPlotFloorView : MonoBehaviour
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

    public void StartSpatialAudio()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load("Ambience") as AudioClip;
        audioSource.loop = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.volume = 1f;
        audioSource.dopplerLevel = 5.0f;
        audioSource.Play(0);
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

    public List<DataPointController> PlotSpherePlots(Dictionary<int, Vector3> regionIdToLocationMap,
        Vector3 dataSphereScale, Mc1Data modelMc1Data, float modelSlicingPlanePosition, AudioClip maxDataPointAudioFile)
    {
        var regionIdToScaleMap = GetRegionIdToScaleMap(regionIdToLocationMap, modelMc1Data, modelSlicingPlanePosition);
        var regionIdWithMaxScale = regionIdToScaleMap.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

        List<DataPointController> dataPointControllers = new List<DataPointController>();

        foreach (KeyValuePair<int, Vector3> keyValuePair in regionIdToLocationMap)
        {
            int regionId = keyValuePair.Key;
            DataPointModel dataPointModel = new DataPointModel();
            dataPointModel.Position =
                Vector3.Scale(keyValuePair.Value - new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-10f, 1f, 10f)) +
                new Vector3(0, 1f, 0);
            dataPointModel.Scale = Vector3.Scale(dataSphereScale * regionIdToScaleMap[regionId], transform.localScale);
            dataPointModel.Color = new Color(0, 0, 255, 0.5f);
            dataPointModel.BrushColor = new Color(255, 0, 127, 0.7f);
            // Set the audio if the current region is the max region.
            if (regionId == regionIdWithMaxScale)
            {
                dataPointModel.AudioClip = maxDataPointAudioFile;
            }

            dataPointModel.SerialId = regionId;
            dataPointControllers.Add(Controller.Instantiate<DataPointController>(
                dataPointModel.PrefabName,
                dataPointModel,
                transform
            ));
        }

        return dataPointControllers;
    }

    public void UpdateScale(Dictionary<int, Vector3> regionIdToLocationMap, Vector3 dataSphereScale,
        Mc1Data modelMc1Data, float modelSlicingPlanePosition, List<DataPointController> dataPointControllers)
    {
        Dictionary<int, float> regionIdToScaleMap =
            GetRegionIdToScaleMap(regionIdToLocationMap, modelMc1Data, modelSlicingPlanePosition);
        int location = 1;
        foreach (DataPointController dataPointController in dataPointControllers)
        {
            Vector3 scale = Vector3.Scale(dataSphereScale * regionIdToScaleMap[location], transform.localScale);
            dataPointController.UpdateScale(scale);
            location += 1;
        }
    }

    private static Dictionary<int, float> GetRegionIdToScaleMap(Dictionary<int, Vector3> regionIdToLocationMap,
        Mc1Data modelMc1Data,
        float modelSlicingPlanePosition)
    {
        Dictionary<int, float> regionIdToScaleMap = new Dictionary<int, float>();
        foreach (KeyValuePair<int, Vector3> keyValuePair in regionIdToLocationMap)
        {
            int regionId = keyValuePair.Key;
            AllReportsAtTimeStamp allReportsAtTimeStamp = modelMc1Data.GetData(modelSlicingPlanePosition);
            AggregateReport aggregateReport = allReportsAtTimeStamp.GetAggregateReport(regionId);
            float average = aggregateReport.GetAverage(Report.ColumnNameToPositionDict["shake_intensity"]);
            regionIdToScaleMap.Add(regionId, average);
        }

        return regionIdToScaleMap;
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