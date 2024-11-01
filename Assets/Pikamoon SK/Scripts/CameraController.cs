using Unity.Cinemachine;
using Pikamoon.Controller;
using UnityEngine;
namespace Pikamoon.Controller
{
public class CameraController : MonoBehaviour
{
    PlayerInput input;

    [SerializeField] CinemachineFreeLook BasicCam;
    [SerializeField] CinemachineFreeLook SprintCam;
    [SerializeField] CinemachineVirtualCamera AimCam;


    private void Start()
    {
        input = ReferencesHolder.Instance._playerInput;
    }

    private void Update()
    {
        CheckIfSprinting();
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
        if(flag)
        {
            if (SprintCam.Priority == 0)
            { 
                SprintCam.m_XAxis.Value = BasicCam.m_XAxis.Value;
                SprintCam.m_YAxis.Value = BasicCam.m_YAxis.Value;
            }
        }
        else
        {
            if (BasicCam.Priority == 0)
            { 
                BasicCam.m_XAxis.Value = SprintCam.m_XAxis.Value;
                BasicCam.m_YAxis.Value = SprintCam.m_YAxis.Value;
            }
        }
        BasicCam.Priority = flag ? 0 : 1;
        SprintCam.Priority = flag ? 1 : 0;
    }
}
}