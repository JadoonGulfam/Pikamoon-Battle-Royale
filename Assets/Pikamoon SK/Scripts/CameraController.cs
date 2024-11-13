using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;

namespace Pikamoon.Controller
{
    public enum Cam
    {
        Default,
        Sprint,
        Aim
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
        public Camera camera;
        [Space]
        [SerializeField] Cam activeCam;
        [Space]
        [SerializeField] CinemachineCamera DefaultCam;
        [SerializeField] CinemachineOrbitalFollow DefaultCamOrbitalFollow;

        [SerializeField] CinemachineCameraOffset camOffsetter;

        [SerializeField] CinemachineCamera SprintCam;
        [SerializeField] CinemachineOrbitalFollow SprintCamOrbitalFollow;
        //[SerializeField] CinemachineFreeLook AimCam;

        [Space]
        public CamSettings[] camRigSettings;

        public float Default_X_Axis;
        public float Default_Y_Axis;

        CinemachineVirtualCameraBase PreviousCam;

        Vector3 aimer;
        float fov;
        bool isAim = false;

        private void Start()
        {
            input = ReferencesHolder.Instance._playerInput;
            activeCam = Cam.Default;

            DefaultCam.gameObject.SetActive(true);
            SprintCam.gameObject.SetActive(false);

            aimer = camRigSettings[0].CamOffset;
            fov = camRigSettings[0].FOV;
            //DefaultCam.m_Orbits[0].m_Radius = camRigSettings[0].rigs[0].Radius;
            //DefaultCam.m_Orbits[1].m_Radius = camRigSettings[0].rigs[1].Radius;
            //DefaultCam.m_Orbits[2].m_Radius = camRigSettings[0].rigs[2].Radius;

        }

        private void Update()
        {
            //CheckIfSprinting();

            camOffsetter.Offset = Vector3.Lerp(camOffsetter.Offset, aimer, Time.deltaTime * 3);
            DefaultCam.Lens.FieldOfView = Mathf.Lerp(DefaultCam.Lens.FieldOfView, fov, Time.deltaTime * 2);
        }

        void CheckIfSprinting()
        {
            if (input.isMoving)
            {
                ToggleSprintCam(input.isSprinting);
            }
            else
            {
                ToggleSprintCam(false);
            }
        }

        public void ToggleSprintCam(bool flag)
        {
            if (flag)
            {
                if (SprintCam.Priority == 0)
                {
                    SprintCamOrbitalFollow.HorizontalAxis.Value = DefaultCamOrbitalFollow.HorizontalAxis.Value;
                    SprintCamOrbitalFollow.VerticalAxis.Value = DefaultCamOrbitalFollow.VerticalAxis.Value;
                }
            }
            else
            {
                if (DefaultCam.Priority == 0)
                {
                    DefaultCamOrbitalFollow.HorizontalAxis.Value = SprintCamOrbitalFollow.HorizontalAxis.Value;
                    DefaultCamOrbitalFollow.VerticalAxis.Value = SprintCamOrbitalFollow.VerticalAxis.Value;
                }
            }


            DefaultCam.Priority = flag ? 0 : 1;
            SprintCam.Priority = flag ? 1 : 0;
        }


        public void ChangeCam(Cam changeTo)
        {
            if (changeTo == activeCam)
                return;


            if (activeCam == Cam.Default)
            {
                Default_X_Axis = DefaultCamOrbitalFollow.HorizontalAxis.Value;
                Default_Y_Axis = DefaultCamOrbitalFollow.VerticalAxis.Value;

            }
            else if (activeCam == Cam.Sprint)
            {
                Default_X_Axis = SprintCamOrbitalFollow.HorizontalAxis.Value;
                Default_Y_Axis = SprintCamOrbitalFollow.VerticalAxis.Value;

                SprintCam.gameObject.SetActive(false);
            }
            else if (activeCam == Cam.Aim)
            {
                Default_X_Axis = DefaultCamOrbitalFollow.HorizontalAxis.Value;
                Default_Y_Axis = DefaultCamOrbitalFollow.VerticalAxis.Value;
            }




            if (changeTo == Cam.Default)
            {
                DefaultCamOrbitalFollow.HorizontalAxis.Value = Default_X_Axis;
                DefaultCamOrbitalFollow.VerticalAxis.Value = Default_Y_Axis;

                aimer = camRigSettings[0].CamOffset;
                fov = camRigSettings[0].FOV;
                //DefaultCam.m_Orbits[0].m_Radius = camRigSettings[0].rigs[0].Radius;
                //DefaultCam.m_Orbits[1].m_Radius = camRigSettings[0].rigs[1].Radius;
                //DefaultCam.m_Orbits[2].m_Radius = camRigSettings[0].rigs[2].Radius;


                DefaultCam.gameObject.SetActive(true);
            }
            else if (changeTo == Cam.Sprint)
            {
                SprintCamOrbitalFollow.HorizontalAxis.Value = Default_X_Axis;
                SprintCamOrbitalFollow.VerticalAxis.Value = Default_Y_Axis;

                SprintCam.gameObject.SetActive(true);
            }
            else if (changeTo == Cam.Aim)
            {
                DefaultCamOrbitalFollow.HorizontalAxis.Value = Default_X_Axis;
                DefaultCamOrbitalFollow.VerticalAxis.Value = Default_Y_Axis;

                aimer = camRigSettings[isAim ? 2 : 1].CamOffset;

                fov = camRigSettings[isAim ? 2 : 1].FOV;
                //DefaultCam.m_Orbits[0].m_Radius = camRigSettings[1].rigs[0].Radius;
                //DefaultCam.m_Orbits[1].m_Radius = camRigSettings[1].rigs[1].Radius;
                //DefaultCam.m_Orbits[2].m_Radius = camRigSettings[1].rigs[2].Radius;

                DefaultCam.gameObject.SetActive(true);
            }

            activeCam = changeTo;

        }


        public void ChangeAimZoom(bool isAiming)
        {
            if (activeCam == Cam.Aim)
            {
                isAim = isAiming;
                if (isAiming)
                {
                    aimer = camRigSettings[2].CamOffset;
                    fov = camRigSettings[2].FOV;
                }
                else
                {
                    aimer = camRigSettings[1].CamOffset;
                    fov = camRigSettings[1].FOV;
                }
            }
        }
    }
}