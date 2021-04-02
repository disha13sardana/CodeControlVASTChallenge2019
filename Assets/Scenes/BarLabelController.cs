using CodeControl;

namespace Scenes
{
    public class BarLabelController : Controller<BarLabelModel>
    {
        private BarLabelView barLabelView;

        private void Awake()
        {
            barLabelView = GetComponent<BarLabelView>();
        }

        protected override void OnInitialize()
        {
            barLabelView.SetScale(model.Scale);
            barLabelView.SetPosition(model.Position);
            barLabelView.SetRotation(model.Rotation);
            barLabelView.SetText(model.Text);
            barLabelView.SetColor(model.Color);
            barLabelView.SetActive(model.Visibility);
        }

        public BarLabelModel GetModel()
        {
            return model;
        }

        public void SetActive(bool active)
        {
            GetModel().Visibility = active;
            barLabelView.SetActive(GetModel().Visibility);
        }
    }
}