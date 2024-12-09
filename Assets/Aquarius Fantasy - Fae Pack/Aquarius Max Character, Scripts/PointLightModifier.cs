/*
 * Purpose: Pulse a lights intensity between two values.
 * Author: Austin Reynolds
 * Email: aumudin@gmail.com
 * Last Changed: 5/28/2020
 */
using UnityEngine;

namespace AquariusMax.Fae.demo
{
    [RequireComponent(typeof(Light))]
    public class PointLightModifier : MonoBehaviour
    {
        [Header("Settings")]
        public float Intensity = 0.0f;
        public float PulseSpeed = 0.0f;
        public float MaxIntensity = 5.0f;
        public float MinIntensity = 0.0f;

        private new Light light;

        private void Awake()
        {
            light = gameObject.GetComponent<Light>();
            light.intensity = Intensity;
        }

        private void Update()
        {
            light.intensity = MinIntensity + Mathf.PingPong(Time.time * PulseSpeed, MaxIntensity - MinIntensity);
        }
    }
}