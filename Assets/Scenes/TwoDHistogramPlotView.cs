using System;
using System.Collections.Generic;
using System.Linq;
using CodeControl;
using UnityEngine;
using UnityEngine.Audio;

namespace Scenes
{
    public class TwoDHistogramPlotView : MonoBehaviour
    {
        private BarController barControllerXAxis;
        private BarController barControllerYAxis;
        private List<BarLabelController> barLabelControllers = new List<BarLabelController>();

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
            GetComponent<Transform>().eulerAngles = rotation;
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

        public List<BarController> PlotData(Mc1Data mc1Data, List<int> regionIndex, float slicingPlanePosition,
            float barThickness, bool modelVisibility)
        {
            if (barControllerXAxis)
            {
                barControllerXAxis.GetModel().Delete();
            }

            if (barControllerYAxis)
            {
                barControllerYAxis.GetModel().Delete();
            }

            if (barLabelControllers.Count > 0)
            {
                barLabelControllers.ForEach(controller => controller.GetModel().Delete());
                barLabelControllers.Clear();
            }

            BarModel barModelYAxis = new BarModel();
            barModelYAxis.Scale = new Vector3(0.1f, barThickness, 7.9f);
            barModelYAxis.Position = new Vector3(-4.5f, 0f, 0.7f);
            barControllerXAxis =
                Controller.Instantiate<BarController>(barModelYAxis.PrefabName, barModelYAxis, transform);


            BarModel barModelXAxis = new BarModel();
            barModelXAxis.Scale = new Vector3(0.1f, barThickness, 10);
            barModelXAxis.Position = new Vector3(0.05f, 0f, -2.9f);
            barModelXAxis.Rotation = new Vector3(0f, 90f, 0f);
            barControllerYAxis =
                Controller.Instantiate<BarController>(barModelXAxis.PrefabName, barModelXAxis, transform);


            List<BarController> barControllers = new List<BarController>();
            List<float> heights = ComputeHistogramHeights(mc1Data, regionIndex, slicingPlanePosition);

            if (heights.Count == 0)
            {
                return barControllers;
            }

            float barWidth = 6f / heights.Count;

            float interBarSpacing = barWidth / 2.5f;

            List<string> columnNames = new List<string>(Report.ColumnNameToPositionDict.Keys);

            for (int i = 0; i < heights.Count - 1; i++)
            {
                BarModel barModel = new BarModel();
                barModel.Scale = new Vector3(barWidth, barThickness, heights[i]);
                barModel.Position = new Vector3(i * (barWidth + interBarSpacing) - 3.5f, 0.1f, -2.8f + (heights[i] / 2));
                BarController barController =
                    Controller.Instantiate<BarController>(barModel.PrefabName, barModel, transform);
                barControllers.Add(barController);

                BarLabelModel barLabelModel = new BarLabelModel();
                barLabelModel.Position = new Vector3(i * (barWidth + interBarSpacing) - 3.5f, 0.2f, -2.9f);
                barLabelModel.Text = columnNames[i];
                BarLabelController barLabelController =
                    Controller.Instantiate<BarLabelController>(barLabelModel.PrefabName, barLabelModel, transform);
                barLabelControllers.Add(barLabelController);
            }

            SetActive(modelVisibility);

            return barControllers;
        }

        public List<float> ComputeHistogramHeights(Mc1Data mc1Data, List<int> regionIndex, float slicingPlanePosition)
        {
            if (regionIndex.Count == 0)
            {
                return new List<float> {0, 0, 0, 0, 0, 0, 0};
            }

            AllReportsAtTimeStamp allReportsAtTimeStamp = mc1Data.GetData(slicingPlanePosition);

            List<Report> allReports = new List<Report>();
            for (int i = 0; i < regionIndex.Count; i++)
            {
                int regionInd = regionIndex[i];
                List<Report> reports = allReportsAtTimeStamp.GetReports(regionInd);
                allReports.AddRange(reports);
            }

            Report result = new Report(new List<int> {0, 0, 0, 0, 0, 0, 0});
            foreach (Report report in allReports)
            {
                result += report;
            }


            var histogramValues = result.GetAllValuesDeepCopy().Select<int, float>(i => i).ToList();
            return DataUtil.NormalizeValues(histogramValues, 7f);
        }

        public void SetActive(bool active)
        {
            GetComponent<Renderer>().enabled = active;
            GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMesh>().GetComponent<Renderer>().enabled = active;
            barLabelControllers.ForEach(barLabelController => barLabelController.SetActive(active));
            if (barControllerXAxis)
            {
                barControllerXAxis.SetActive(active);
            }

            if (barControllerYAxis)
            {
                barControllerYAxis.SetActive(active);
            }
            // DataUtil.SetVisibility(gameObject, active);
        }

        public void SetPlotName(string plotName)
        {
            GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMesh>().text = plotName;
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
    }
}