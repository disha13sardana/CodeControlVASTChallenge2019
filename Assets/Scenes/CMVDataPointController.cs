using CodeControl;
using UnityEngine;

namespace Scenes
{
    public class CMVDataPointController : Controller<CMVDataPointModel>
    {
        private CMVDataPointView cmvDataPointView;

        public bool isBrushed;

        private void Awake()
        {
            cmvDataPointView = GetComponent<CMVDataPointView>();
        }

        protected override void OnInitialize()
        {
            cmvDataPointView.SetPosition(model.Position);
            cmvDataPointView.SetScale(model.Scale);
            Debug.Log("On initialize, setting the CMVDataPointColor to " +
                      (isBrushed ? model.BrushColor : model.Color));
            cmvDataPointView.SetColor(isBrushed ? model.BrushColor : model.Color);
            // Message.AddListener<DataPointMessage>("floor_data_sphere_clicked", OnDataPointMessageReceive);
            SetActive(model.Visibility);
            EventManager.StartListening("floor_data_sphere_clicked", OnDataPointMessageReceive);
        }

        public void UpdateScale(Vector3 newScale)
        {
            cmvDataPointView.SetScale(newScale);
        }

        public void UpdateColor(Color newColor, Color newBrushColor)
        {
            model.Color = newColor;
            model.BrushColor = newBrushColor;
            cmvDataPointView.SetColor(isBrushed ? model.BrushColor : model.Color);
        }

        private void OnDataPointMessageReceive(DataPointMessage m)
        {
            if (m.regionId == model.SerialId)
            {
                Debug.Log("Color change message received for regionId: " + m.regionId);
                isBrushed = !isBrushed;
                cmvDataPointView.SetColor(isBrushed ? model.BrushColor : model.Color);
            }
        }

        public CMVDataPointModel GetModel()
        {
            return model;
        }

        public void SetActive(bool active)
        {
            GetModel().Visibility = active;
            cmvDataPointView.SetActive(active);
        }

        public void ResetSceneUpdateColor()
        {
            cmvDataPointView.SetColor(isBrushed ? model.BrushColor : model.Color);

        }
    }
}