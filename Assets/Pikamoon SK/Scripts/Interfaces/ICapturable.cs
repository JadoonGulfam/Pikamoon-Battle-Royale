
using UnityEngine;

namespace Pikamoon.Controller
{
    public struct CapturedInfo
    {
        public Transform transform;
        public float TimeToCapture;
    }

    public interface ICapturable
    {
        public bool isReadyToBeCaptured
        {
            get; set;
        }

        bool onCapture(out CapturedInfo captureReturnInfo);
    }
}