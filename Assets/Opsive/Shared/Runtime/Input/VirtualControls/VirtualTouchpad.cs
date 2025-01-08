/// ---------------------------------------------
/// Opsive Shared
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.Shared.Input.VirtualControls
{
    using Opsive.Shared.Game;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// A virtual touchpad that will move the axis based on the position of the press relative to the starting press position.
    /// </summary>
    public class VirtualTouchpad : VirtualAxis, IDragHandler
    {
        [Tooltip("Should the input value be stopped if there is no movement on the touch pad?")]
        [SerializeField] protected bool m_RequireActiveDrag;
        [Tooltip("The value to dampen the drag value by when no longer dragging. The higher the value the quicker the drag value will decrease.")]
        [SerializeField] protected float m_ActiveDragDamping = 1f;
        [Tooltip("The multiplier to apply to the touch delta position.")]
        [SerializeField] protected float m_DeltaPositionMultiplier = 20f;
        [Tooltip("Should the touchpad return the Input.GetAxis value?")]
        [SerializeField] protected bool m_UseAxisInput = false;

        private RectTransform m_RectTransform;
        private Vector2 m_LocalStartPosition;
        private Vector2 m_LastDragPosition;
        private Transform m_CanvasScalarTransform;
        private ScheduledEventBase m_ActiveDragScheduler;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            m_RectTransform = GetComponent<RectTransform>();
            m_CanvasScalarTransform = GetComponentInParent<CanvasScaler>().transform;
        }

        /// <summary>
        /// Callback when a pointer has pressed on the button.
        /// </summary>
        /// <param name="data">The pointer data.</param>
        public override void OnPointerDown(PointerEventData data)
        {
            base.OnPointerDown(data);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_RectTransform, data.pressPosition, null, out m_LocalStartPosition);

            // Reset the last drag position.
            m_LastDragPosition = Vector2.zero;
        }

        /// <summary>
        /// Callback when a pointer has dragged the button.
        /// </summary>
        /// <param name="data">The pointer data.</param>
        public void OnDrag(PointerEventData data)
        {
            if (!m_UseAxisInput && RectTransformUtility.RectangleContainsScreenPoint(m_RectTransform, data.position, null)) {
                var canvasScale = m_CanvasScalarTransform == null ? Vector3.one : m_CanvasScalarTransform.localScale;
                m_DeltaPosition.x += data.delta.x * m_DeltaPositionMultiplier / canvasScale.x;
                m_DeltaPosition.y += data.delta.y * m_DeltaPositionMultiplier / canvasScale.y;
                Scheduler.Cancel(m_ActiveDragScheduler);
                if (m_RequireActiveDrag) {
                    m_ActiveDragScheduler = Scheduler.Schedule(Time.fixedDeltaTime, DampenDeltaPosition);
                }

                // Store the current drag position.
                m_LastDragPosition = data.position;
            }
        }

        /// <summary>
        /// Track the drag throughout FixedUpdate.
        /// </summary>
        private void FixedUpdate()
        {
            if (m_Pressed && m_LastDragPosition != Vector2.zero) {
                var lastDragPosition = m_LastDragPosition;
                var dragDelta = lastDragPosition - m_LastDragPosition;
                var canvasScale = m_CanvasScalarTransform == null ? Vector3.one : m_CanvasScalarTransform.localScale;
                m_DeltaPosition.x = dragDelta.x * m_DeltaPositionMultiplier / canvasScale.x;
                m_DeltaPosition.y = dragDelta.y * m_DeltaPositionMultiplier / canvasScale.y;

                // Update the last drag position for the next frame.
                m_LastDragPosition = lastDragPosition;
            }
        }

        /// <summary>
        /// Clears the delta drag position.
        /// </summary>
        private void DampenDeltaPosition()
        {
            m_DeltaPosition /= (1 + m_ActiveDragDamping);
            if (m_DeltaPosition.sqrMagnitude > 0.1f) {
                m_ActiveDragScheduler = Scheduler.Schedule(Time.fixedDeltaTime, DampenDeltaPosition);
            }
        }

        /// <summary>
        /// Returns the value of the axis.
        /// </summary>
        /// <param name="buttonName">The name of the axis.</param>
        /// <returns>The value of the axis.</returns>
        public override float GetAxis(string buttonName)
        {
            if (!m_Pressed) {
                return 0;
            }

            if (m_UseAxisInput && (buttonName == m_HorizontalInputName || buttonName == m_VerticalInputName)) {
                return Input.GetAxis(buttonName);
            }

            if (buttonName == m_HorizontalInputName) {
                return m_DeltaPosition.x / (m_RectTransform.sizeDelta.x - m_LocalStartPosition.x);
            }
            return m_DeltaPosition.y / (m_RectTransform.sizeDelta.y - m_LocalStartPosition.y);
        }
    }
}