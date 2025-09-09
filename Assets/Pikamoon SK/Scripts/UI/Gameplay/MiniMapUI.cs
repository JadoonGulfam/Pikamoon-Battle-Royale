using Pikamoon.Controller;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pikamoon.UI
{
    public class MiniMapUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [Header("Minimap Camera")]
        [SerializeField] Camera cam;
        [SerializeField] LayerMask minimapLayer;

        [Header("Player Reference")]
        [SerializeField] Transform MyPlayer;
        public bool isPlayerFollowing = true;
        public Vector3 PlayerFollowoffset = Vector3.zero; 
        
        [Header("Camera Clamp Bounds")]
        public Vector2 clampMin = new Vector2(-100, -100); // X and Z min
        public Vector2 clampMax = new Vector2(100, 100);   // X and Z max

        [Header("Zoom")]
        public float zoomSpeed = 10f;
        public float minSize = 10f;
        public float maxSize = 100f;
        public float defaultZoomSize = 50f; // ← add this

        [Header("Drag")]
        public float dragSpeed = 0.1f;
        private Vector3 dragOrigin;

        [Header("Marker + Line")]
        public Transform Marker; // UI marker
        public LineRenderer lineRenderer;
        Vector3 worldTargetPosition;
        private PlayerInput input;
        bool isDragging;
        Coroutine resetCoroutine;

        public void Initialize(Transform Player, PlayerInput _input)
        {
            input = _input;
            MyPlayer = Player;

            isDragging = false;
            isPlayerFollowing = false;

            PlayerFollowoffset = Vector3.up * cam.transform.position.y;

            input.onJump_Down += ResetCameraToPlayer;
        }

        void OnEnable()
        {
            isPlayerFollowing = false;
            // ResetCameraToPlayer();
        }

        void Update()
        {
            //if (isPlayerFollowing && MyPlayer != null)
            //{
            //    ViewMyLocation(); // follow player
            //}

            ManageZoom();
        }

        void ManageZoom()
        {
            if (isPlayerFollowing)
                return;

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minSize, maxSize);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isPlayerFollowing)
                return;

            isDragging = true;
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            isDragging = true;

            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += new Vector3(difference.x, 0, difference.z);

            ClampCameraPosition();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isDragging)
                return;


            // Right-click = remove marker
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                RemoveMarker();
                return;
            }

            // Left-click = place marker
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //Ray ray = cam.ScreenPointToRay(eventData.position);
                //Plane plane = new Plane(Vector3.up, Vector3.zero);
                //float distance;

                //if (plane.Raycast(ray, out distance))
                //{
                //    worldTargetPosition = ray.GetPoint(distance);

                //    AddMarker();
                //}



                Ray ray = cam.ScreenPointToRay(eventData.position);

                // 1. Try physics raycast (recommended if you have terrain or ground colliders)
                if (Physics.Raycast(ray, out RaycastHit hit, 3000f, minimapLayer))
                {
                    worldTargetPosition = hit.point;
                    AddMarker();
                }
            }



        }
        void ClampCameraPosition()
        {
            Vector3 pos = cam.transform.position;

            pos.x = Mathf.Clamp(pos.x, clampMin.x, clampMax.x);
            pos.z = Mathf.Clamp(pos.z, clampMin.y, clampMax.y);

            cam.transform.position = pos;
        }

        public void AddMarker()
        {
            if (Marker == null) return;

            // Enable and move marker
            Marker.gameObject.SetActive(true);
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.positionCount = 2;

            worldTargetPosition.y = 5;

            Marker.position = worldTargetPosition;
        }

        public void RemoveMarker()
        {
            if (Marker == null) return;

            Marker.gameObject.SetActive(false);

            lineRenderer.gameObject.SetActive(false);

            lineRenderer.positionCount = 0;
        }

        public void RemoveAllMarker()
        {
            RemoveMarker(); // In case you add multi-marker later
        }

        public void ViewMyLocation()
        {
            if (cam == null || MyPlayer == null) return;

            Vector3 newPos = MyPlayer.position + PlayerFollowoffset;

            cam.transform.position = newPos;

            ClampCameraPosition();
        }

        IEnumerator SmoothZoom(float targetSize, float duration = 0.5f)
        {
            float startSize = cam.orthographicSize;
            float time = 0f;

            Vector3 startPosition = cam.transform.position;


            while (time < duration)
            {
                cam.orthographicSize = Mathf.Lerp(startSize, targetSize, time / duration);
                cam.transform.position = Vector3.Lerp(startPosition, MyPlayer.position + PlayerFollowoffset, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            isPlayerFollowing = false;
            cam.orthographicSize = targetSize;
        }

        // 📌 Call this from a button to manually reset camera
        public void ResetCameraToPlayer()
        {
            if (!gameObject.activeInHierarchy || isPlayerFollowing)
                return;

            isPlayerFollowing = true;

            // Smoothly reset zoom
            resetCoroutine = StartCoroutine(SmoothZoom(Mathf.Clamp(defaultZoomSize, minSize, maxSize)));

            //// Move camera to follow player immediately
            //ViewMyLocation();
        }


        private void OnDisable()
        {
            if (resetCoroutine != null)
                StopCoroutine(resetCoroutine);

            isPlayerFollowing = false;
        }
    }

}