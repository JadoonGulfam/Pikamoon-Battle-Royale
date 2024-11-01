using UnityEngine;

namespace Pikamoon.Controller
{
    public class Aim : MonoBehaviour
    {

        Camera cam;

        private void Start()
        {
            cam = ReferencesHolder.Instance._Camera;
        }

        public void AimStarted()
        {

        }


        public void AimEnded()
        {

        }
    }


}