using System;
using System.Collections;
using System.Collections.Generic;
using CodeControl;
using UnityEngine;
using UnityEditor;

public class ScatterPlotView : MonoBehaviour
{
    public void PlotData(Vector3 originPosition, List<Dictionary<string, object>> data, List<string> columnNames, int serialId)
    {
        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere.transform.position = originPosition;
        Debug.Log("Origin Position: " + originPosition.x + originPosition.y + originPosition.z);

        int SerialId = 0;
        foreach (Dictionary<string, object> dictionary in data)
        {
            try
            {
                DataPointModel dataPointModel = new DataPointModel();
                dataPointModel.ParentSerialId = serialId;
                dataPointModel.SerialId = SerialId;
                dataPointModel.Position = new Vector3(
                    Convert.ToSingle(dictionary[columnNames[0]]) + originPosition.x,
                    Convert.ToSingle(dictionary[columnNames[1]]) + originPosition.y,
                    Convert.ToSingle(dictionary[columnNames[2]]) + originPosition.z
                );
                
                dataPointModel.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                Controller.Instantiate<DataPointController>(dataPointModel.PrefabName, dataPointModel, transform);
            }
            catch (FormatException exception)
            {
                Debug.Log("Format exception!" + exception.ToString());
            }

            SerialId = SerialId + 1;
        }
    }

    public void SetScale(Vector3 scale)
    {
        GetComponent<Transform>().localScale = scale;
    }
}
