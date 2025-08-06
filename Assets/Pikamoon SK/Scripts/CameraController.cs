using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;
using System;

namespace Pikamoon.Controller
{
    public enum Cam
    {
        Default,
        Sprint,
        Crouch,
        Slide,
        Aim,
        ZoomedAim,
        Capture
    }


    [System.Serializable]
    public struct CamRigSetting
    {
        public string Name;
        [Space]
        public float Height;
        public float Radius;
    }


    [System.Serializable]
    public struct CamSettings
    {
        public string Name;
        [Space]
        public Vector3 CamOffset;
        public float FOV;
    }


    [Serializable]
    public struct MiniMapTypeSetting
    {
        public int OrthographicSize;
    }

    public class CameraController : MonoBehaviour
    {
        public Camera _camera;
        [Space]
        [SerializeField] Cam activeCam;

        [Header("Arrow Action Cam")]
        [Space]
        [SerializeField] CinemachineCamera ActionArrowCam;
        [SerializeField] CinemachineBasicMultiChannelPerlin shakeNoisePerlin;
        [SerializeField] float cameraShakeAmplitude; 
        [SerializeField] float cameraShakeFrequency; 

        [Header("Default Cam Property")]
        [Space]
        [SerializeField] CinemachineCamera DefaultCam;
        [SerializeField] CinemachineOrbitalFollow DefaultCamOrbitalFollow;
        [SerializeField] CinemachineCameraOffset camOffsetter;
        [Space]
        public CamSettings[] camRigSettings;

        [Header("Minimap Settings")]
        [Space]
        public Camera miniMapCamera;
        public int SmallMinimap_OrthographicSize;
        public int LargeMinimap_OrthographicSize;
        public bool isLargeMiniMapVisible;

        CinemachineInputAxisController CamAxisController;

        Vector3 aimer;
        float fov;
        bool isAim = false;
        Transform Target;

        private void Start()
        {
            activeCam = Cam.Default;

            CamAxisController = DefaultCam.GetComponent<CinemachineInputAxisController>();
            shakeNoisePerlin.AmplitudeGain = 0;
            shakeNoisePerlin.FrequencyGain = 0;

            DefaultCam.gameObject.SetActive(true);

            aimer = camRigSettings[0].CamOffset;
            fov = camRigSettings[0].FOV;

        }

        private void Update()
        {
            if ( Target == null )
                return;

            camOffsetter.Offset = Vector3.Lerp(camOffsetter.Offset, aimer, Time.deltaTime * 3);
            DefaultCam.Lens.FieldOfView = Mathf.Lerp(DefaultCam.Lens.FieldOfView, fov, Time.deltaTime * 2);


        }

        public void AssignPlayer(Transform FollowTarget, Transform LookTarget)
        {
            Target = FollowTarget;

            DefaultCam.Follow = FollowTarget;
            DefaultCam.LookAt = LookTarget;
        }

        public void ChangeCam(Cam changeTo)
        {
            if (changeTo == activeCam)
                return;
            
            aimer = camRigSettings[(int)changeTo].CamOffset;
            fov = camRigSettings[(int)changeTo].FOV;

            activeCam = changeTo;

        }


        public void ToggleCaptureCam(bool flag)
        {
            if(flag)
            {
                ChangeCam(Cam.Capture);
            }
            else
            {
                ChangeCam(Cam.Default);
            }
        }

        public void ChangeAimZoom(bool isAiming)
        {
            isAim = isAiming;
            if (isAiming)
            {
                ChangeCam(Cam.ZoomedAim);
            }
            else
            {
                ChangeCam(Cam.Aim);
            }
        }

        public void EnableBulletActionCam(Transform Bullet)
        {
            ActionArrowCam.transform.parent = Bullet;
            ActionArrowCam.transform.localPosition = Vector3.zero;
            ActionArrowCam.transform.localRotation = Quaternion.identity;

            // Set initial shake

            Time.timeScale = 0.08f;

            shakeNoisePerlin.AmplitudeGain = cameraShakeAmplitude;
            shakeNoisePerlin.FrequencyGain = cameraShakeFrequency;



            ActionArrowCam.gameObject.SetActive(true);
            DefaultCam.gameObject.SetActive(false);
        }

        public void DisableBulletActionCam()
        {
            Time.timeScale = 1f;

            ActionArrowCam.gameObject.SetActive(false);
            DefaultCam.gameObject.SetActive(true);
        }


        public void CameraOrbitStatus(bool flag)
        {

            if (flag)
            {
                if (!CamAxisController.enabled)
                    CamAxisController.enabled = true;
            }
            else
            {
                if (CamAxisController.enabled)
                    CamAxisController.enabled = false;

            }

        }

    }
}