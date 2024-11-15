using UnityEngine;

namespace Pikamoon.Controller
{
    public class Aim : MonoBehaviour
    {

        Camera cam;

        private void Start()
        {
            cam = ReferencesHolder.Instance._CameraController.camera;
        }

        public void AimStarted()
        {

        }


        public void AimEnded()
        {

        }
    }


}