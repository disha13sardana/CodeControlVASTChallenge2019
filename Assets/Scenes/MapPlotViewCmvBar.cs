using System;
using System.Collections.Generic;
using System.Net.Configuration;
using CodeControl;
using UnityEngine;
using UnityEngine.Audio;

namespace Scenes
{
    public class MapPlotViewCmvBar : MonoBehaviour
    {
        public Vector3 GetPosition()
        {
            return GetComponent<Transform>().localPosition;
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
            Vector3 currentEulerAngles = GetComponent<Transform>().localEulerAngles;
            GetComponent<Transform>().localEulerAngles = new Vector3(
                currentEulerAngles.x + rotation.x,
                currentEulerAngles.y + rotation.y,
                currentEulerAngles.z + rotation.z
            );
        }

        public void SetPlotName(string plotName)
        {
            GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMesh>().text = plotName;
        }

        public List<BarPlotController> PlotBarPlots2(Mc1Data modelMc1Data,
            Dictionary<int, Vector3> modelRegionIdToLocationMap, float modelSlicingPlanePosition,
            Vector3 modelMaxBarHeight, Vector3 modelMinBarHeight, int modelColorDataColumnIndex,
            int modelHeightDataColumnIndex, Vector3 modelBarPlotScale, int modelNumOfBarsToPlot, bool modelVisibility,
            Color minColor, Color maxColor,
            Color minBrushColor, Color maxBrushColor)
        {
            List<BarPlotController> barPlotControllers = new List<BarPlotController>();
            foreach (KeyValuePair<int, Vector3> keyValuePair in modelRegionIdToLocationMap)
            {
                BarPlotModel barPlotModel = new BarPlotModel();
                barPlotModel.SerialId = keyValuePair.Key; // RegionId
                barPlotModel.Scale = Vector3.one;
                barPlotModel.OriginPosition =
                    Vector3.Scale(keyValuePair.Value - new Vector3(0.5f, 0f, 0.5f), new Vector3(-10f, 0f, 10f)) +
                    new Vector3(0, 0f, 0);
                barPlotModel.OriginPosition.y = 0.2f;
                barPlotModel.Rotation = new Vector3(0f, -90f, 90f);
                barPlotModel.InterBarSpacing = 0.3f;

                barPlotModel.BarScales = ComputeBarHeights(modelMc1Data, modelSlicingPlanePosition, keyValuePair.Key,
                    modelMaxBarHeight, modelMinBarHeight, modelHeightDataColumnIndex, modelNumOfBarsToPlot);
                Tuple<List<Color>, List<Color>> barColors = ComputeBarColors(modelMc1Data, modelSlicingPlanePosition,
                    keyValuePair.Key,
                    modelColorDataColumnIndex, modelNumOfBarsToPlot, minColor, maxColor, minBrushColor, maxBrushColor);
                barPlotModel.BarColors = barColors.Item1;
                barPlotModel.BrushedBarColors = barColors.Item2;
                barPlotModel.Visibility = modelVisibility;

                barPlotControllers.Add(
                    Controller.Instantiate<BarPlotController>(barPlotModel.PrefabName, barPlotModel, transform));
            }

            SetActive(modelVisibility);

            return barPlotControllers;
        }

        public static List<Vector3> ComputeBarHeights(Mc1Data modelMc1Data, float modelSlicingPlanePosition,
            int location, Vector3 modelMaxBarHeight, Vector3 modelMinBarHeight, int modelScaleDataIndex,
            int modelNumOfBarsToPlot)
        {
            AllReportsAtTimeStamp allReportsAtTimeStamp = modelMc1Data.GetData(modelSlicingPlanePosition);
            Vector3 scalingFactor = (modelMaxBarHeight - modelMinBarHeight) / 10;
            Vector3 shiftingFactor = modelMinBarHeight;

            List<Vector3> barScales = new List<Vector3>();
            List<Report> reports = allReportsAtTimeStamp.GetReports(location);

            for (int i = 0; i < Math.Min(reports.Count, modelNumOfBarsToPlot); i++)
            {
                int realHeight = reports[i].ReadValue(modelScaleDataIndex);

                // Sanity correction.
                if (realHeight < 0)
                {
                    realHeight = 0;
                }

                Vector3 scaledScale = realHeight * scalingFactor + shiftingFactor;
                scaledScale.x = modelMaxBarHeight.x;
                scaledScale.z = modelMaxBarHeight.z;
                barScales.Add(scaledScale);
            }

            return barScales;
        }

        public static Tuple<List<Color>, List<Color>> ComputeBarColors(Mc1Data modelMc1Data,
            float modelSlicingPlanePosition, int location,
            int modelColorDataColumnIndex, int modelNumOfBarsToPlot, Color minColor, Color maxColor,
            Color minBrushColor, Color maxBrushColor)
        {
            AllReportsAtTimeStamp allReportsAtTimeStamp = modelMc1Data.GetData(modelSlicingPlanePosition);
            List<Report> reports = allReportsAtTimeStamp.GetReports(location);
            List<Color> colors = new List<Color>();
            List<Color> brushColors = new List<Color>();

            for (int i = 0; i < Math.Min(reports.Count, modelNumOfBarsToPlot); i++)
            {
                float colorFactor = reports[i].ReadValue(modelColorDataColumnIndex) / 10f;

                //Sanity correction.
                if (colorFactor < 0)
                {
                    colorFactor = 0f;
                }

                Color color = Color.Lerp(minColor, maxColor, colorFactor);
                colors.Add(color);
                Color brushColor = Color.Lerp(minBrushColor, maxBrushColor, colorFactor);
                brushColors.Add(brushColor);
            }

            return new Tuple<List<Color>, List<Color>>(colors, brushColors);
        }

        public void SetActive(bool active)
        {
            gameObject.GetComponent<Renderer>().enabled = active;
            GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMesh>().GetComponent<Renderer>().enabled = active;
            // DataUtil.SetVisibility(gameObject, active);
        }

        public void PlayAudioClip(AudioSource showHideAudioSource, AudioClip audioClip)
        {
            if (audioClip == null)
            {
                return;
            }

            showHideAudioSource.PlayOneShot(audioClip);
        }

        public void SetAmbientAudio(AudioSource ambientAudioSource, AudioClip audioClip)
        {
            if (audioClip == null)
            {
                return;
            }

            ambientAudioSource.clip = audioClip;
            ambientAudioSource.loop = true;
            ambientAudioSource.spatialBlend = 1.0f;
            ambientAudioSource.volume = 1f;
        }

        public void PlayAmbientAudio(AudioSource ambientAudioSource, AudioClip ambientAudioClip,
            AudioClip showAudioClip)
        {
            if (ambientAudioClip == null)
            {
                return;
            }

            // Citation: https://gamedevbeginner.com/ultimate-guide-to-playscheduled-in-unity/
            // double delay2 = 0.2;
            // if (showAudioClip)
            // {
            //     delay2 = (double) showAudioClip.samples / showAudioClip.frequency;
            // }

            ambientAudioSource.loop = true;
            ambientAudioSource.Play(0);
            // ambientAudioSource.PlayScheduled(AudioSettings.dspTime + delay2);
        }

        public void PauseAmbientAudio(AudioSource ambientAudioSource)
        {
            ambientAudioSource.Pause();
        }

        public void SetAmbientAudioPitch(AudioSource ambientAudioSource, AudioMixerGroup pitchBendGroup, float pitch)
        {
            ambientAudioSource.pitch = pitch;
            pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / pitch);
        }

        public void StopAudio()
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
        }
    }
}