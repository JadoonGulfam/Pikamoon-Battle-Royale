using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearObjectRules : MonoBehaviour
{
    [Tooltip("Linear Distance along spline to spawn the object")]
    public float linearDistance = 2;
    [Tooltip("Begining offset for spawning")]
    public float beginOffset = 1.0f;
}