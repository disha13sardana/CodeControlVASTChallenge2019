using System.Collections.Generic;
using CodeControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes
{
    public class MapPlotFloorController : Controller<MapPlotFloorModel>
    {
        private MapPlotFloorView view;

        private List<DataPointController> dataPointControllers = new List<DataPointController>();
        private List<BarPlotController> barPlotControllers = new List<BarPlotController>();

        public AudioClip maxDataPointAudioClip;

        public void Awake()
        {
            view = GetComponent<MapPlotFloorView>();
        }

        protected override void OnInitialize()
        {
            view.SetRotation(model.Rotation);
            view.SetPosition(model.CenterPosition);
            view.SetScale(model.Scale);
            view.SetPlotLabel(model.Label);
            view.StartSpatialAudio();

            if (model.ShouldPlotDataSpheres)
            {
                dataPointControllers =
                    view.PlotSpherePlots(model.RegionIdToLocationMap, model.DataSphereScale, model.Mc1Data,
                        model.SlicingPlanePosition, maxDataPointAudioClip);
            }

            if (model.ShouldListenToSlicingPlaneEvents)
            {
                Message.AddListener<SlicingPlaneMessage>("slicing_plane_position_changed",
                    OnSlicingPlaneMessageReceive);
                Message.AddListener<SlicingPlaneManipulationMessage>("slicing_plane_manipulation_changed",
                    OnSlicingPlaneManipulationMessageReceive);
            }
        }

        public void OnMessageReceive(float slicingPlanePosition)
        {
            if (model.ShouldPlotDataSpheres)
            {
                view.UpdateScale(model.RegionIdToLocationMap, model.DataSphereScale, model.Mc1Data,
                    slicingPlanePosition, dataPointControllers);
            }
        }

        private void OnSlicingPlaneMessageReceive(SlicingPlaneMessage m)
        {
            OnMessageReceive(m.slicingPlanePosition);
        }

        private void OnSlicingPlaneManipulationMessageReceive(SlicingPlaneManipulationMessage m)
        {
//            OnMessageReceive(m.slicingPlanePosition);
        }

        public void SetActive(bool active)
        {
            view.SetActive(active);
        }
    }
}