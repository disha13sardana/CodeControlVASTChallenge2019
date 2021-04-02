using System;
using System.Collections.Generic;
using CodeControl;
using UnityEngine;

namespace Scenes
{
    public class Main : MonoBehaviour
    {
        private MapPlotControllerWithLines mapPlotControllerWithLines;
        private MapPlotController rightTimeLinePlane;
        private MapPlotFloorController floorPlane;
        private MapPlotControllerCmvBar mapPlotControllerCmvBar1;
        private MapPlotControllerCmvSphere mapPlotControllerCmvSphere1;
        private MapPlotControllerCmvBar mapPlotControllerCmvBar2;
        private MapPlotControllerCmvSphere mapPlotControllerCmvSphere2;
        private MapPlotControllerCmvBar mapPlotControllerCmvBar3;
        private TwoDHistogramPlotController twoDHistogramPlotController;
        private SlicingPlaneController slicingPlaneController;

        private List<Dictionary<string, object>> pointList;
        private Mc1Data mc1Data;
        private float CMVScale = 0.3f;

        void Start()
        {
            //SpatialMapping.Instance.DrawVisualMeshes = false;

            pointList = CSVReader.Read("mc1_clean");
            mc1Data = new Mc1Data("mc1_processed_neg_1");

            // Left time-line plane
            MapPlotModelWithLines mapPlotModelWithLines = new MapPlotModelWithLines();
            mapPlotModelWithLines.SerialId = 0;
            mapPlotModelWithLines.CenterPosition = new Vector3(0f, 10f, 6f);
            mapPlotModelWithLines.Scale = new Vector3(1f, 1f, 1f);
            mapPlotModelWithLines.Rotation = new Vector3(90f, 180f, 0f);
            mapPlotModelWithLines.Mc1Data = mc1Data;
            mapPlotModelWithLines.Label = "04/06/2020 00:00:00";
            mapPlotControllerWithLines = Controller.Instantiate<MapPlotControllerWithLines>(
                MapPlotModelWithLines.PrefabName, mapPlotModelWithLines, transform);

            // Right time-line plane
            MapPlotModel mapPlotModel2 = new MapPlotModel();
            mapPlotModel2.SerialId = 0;
            mapPlotModel2.CenterPosition = new Vector3(0f, 10f, -6f);
            mapPlotModel2.Scale = new Vector3(1f, 1f, 1f);
            mapPlotModel2.Rotation = new Vector3(-270f, 180f, 0f);
            mapPlotModel2.PointList = pointList;
            mapPlotModel2.ShouldPlotDataSpheres = false;
            mapPlotModel2.ShouldPlotBarPlots = false;
            mapPlotModel2.ShouldPlotLinePlots = false;
            mapPlotModel2.Label = "04/11/2020 00:00:00";
            rightTimeLinePlane =
                Controller.Instantiate<MapPlotController>(MapPlotModel.PrefabName, mapPlotModel2, transform);

            // Floor plane
            MapPlotFloorModel mapPlotFloorModel = new MapPlotFloorModel();
            mapPlotFloorModel.SerialId = 0;
            mapPlotFloorModel.CenterPosition = new Vector3(0f, 5f, 0f);
            mapPlotFloorModel.Scale = new Vector3(1f, 1f, 1f);
            mapPlotFloorModel.Rotation = new Vector3(0f, -90f, 0f);
            mapPlotFloorModel.PointList = pointList;
            mapPlotFloorModel.ShouldPlotDataSpheres = true;
            mapPlotFloorModel.ShouldPlotBarPlots = false;
            mapPlotFloorModel.ShouldPlotLinePlots = false;
            mapPlotFloorModel.DataSphereScale = new Vector3(0.2f, 0.2f, 0.2f);
            mapPlotFloorModel.Mc1Data = mc1Data;
            mapPlotFloorModel.ShouldListenToSlicingPlaneEvents = true;
            floorPlane =
                Controller.Instantiate<MapPlotFloorController>("MapPlotPrefabFloor", mapPlotFloorModel, transform);

            // CMV plane 1 (bar)
            MapPlotModelCmvBar mapPlotModelCmvBar1 = new MapPlotModelCmvBar();
            mapPlotModelCmvBar1.SerialId = 0;
            mapPlotModelCmvBar1.CenterPosition = new Vector3(7f, 13f, -4f);
            mapPlotModelCmvBar1.Scale = Vector3.one * CMVScale;
            mapPlotModelCmvBar1.Rotation = new Vector3(90f, 0f, 90f);
            mapPlotModelCmvBar1.PointList = pointList;
            mapPlotModelCmvBar1.BarPlotScale = Vector3.one * CMVScale;
            mapPlotModelCmvBar1.Mc1Data = mc1Data;
            mapPlotModelCmvBar1.HeightDataColumnIndex = 0;
            mapPlotModelCmvBar1.ColorDataColumnIndex = 0;
            mapPlotModelCmvBar1.AmbientAudioDataColumnIndex = 0;
            mapPlotModelCmvBar1.PlotName = "Sewer and Water";
            mapPlotModelCmvBar1.visibility = false;
            mapPlotControllerCmvBar1 =
                Controller.Instantiate<MapPlotControllerCmvBar>(MapPlotModelCmvBar.PrefabName,
                    mapPlotModelCmvBar1, transform);

            // CMV Plane 2 (sphere)
            MapPlotModelCmvSphere mapPlotModelCmvSphere1 = new MapPlotModelCmvSphere();
            mapPlotModelCmvSphere1.SerialId = 0;
            mapPlotModelCmvSphere1.CenterPosition = new Vector3(7f, 13f, 0f);
            mapPlotModelCmvSphere1.Scale = Vector3.one * CMVScale;
            mapPlotModelCmvSphere1.Rotation = new Vector3(90f, 0f, 90f);
            mapPlotModelCmvSphere1.PointList = pointList;
            mapPlotModelCmvSphere1.Mc1Data = mc1Data;
            mapPlotModelCmvSphere1.ScaleDataColumnIndex = 1;
            mapPlotModelCmvSphere1.ColorDataColumnIndex = 1;
            mapPlotModelCmvSphere1.AmbientAudioDataColumnIndex = 1;
            mapPlotModelCmvSphere1.PlotName = "Power";
            mapPlotModelCmvSphere1.visibility = false;
            mapPlotControllerCmvSphere1 =
                Controller.Instantiate<MapPlotControllerCmvSphere>(MapPlotModelCmvSphere.PrefabName,
                    mapPlotModelCmvSphere1, transform);

//            // CMV plane 3 (bar)
//            MapPlotModelCmvBar mapPlotModelCmvBar2 = new MapPlotModelCmvBar();
//            mapPlotModelCmvBar2.SerialId = 0;
//            mapPlotModelCmvBar2.CenterPosition = new Vector3(7f, 13f, 4f);
//            mapPlotModelCmvBar2.Scale = Vector3.one * CMVScale;
//            mapPlotModelCmvBar2.Rotation = new Vector3(90f, 0f, 90f);
//            mapPlotModelCmvBar2.PointList = pointList;
//            mapPlotModelCmvBar2.BarPlotScale = Vector3.one * CMVScale;
//            mapPlotModelCmvBar2.Mc1Data = mc1Data;
//            mapPlotModelCmvBar2.HeightDataColumnIndex = 2;
//            mapPlotModelCmvBar2.ColorDataColumnIndex = 2;
//            mapPlotModelCmvBar2.PlotName = "Roads and Bridges";
//            mapPlotControllerCmvBar2 =
//                Controller.Instantiate<MapPlotControllerCmvBar>(MapPlotModelCmvBar.PrefabName, mapPlotModelCmvBar2,
//                    transform);
//
//            // CMV plane 4 (sphere)
//            MapPlotModelCmvSphere mapPlotModelCmvSphere2 = new MapPlotModelCmvSphere();
//            mapPlotModelCmvSphere2.SerialId = 0;
//            mapPlotModelCmvSphere2.CenterPosition = new Vector3(7f, 8f, -4f);
//            mapPlotModelCmvSphere2.Scale = Vector3.one * CMVScale;
//            mapPlotModelCmvSphere2.Rotation = new Vector3(90f, 0f, 90f);
//            mapPlotModelCmvSphere2.PointList = pointList;
//            mapPlotModelCmvSphere2.Mc1Data = mc1Data;
//            mapPlotModelCmvSphere2.ScaleDataColumnIndex = 3;
//            mapPlotModelCmvSphere2.ColorDataColumnIndex = 3;
//            mapPlotModelCmvSphere2.PlotName = "Medical";
//            mapPlotControllerCmvSphere2 =
//                Controller.Instantiate<MapPlotControllerCmvSphere>(MapPlotModelCmvSphere.PrefabName,
//                    mapPlotModelCmvSphere2, transform);
//
//            // CMV plane 5 (bar)
//            MapPlotModelCmvBar mapPlotModelCmvBar3 = new MapPlotModelCmvBar();
//            mapPlotModelCmvBar3.SerialId = 0;
//            mapPlotModelCmvBar3.CenterPosition = new Vector3(7f, 8f, 0f);
//            mapPlotModelCmvBar3.Scale = Vector3.one * CMVScale;
//            mapPlotModelCmvBar3.Rotation = new Vector3(90f, 0f, 90f);
//            mapPlotModelCmvBar3.PointList = pointList;
//            mapPlotModelCmvBar3.BarPlotScale = Vector3.one * CMVScale;
//            mapPlotModelCmvBar3.Mc1Data = mc1Data;
//            mapPlotModelCmvBar3.HeightDataColumnIndex = 4;
//            mapPlotModelCmvBar3.ColorDataColumnIndex = 4;
//            mapPlotModelCmvBar3.PlotName = "Buildings";
//            mapPlotControllerCmvBar3 =
//                Controller.Instantiate<MapPlotControllerCmvBar>(MapPlotModelCmvBar.PrefabName, mapPlotModelCmvBar3,
//                    transform);

            // CMV plane 6 (sphere)
//            MapPlotModelCmvSphere mapPlotModelCmvSphere3 = new MapPlotModelCmvSphere();
//            mapPlotModelCmvSphere3.SerialId = 0;
//            mapPlotModelCmvSphere3.CenterPosition = new Vector3(7f, 8f, 4f);
//            mapPlotModelCmvSphere3.Scale = Vector3.one * CMVScale;
//            mapPlotModelCmvSphere3.Rotation = new Vector3(-90f, 0f, 90f);
//            mapPlotModelCmvSphere3.PointList = pointList;
//            mapPlotModelCmvSphere3.Mc1Data = mc1Data;
//            mapPlotModelCmvSphere2.ScaleDataColumnIndex = 5;
//            mapPlotModelCmvSphere2.ColorDataColumnIndex = 5;
//            mapPlotModelCmvSphere2.PlotName = "Plot 6";
//            MapPlotControllerCmvSphere mapPlotControllerCmvSphere3 =
//                Controller.Instantiate<MapPlotControllerCmvSphere>(MapPlotModelCmvSphere.PrefabName,
//                    mapPlotModelCmvSphere3,
//                    transform);

            TwoDHistogramPlotModel twoDHistogramPlotModel = new TwoDHistogramPlotModel();
            twoDHistogramPlotModel.SerialId = 0;
            twoDHistogramPlotModel.CenterPosition = new Vector3(7f, 13f, 4f);
            twoDHistogramPlotModel.Scale = Vector3.one * CMVScale;
            twoDHistogramPlotModel.Rotation = new Vector3(-90f, 0f, 90f);
            twoDHistogramPlotModel.ShouldListenToSlicingPlaneEvents = true;
            twoDHistogramPlotModel.SlicingPlanePosition = 0.5f;
            twoDHistogramPlotModel.RegionIndex = new List<int> { };
            twoDHistogramPlotModel.Mc1Data = mc1Data;
            twoDHistogramPlotModel.PlotName = "Brushed Data";
            twoDHistogramPlotModel.visibility = true;
            twoDHistogramPlotController =
                Controller.Instantiate<TwoDHistogramPlotController>(TwoDHistogramPlotModel.PrefabName,
                    twoDHistogramPlotModel, transform);

            SlicingPlaneModel slicingPlaneModel = new SlicingPlaneModel();
            slicingPlaneModel.Position = new Vector3(0f, 10f, 0f);
            slicingPlaneModel.Scale = new Vector3(1f, 1f, 1f);
            slicingPlaneModel.Rotation = new Vector3(90f, 0f, 0f);
            slicingPlaneModel.lowerZBound = mapPlotModelWithLines.CenterPosition.z;
            slicingPlaneModel.upperZBound = mapPlotModel2.CenterPosition.z;
            slicingPlaneModel.Mc1Data = mc1Data;
            slicingPlaneController =
                Controller.Instantiate<SlicingPlaneController>(slicingPlaneModel.PrefabName, slicingPlaneModel,
                    transform);

            var transform1 = transform;
            var transformEulerAngles = transform1.eulerAngles;
            transformEulerAngles.y = -90;
            transform1.eulerAngles = transformEulerAngles;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetHistogramActive(bool active)
        {
            if (twoDHistogramPlotController == null)
            {
                return;
            }

            Debug.Log("Hist func called.");
            twoDHistogramPlotController.GetModel().visibility = active;
            twoDHistogramPlotController.SetActive(active);
            if (active)
            {
                twoDHistogramPlotController.PlayShowSound();
            }
            else
            {
                twoDHistogramPlotController.PlayHideSound();
            }
        }

        public void SetSewerAndWaterCmvBar1Active(bool active)
        {
            if (mapPlotControllerCmvBar1 == null)
            {
                return;
            }

            mapPlotControllerCmvBar1.GetModel().visibility = active;
            mapPlotControllerCmvBar1.SetActive(active);
            if (active)
            {
                mapPlotControllerCmvBar1.PlayShowSound();
            }
            else
            {
                mapPlotControllerCmvBar1.PlayHideSound();
            }
        }

        public void SetPowerCmvSphere1(bool active)
        {
            if (mapPlotControllerCmvSphere1 == null)
            {
                return;
            }

            mapPlotControllerCmvSphere1.GetModel().visibility = active;
            mapPlotControllerCmvSphere1.SetActive(active);
            if (active)
            {
                mapPlotControllerCmvSphere1.PlayShowSound();
            }
            else
            {
                mapPlotControllerCmvSphere1.PlayHideSound();
            }
        }

        public void SetRoadsAndBridgesCmvBa2Active(bool active)
        {
            if (mapPlotControllerCmvBar2 == null)
            {
                return;
            }

            mapPlotControllerCmvBar2.GetModel().visibility = active;
            mapPlotControllerCmvBar2.SetActive(active);
            if (active)
            {
                mapPlotControllerCmvBar2.PlayShowSound();
            }
            else
            {
                mapPlotControllerCmvBar2.PlayHideSound();
            }
        }

        public void SetMedicalSmvSphere2Active(bool active)
        {
            if (mapPlotControllerCmvSphere2 == null)
            {
                return;
            }

            mapPlotControllerCmvSphere2.GetModel().visibility = active;
            mapPlotControllerCmvSphere2.SetActive(active);
            if (active)
            {
                mapPlotControllerCmvSphere2.PlayShowSound();
            }
            else
            {
                mapPlotControllerCmvSphere2.PlayHideSound();
            }
        }

        public void SetBuildingsCmvBar3Active(bool active)
        {
            if (mapPlotControllerCmvBar3 == null)
            {
                return;
            }

            mapPlotControllerCmvBar3.GetModel().visibility = active;
            mapPlotControllerCmvBar3.SetActive(active);
            if (active)
            {
                mapPlotControllerCmvBar3.PlayShowSound();
            }
            else
            {
                mapPlotControllerCmvBar3.PlayHideSound();
            }
        }

//        public void SetShakeIntensityHistogramActive(bool active)
//        {
//            
//            twoDHistogramPlotController.SetActive(active);
//        }
    }
}