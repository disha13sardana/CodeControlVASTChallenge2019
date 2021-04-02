using System.Collections.Generic;
using CodeControl;
using UnityEngine;

namespace Scenes
{
    public class MapPlotController : Controller<MapPlotModel>
    {
        private MapPlotView view;

        private List<DataPointController> dataPointControllers = new List<DataPointController>();
        private List<BarPlotController> barPlotControllers = new List<BarPlotController>();

        public void Awake()
        {
            view = GetComponent<MapPlotView>();
        }

        protected override void OnInitialize()
        {
            view.SetRotation(model.Rotation);
            view.SetPosition(model.CenterPosition);
            view.SetScale(model.Scale);
            view.SetPlotLabel(model.Label);

            if (model.ShouldPlotLinePlots)
            {
                view.PlotLinePlots(model.RegionIdToLocationMap, model.PointList);
            }

            if (model.ShouldPlotDataSpheres)
            {
                dataPointControllers = view.PlotSpherePlots(model.RegionIdToLocationMap, model.DataSphereScale);
            }

            if (model.ShouldPlotBarPlots)
            {
                barPlotControllers = view.PlotBarPlots(model.RegionIdToLocationMap, model.BarPlotScale);
            }

            if (model.ShouldListenToSlicingPlaneEvents)
            {
                Message.AddListener<SlicingPlaneMessage>("slicing_plane_position_changed",
                    OnSlicingPlaneMessageReceive);
            }
        }

        private void OnSlicingPlaneMessageReceive(SlicingPlaneMessage m)
        {
            //Debug.Log("Message received! SlicingPlane position: " + m.slicingPlanePosition);

//            if (model.ShouldListenToSlicingPlaneEvents)
//            {
//                foreach (BarPlotController barPlotController in barPlotControllers)
//                {
//                    barPlotController.UpdateScale(Vector3.one *
//                                                  m.slicingPlanePosition); //TODO: Change this to an actual value.
//                }
//            }
//
//            if (model.ShouldListenToSlicingPlaneEvents)
//            {
//                foreach (DataPointController dataPointController in dataPointControllers)
//                {
//                    dataPointController.UpdateScale(Vector3.one *
//                                                    m.slicingPlanePosition); //TODO: Change this to an actual value.
//                }
//            }
        }

        public void SetActive(bool active)
        {
            view.SetActive(active);
        }
    }
}