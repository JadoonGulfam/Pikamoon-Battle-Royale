using Cinemachine;
using SickscoreGames.HUDNavigationSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CameraScrollZoom : MonoBehaviour
{
    public float minFov ;
    public float maxFov ;
    public float sensitivity = 10f;
    public GameObject virtualCameraDistance;
    Cinemachine3rdPersonFollow setDistance;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        GameObject temp=  GameObject.FindWithTag("HUD");
        temp.GetComponent<HUDNavigationSystem>().PlayerController=gameObject.transform;

        yield return new WaitForSeconds(1.0f);
        virtualCameraDistance = GameObject.Find("PlayerFollowCamera");
        setDistance = virtualCameraDistance.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>();
      
    }
   
    void Update()
    {
      
        if (virtualCameraDistance != null )
        {
            float fov = setDistance.CameraDistance;
            fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            setDistance.CameraDistance = fov;
        }

       


    }
}
