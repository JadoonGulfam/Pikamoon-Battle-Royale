using Pikamoon.Controller;
using UnityEngine;

public class MiniMapHandler : MonoBehaviour
{
    public Transform MyMMPointer;

    public Transform MarkedLocation;
    public LineRenderer lineRenderer;

    public Vector3 CamOffsetFromPlayer;

    Transform smallMiniMapCam;
    Transform LargeMiniMapCamForUI;

    PlayerController Controller;
    Pikamoon.Controller.CameraController cameraController;

    bool isInitialize;
    bool isMapOpened = false;
    public void Initialize(Pikamoon.Controller.CameraController _cameraController, PlayerController playerController)
    {
        cameraController = _cameraController;
        Controller = playerController;

        Controller.input.onMap_Down += ToggleMap;

        smallMiniMapCam = cameraController.hudMiniMapCamera.transform;
        LargeMiniMapCamForUI = cameraController.largeMiniMapCamera.transform;

        MyMMPointer.gameObject.SetActive(true);
        MarkedLocation = Controller.UI.hudcontroller.miniMapUI.Marker;
        lineRenderer = Controller.UI.hudcontroller.miniMapUI.lineRenderer;


        isInitialize = true;
        isMapOpened = false;
    }

    public void ToggleMap()
    {
        isMapOpened = !isMapOpened;

        if (isMapOpened)
        {
            Controller.UI.hudcontroller.miniMapUI.gameObject.SetActive(true);

            LargeMiniMapCamForUI.gameObject.SetActive(true);
            smallMiniMapCam.gameObject.SetActive(false);

            Controller.ToggleCursor(true);

            Controller.CameraOrbitStatus = false;
            Controller.IsUIOpened = true;
        }
        else
        {
            smallMiniMapCam.gameObject.SetActive(true);
            LargeMiniMapCamForUI.gameObject.SetActive(false);

            Controller.UI.hudcontroller.miniMapUI.gameObject.SetActive(false);

            Controller.ToggleCursor(false);

            Controller.CameraOrbitStatus = true;
            Controller.IsUIOpened = false;
        }
    }


    private void LateUpdate()
    {
        if (!isInitialize)
            return;

        if (isMapOpened)
        {

        }
        else
        {
            UpdateMiniMapCamTransform();
            UpdatePointerTransform();
        }

        UpdateLineRenderer();

    }

    private void UpdateLineRenderer()
    {
        if (MarkedLocation)
        {
            if (lineRenderer.gameObject.activeInHierarchy)
            {
                Vector3 playerPos = this.transform.position;
                Vector3 markerPos = MarkedLocation.position;

                playerPos.y = 1f;
                markerPos.y = 1f;

                lineRenderer.SetPosition(0, playerPos); // slightly above ground
                lineRenderer.SetPosition(1, markerPos); // slightly above ground

            }
        }
    }


    void UpdateMiniMapCamTransform()
    {
        smallMiniMapCam.transform.position = this.transform.position + CamOffsetFromPlayer;
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
