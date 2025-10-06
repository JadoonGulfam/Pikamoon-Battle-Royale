
using UnityEngine;

namespace Pikamoon.Controller
{
    public struct CapturedInfo
    {
        public string Name;
        public Transform transform;
        public float TimeToCapture;
    }

    public interface ICapturable
    {
        public bool isReadyToBeCaptured
        {
            get; set;
        }
        void onCaptureStart();
        void onCaptureCancel();
        bool onCapture(out CapturedInfo captureReturnInfo);
        void CapturedSuccessfully(Transform _player);
    }
}