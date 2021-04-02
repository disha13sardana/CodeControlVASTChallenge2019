using System;
using System.Collections.Generic;
using System.Linq;
using CodeControl;
using UnityEngine;
using UnityEngine.Audio;

namespace Scenes
{
    public class MapPlotControllerCmvBar : Controller<MapPlotModelCmvBar>
    {
        public AudioClip showAudioClip;
        public AudioClip hideAudioClip;
        public AudioClip ambientAudioClip;

        private AudioSource showHideAudioSource;
        private AudioSource ambientAudioSource;

        private AudioMixerGroup pitchBendGroup;

        private MapPlotViewCmvBar view;
        private List<BarPlotController> barPlotControllers = new List<BarPlotController>();

        public void Awake()
        {
            view = GetComponent<MapPlotViewCmvBar>();
            AudioSource[] audioSources = GetComponents<AudioSource>();
            if (audioSources.Length < 2)
            {
                Debug.Log("Error fetching the audio source array.");
            }

            showHideAudioSource = audioSources[0];
            ambientAudioSource = audioSources[1];

            pitchBendGroup = DataUtil.FindAudioMixerGroup("Pitch Bend Mixer CMV Bar");
            ambientAudioSource.outputAudioMixerGroup = pitchBendGroup;
        }

        protected override void OnInitialize()
        {
            view.SetPlotName(model.PlotName);
            view.SetRotation(model.Rotation);
            view.SetPosition(model.CenterPosition);
            view.SetAmbientAudio(ambientAudioSource, ambientAudioClip);
            view.SetScale(model.Scale);
            barPlotControllers = view.PlotBarPlots2(model.Mc1Data, model.RegionIdToLocationMap,
                model.SlicingPlanePosition, model.MaxBarHeight, model.MinBarHeight, model.ColorDataColumnIndex,
                model.HeightDataColumnIndex, model.BarPlotScale, model.NumOfBarsToPlot, model.visibility,
                model.minColor, model.maxColor, model.minBrushColor, model.maxBrushColor);

            Message.AddListener<SlicingPlaneMessage>("slicing_plane_position_changed", OnSlicingPlaneMessageReceive);
            Message.AddListener<SlicingPlaneManipulationMessage>("slicing_plane_manipulation_changed",
                OnSlicingPlaneManipulationMessageReceive);
            SetActive(model.visibility);
        }

        private void OnSlicingPlaneMessageReceive(SlicingPlaneMessage m)
        {
            OnMessageReceive(m.slicingPlanePosition);
        }

        private void OnSlicingPlaneManipulationMessageReceive(SlicingPlaneManipulationMessage m)
        {
//            OnMessageReceive(m.slicingPlanePosition);
        }

        private void OnMessageReceive(float slicingPlanePosition)
        {
            model.SlicingPlanePosition = slicingPlanePosition;

            foreach (BarPlotController barPlotController in barPlotControllers)
            {
                barPlotController.CleanUp();

                List<Vector3> newBarHeights = MapPlotViewCmvBar.ComputeBarHeights(model.Mc1Data,
                    model.SlicingPlanePosition, barPlotController.GetRegionId(),
                    model.MaxBarHeight, model.MinBarHeight, model.HeightDataColumnIndex, model.NumOfBarsToPlot);
                Tuple<List<Color>, List<Color>> barColors = MapPlotViewCmvBar.ComputeBarColors(model.Mc1Data,
                    model.SlicingPlanePosition, barPlotController.GetRegionId(),
                    model.ColorDataColumnIndex, model.NumOfBarsToPlot, model.minColor, model.maxColor,
                    model.minBrushColor, model.maxBrushColor);
                barPlotController.PlotData(newBarHeights, barColors.Item1, barColors.Item2);
            }

            float totalSum = 0f;
            AllReportsAtTimeStamp allReportsAtTimeStamp = model.Mc1Data.GetData(slicingPlanePosition);
            for (int i = 1; i < 20; i++)
            {
                AggregateReport aggregateReport = allReportsAtTimeStamp.GetAggregateReport(i);
                // List<float> average = aggregateReport.GetAverage();
                // float sum = average.Sum(item => item);
                totalSum = totalSum + aggregateReport.GetNonNegativeReportCount(model.AmbientAudioDataColumnIndex);
            }

            float pitch = (totalSum / 500) * 1.5f + 0.5f;
            view.SetAmbientAudioPitch(ambientAudioSource, pitchBendGroup, pitch);

            // https://answers.unity.com/questions/25139/how-i-can-change-the-speed-of-a-song-or-sound.html

            SetActive(model.visibility);
        }

        public void SetActive(bool active)
        {
            GetModel().visibility = active;
            view.SetActive(model.visibility);
            SetChildrenVisibility(GetModel().visibility);
        }

        private void SetChildrenVisibility(bool visibility)
        {
            barPlotControllers.ForEach(barPlotController => barPlotController.SetActive(visibility));
        }

        public MapPlotModelCmvBar GetModel()
        {
            return model;
        }

        public void PlayShowSound()
        {
            view.PlayAudioClip(showHideAudioSource, showAudioClip);
            view.PlayAmbientAudio(ambientAudioSource, ambientAudioClip, showAudioClip);
        }

        public void PlayHideSound()
        {
            view.PauseAmbientAudio(ambientAudioSource);
            view.PlayAudioClip(showHideAudioSource, hideAudioClip);
        }
    }
}