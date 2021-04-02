using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

namespace Scenes
{
    public class SurfaceMagnetismEnabler : MonoBehaviour, IMixedRealityGestureHandler<Vector3>
    {
        private bool isSurfaceMagnetismEnabled = false;

        public void OnGestureStarted(InputEventData eventData)
        {
            //throw new System.NotImplementedException();
            Debug.Log($"OnGestureStarted [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

            var action = eventData.MixedRealityInputAction.Description;
            if (action == "Hold Action")
            {
                //SetIndicator(HoldIndicator, "Hold: started", HoldMaterial);
            }
            else if (action == "Manipulate Action")
            {
                //SetIndicator(ManipulationIndicator, $"Manipulation: started {Vector3.zero}", ManipulationMaterial, Vector3.zero);
            }
            else if (action == "Navigation Action")
            {
                //SetIndicator(NavigationIndicator, $"Navigation: started {Vector3.zero}", NavigationMaterial, Vector3.zero);
                //ShowRails(Vector3.zero);
            }

            //SetIndicator(SelectIndicator, "Select:", DefaultMaterial);
            Debug.Log("Selected.");
            isSurfaceMagnetismEnabled = true;
            GetComponent<SurfaceMagnetism>().enabled = true;
        }

        public void OnGestureUpdated(InputEventData eventData)
        {
            //throw new System.NotImplementedException();
        }

        public void OnGestureCompleted(InputEventData eventData)
        {
            //throw new System.NotImplementedException();
            Debug.Log($"OnGestureCompleted [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

            var action = eventData.MixedRealityInputAction.Description;
            if (action == "Hold Action")
            {
                //SetIndicator(HoldIndicator, "Hold: completed", DefaultMaterial);
            }
            else if (action == "Select")
            {
                //SetIndicator(SelectIndicator, "Select: completed", SelectMaterial);
                isSurfaceMagnetismEnabled = false;
                Debug.Log("Selected.");
                GetComponent<SurfaceMagnetism>().enabled = false;
            }
        }

        public void OnGestureCanceled(InputEventData eventData)
        {
            //throw new System.NotImplementedException();
        }

        public void OnGestureUpdated(InputEventData<Vector3> eventData)
        {
            //throw new System.NotImplementedException();
        }

        public void OnGestureCompleted(InputEventData<Vector3> eventData)
        {
            //throw new System.NotImplementedException();
        }
    }
}