using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScrollZoom : MonoBehaviour
{
    public float minFov ;
    public float maxFov ;
    public float sensitivity = 10f;
    public GameObject virtualCamera;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2.0f);
        virtualCamera = GetComponent<PlayerController>().virtualCamera;

    }
   
    void Update()
    {
        print(Input.GetAxis("Mouse ScrollWheel"));

        if (virtualCamera != null )
        {
            float fov = virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
        }
    }
}
