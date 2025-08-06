using Pikamoon.Controller;
using UnityEngine;

public class MiniMapHandler : MonoBehaviour
{
    public Transform MyMMPointer;

    public Vector3 CamOffsetFromPlayer;

    Transform MiniMapCam;

    bool isInitialize;

    Pikamoon.Controller.CameraController cameraController;

    public void Initialize(Pikamoon.Controller.CameraController _cameraController)
    {
        cameraController = _cameraController;

        MiniMapCam = cameraController.miniMapCamera.transform;
        MyMMPointer.gameObject.SetActive(true);

        isInitialize = true;
    }


    private void LateUpdate()
    {
        if (!isInitialize)
            return;

        if (cameraController.isLargeMiniMapVisible)
        {

        }
        else
        {
            UpdateMiniMapCamTransform();
            UpdatePointerTransform();
        }



    }

    void UpdateMiniMapCamTransform()
    {
        MiniMapCam.transform.position = this.transform.position + CamOffsetFromPlayer;
    }
    void UpdatePointerTransform()
    {
        MyMMPointer.transform.position = this.transform.position;

        Quaternion desiredRotation = MyMMPointer.transform.rotation;

        desiredRotation.x = 0f;
        desiredRotation.z = 0f;

        MyMMPointer.transform.rotation = desiredRotation;
    }

}
