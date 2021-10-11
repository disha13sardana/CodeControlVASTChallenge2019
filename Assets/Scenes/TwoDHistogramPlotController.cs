using System.Collections.Generic;
using System.Linq;
using CodeControl;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace Scenes
{
    public class TwoDHistogramPlotController : Controller<TwoDHistogramPlotModel>
    {
        public AudioClip showAudioClip;
        public AudioClip hideAudioClip;
        public AudioClip ambientAudioClip;

        private AudioSource showHideAudioSource;
        private AudioSource ambientAudioSource;

        private AudioMixerGroup pitchBendGroup;

        public TwoDHistogramPlotView view;
        private List<BarController> barControllers = new List<BarController>();

        // private static ILogger logger = Debug.unityLogger;

        public void Awake()
        {
            view = GetComponent<TwoDHistogramPlotView>();
            AudioSource[] audioSources = GetComponents<AudioSource>();
            if (audioSources.Length < 2)
            {
                Debug.Log("Error fetching the audio source array.");
            }

            showHideAudioSource = audioSources[0];
            ambientAudioSource = audioSources[1];

            pitchBendGroup = DataUtil.FindAudioMixerGroup("Pitch Bend Mixer CMV Histogram");
            ambientAudioSource.outputAudioMixerGroup = pitchBendGroup;
            EventManager.StartListening("floor_data_sphere_clicked", OnDataPointMessageReceive);
        }

        protected override void OnInitialize()
        {
            view.SetPlotName(model.PlotName);
            view.SetRotation(model.Rotation);
            view.SetPosition(model.CenterPosition);
            view.SetAmbientAudio(ambientAudioSource, ambientAudioClip);
            view.SetScale(model.Scale);
            barControllers = view.PlotData(model.Mc1Data, model.RegionIndex, model.SlicingPlanePosition,
                model.BarThickness, model.visibility);
            view.SetActive(model.visibility);

            Message.AddListener<SlicingPlaneMessage>("slicing_plane_position_changed", OnSlicingPlaneMessageReceive);
            Message.AddListener<SlicingPlaneManipulationMessage>("slicing_plane_manipulation_changed",
                OnSlicingPlaneManipulationMessageReceive);
            // Message.AddListener<DataPointMessage>("floor_data_sphere_clicked", OnDataPointMessageReceive);
        }

        private void OnDataPointMessageReceive(DataPointMessage m)
        {
            // logger.Log("Default", "Received brushing message for regionId: " + model.SerialId);
            Debug.Log("Received brushing message for regionId: " + m.regionId + ". New status brushed is: " + m.isNewStatusBrushed);
            if (m.isNewStatusBrushed)
            {
                model.RegionIndex.Add(m.regionId);
            }
            else
            {
                if (model.RegionIndex.Contains(m.regionId))
                {
                    model.RegionIndex.Remove(m.regionId);
                }
                else
                {
                    Debug.LogError("This should not happen.");
                    return;
                }
            }

            OnMessageReceive(model.SlicingPlanePosition);
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

            barControllers.ForEach(controller => controller.GetModel().Delete());
            barControllers.Clear();

            barControllers = view.PlotData(model.Mc1Data, model.RegionIndex, slicingPlanePosition,
                model.BarThickness, model.visibility);

            float totalSum = 0f;
            AllReportsAtTimeStamp allReportsAtTimeStamp = model.Mc1Data.GetData(slicingPlanePosition);
            for (var j = 0; j < model.RegionIndex.Count; j++)
            {
                int i = model.RegionIndex[j];
                AggregateReport aggregateReport = allReportsAtTimeStamp.GetAggregateReport(i);
                List<float> average = aggregateReport.GetAverage();
                float sum = average.Sum(item => item);
                totalSum = totalSum + sum;
            }

            // When no region is brushed.
            if (model.RegionIndex.Count == 0)
            {
                // Mute the ambient audio.
                ambientAudioSource.mute = true;
                Debug.Log("No region brushed. Muting hist ambient audio.");
            }
            else
            {
                // Set the ambient audio pitch.
                ambientAudioSource.mute = false;
                float pitch = (totalSum / 500) * 1.5f + 0.5f; // https://answers.unity.com/questions/25139/how-i-can-change-the-speed-of-a-song-or-sound.html
                view.SetAmbientAudioPitch(ambientAudioSource, pitchBendGroup, pitch);
            }
        }

        public void SetActive(bool active)
        {
            GetModel().visibility = active;
            view.SetActive(active);
            SetChildrenVisibility(active);
        }

        private void SetChildrenVisibility(bool visibility)
        {
            barControllers.ForEach(barController => barController.SetActive(visibility));
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

        public TwoDHistogramPlotModel GetModel()
        {
            return model;
        }
    }
}