using CodeControl;
using UnityEngine;

namespace Scenes
{
    public class BarController : Controller<BarModel>
    {
        private BarView barView;

        public void Awake()
        {
            barView = GetComponent<BarView>();
        }

        protected override void OnInitialize()
        {
            barView.SetPosition(model.Position);
            barView.SetScale(model.Scale);
            barView.SetColor(model.Color);
            barView.SetRotation(model.Rotation);
            SetActive(model.Visibility);
        }

        public void UpdateScale(Vector3 scale)
        {
            barView.SetScale(scale);
        }

        public void UpdateColor(Color color)
        {
            barView.SetColor(color);
        }

        public BarModel GetModel()
        {
            return model;
        }

        public void SetActive(bool active)
        {
            GetModel().Visibility = active;
            GetComponent<Renderer>().enabled = active;
        }
    }
}