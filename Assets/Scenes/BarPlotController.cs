using System.Collections.Generic;
using CodeControl;
using UnityEngine;

namespace Scenes
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BarPlotController : Controller<BarPlotModel>
    {
        private List<BarController> barControllers = new List<BarController>();
        private BarPlotView barPlotView;
        private bool isBrushed = false;

        private void Awake()
        {
            barPlotView = GetComponent<BarPlotView>();
        }

        protected override void OnInitialize()
        {
            barPlotView.SetScale(model.Scale);
            barPlotView.SetPosition(model.OriginPosition);
            barPlotView.SetRotation(model.Rotation);
            barControllers = barPlotView.PlotData(
                model.BarScales,
                isBrushed ? model.BrushedBarColors : model.BarColors,
                model.InterBarSpacing
            );
            barPlotView.SetActive(model.Visibility);
            // Message.AddListener<DataPointMessage>("floor_data_sphere_clicked", OnDataPointMessageReceive);
            EventManager.StartListening("floor_data_sphere_clicked", OnDataPointMessageReceive);
        }

        public int GetRegionId()
        {
            return model.SerialId;
        }

        public BarPlotModel GetModel()
        {
            return model;
        }

        public void CleanUp()
        {
            barControllers.ForEach(controller => controller.GetModel().Delete());
            barControllers.Clear();
        }

        public void PlotData(List<Vector3> modelBarScales, List<Color> modelBarColors,
            List<Color> modelBrushedBarColors)
        {
            model.BarScales = modelBarScales;
            model.BarColors = modelBarColors;
            model.BrushedBarColors = modelBrushedBarColors;

            barControllers = barPlotView.PlotData(
                model.BarScales,
                isBrushed ? model.BrushedBarColors : model.BarColors,
                model.InterBarSpacing
            );
        }

        private void OnDataPointMessageReceive(DataPointMessage m)
        {
            if (m.regionId == model.SerialId)
            {
                Debug.Log("Color change message received for regionId: " + m.regionId);
                isBrushed = !isBrushed;
                CleanUp();
                barControllers = barPlotView.PlotData(
                    model.BarScales,
                    isBrushed ? model.BrushedBarColors : model.BarColors,
                    model.InterBarSpacing
                );
            }

            try
            {
                // Setting the visibility the same as the parent.
                SetActive(GetModel().Visibility);
                // DataUtil.SetVisibility(gameObject, GetComponent<Renderer>().enabled);
            }
            catch (MissingComponentException exception)
            {
                Debug.Log("Caught MissingComponentException.");
            }
            
        }

        public void SetActive(bool active)
        {
            GetModel().Visibility = active;
            barPlotView.SetActive(GetModel().Visibility);
            SetChildrenVisibility(GetModel().Visibility);
        }

        private void SetChildrenVisibility(bool visibility)
        {
            barControllers.ForEach(barController => barController.SetActive(visibility));
        }
    }
}