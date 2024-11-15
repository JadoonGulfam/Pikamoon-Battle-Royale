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
        [SerializeField][System.Obsolete] CinemachineFreeLook DefaultCam;
        [SerializeField] CinemachineCameraOffset camOffsetter;

        [SerializeField][System.Obsolete] CinemachineFreeLook SprintCam;
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

            camOffsetter.Offset = Vector3.Lerp(camOffsetter.Offset, aimer,Time.deltaTime * 3);
            DefaultCam.m_Lens.FieldOfView = Mathf.Lerp(DefaultCam.m_Lens.FieldOfView, fov,Time.deltaTime *2);
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
                    SprintCam.m_XAxis.Value = DefaultCam.m_XAxis.Value;
                    SprintCam.m_YAxis.Value = DefaultCam.m_YAxis.Value;
                }
            }
            else
            {
                if (DefaultCam.Priority == 0)
                {
                    DefaultCam.m_XAxis.Value = SprintCam.m_XAxis.Value;
                    DefaultCam.m_YAxis.Value = SprintCam.m_YAxis.Value;
                }
            }


            DefaultCam.Priority = flag ? 0 : 1;
            SprintCam.Priority = flag ? 1 : 0;
        }


        public void ChangeCam(Cam changeTo)
        {
            if(changeTo == activeCam)
                return;


            if(activeCam == Cam.Default)
            {
                Default_X_Axis = DefaultCam.m_XAxis.Value;
                Default_Y_Axis = DefaultCam.m_YAxis.Value;

            }
            else if(activeCam == Cam.Sprint)
            {
                Default_X_Axis = SprintCam.m_XAxis.Value;
                Default_Y_Axis = SprintCam.m_YAxis.Value;

                SprintCam.gameObject.SetActive(false);
            }
            else if(activeCam == Cam.Aim)
            {
                Default_X_Axis = DefaultCam.m_XAxis.Value;
                Default_Y_Axis = DefaultCam.m_YAxis.Value;
            }




            if (changeTo == Cam.Default)
            {
                DefaultCam.m_XAxis.Value = Default_X_Axis;
                DefaultCam.m_YAxis.Value = Default_Y_Axis;

                aimer = camRigSettings[0].CamOffset;
                fov = camRigSettings[0].FOV;
                //DefaultCam.m_Orbits[0].m_Radius = camRigSettings[0].rigs[0].Radius;
                //DefaultCam.m_Orbits[1].m_Radius = camRigSettings[0].rigs[1].Radius;
                //DefaultCam.m_Orbits[2].m_Radius = camRigSettings[0].rigs[2].Radius;


                DefaultCam.gameObject.SetActive(true);
            }
            else if (changeTo == Cam.Sprint)
            {
                SprintCam.m_XAxis.Value = Default_X_Axis;
                SprintCam.m_YAxis.Value = Default_Y_Axis;

                SprintCam.gameObject.SetActive(true);
            }
            else if (changeTo == Cam.Aim)
            {
                DefaultCam.m_XAxis.Value = Default_X_Axis;
                DefaultCam.m_YAxis.Value = Default_Y_Axis;

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
            if(activeCam == Cam.Aim)
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