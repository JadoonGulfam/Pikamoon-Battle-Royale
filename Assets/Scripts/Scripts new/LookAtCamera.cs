using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject ManiCamera;
    private void Awake()
    {
        ManiCamera = Camera.main.gameObject;
    }
    private void Update()
    {
        transform.LookAt(ManiCamera.transform);
    }
}
