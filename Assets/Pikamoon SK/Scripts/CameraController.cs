using Unity.Cinemachine;
using UnityEngine;

namespace Pikamoon.Controller
{
    public enum Cam
    {
        Default,
        Sprint,
        Crouch,
        Slide,
        Aim,
        ZoomedAim
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


    public class CameraController : MonoBehaviour
    {
        PlayerInput input;
        public Camera _camera;
        [Space]
        [SerializeField] Cam activeCam;

        [Space]
        [SerializeField] CinemachineCamera CaptureCam;
        [SerializeField] CinemachineCamera DefaultCam;
        [SerializeField] CinemachineOrbitalFollow DefaultCamOrbitalFollow;

        [SerializeField] CinemachineCameraOffset camOffsetter;

        [Space]
        public CamSettings[] camRigSettings;

        CinemachineVirtualCameraBase PreviousCam;

        Vector3 aimer;
        float fov;
        bool isAim = false;
        Transform Target;

        private void Start()
        {
            input = ReferencesHolder.Instance._playerInput;
            activeCam = Cam.Default;

            DefaultCam.gameObject.SetActive(true);

            aimer = camRigSettings[0].CamOffset;
            fov = camRigSettings[0].FOV;

            Application.targetFrameRate = 120;
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

            if (changeTo == Cam.Default)
            {
                aimer = camRigSettings[0].CamOffset;
                fov = camRigSettings[0].FOV;

            }
            else if (changeTo == Cam.Sprint)
            {
                aimer = camRigSettings[1].CamOffset;
                fov = camRigSettings[1].FOV;

            }
            else if (changeTo == Cam.Crouch)
            {
                aimer = camRigSettings[2].CamOffset;
                fov = camRigSettings[2].FOV;

            }
            else if (changeTo == Cam.Slide)
            {
                aimer = camRigSettings[3].CamOffset;
                fov = camRigSettings[3].FOV;

            }
            else if (changeTo == Cam.Aim)
            {
                aimer = camRigSettings[4].CamOffset;
                fov = camRigSettings[4].FOV;

            }
            else if (changeTo == Cam.ZoomedAim)
            {
                aimer = camRigSettings[5].CamOffset;
                fov = camRigSettings[5].FOV;

            }
            activeCam = changeTo;

        }


        public void ToggleCaptureCam(bool flag)
        {
            DefaultCam.gameObject.SetActive(!flag);
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
    }
}