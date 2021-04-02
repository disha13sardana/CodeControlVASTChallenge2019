using System.Collections.Generic;
using CodeControl;
using UnityEngine;

namespace Scenes
{
    public class BarPlotView : MonoBehaviour
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

        public List<BarController> PlotData(List<Vector3> modelBarScales,
            List<Color> modelBarColors, float modelInterBarSpacing)
        {
            List<BarController> barControllers = new List<BarController>();
            if (modelBarScales.Count == 0)
            {
                //Debug.Log("BarPlotView: No data to plot.");
                return new List<BarController>();
            }

            for (int i = 0; i < modelBarScales.Count; i++)
            {
                BarModel barModel = new BarModel();
                barModel.Scale = modelBarScales[i];
                barModel.Color = modelBarColors[i];
                barModel.Position = new Vector3(
                    i * modelInterBarSpacing,
                    (modelBarScales[i].y / 2) + DataUtil.EPSILON,
                    0
                );

                BarController barController =
                    Controller.Instantiate<BarController>(barModel.PrefabName, barModel, transform);
                barControllers.Add(barController);
            }

            return barControllers;
        }

        public void SetActive(bool visibility)
        {
            if (gameObject.GetComponent<Renderer>() == null)
            {
                return;
            }
            gameObject.GetComponent<Renderer>().enabled = visibility;
        }
    }
}