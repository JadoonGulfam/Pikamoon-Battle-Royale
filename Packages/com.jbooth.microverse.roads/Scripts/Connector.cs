using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JBooth.MicroVerseCore
{
    public class Connector : MonoBehaviour
    {
        [Tooltip("Road to spawn from this connection point")]
        public RoadConfig config;

        public Color color = new Color(1, 0.8f, 0.4f, 1);
    }
}
