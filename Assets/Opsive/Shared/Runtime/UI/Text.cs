/// ---------------------------------------------
/// Opsive Shared
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.Shared.UI
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Specifies the text alignment.
    /// </summary>
    public enum TextAlignment
    { 
        TopLeft,        // The text should be aligned to the top left. 
        TopCenter,      // The text should be aligned to the top center. 
        TopRight,       // The text should be aligned to the top right. 
        MiddleLeft,     // The text should be aligned to the middle left. 
        MiddleCenter,   // The text should be aligned to the middle center. 
        MiddleRight,    // The text should be aligned to the middle right. 
        BottomLeft,     // The text should be aligned to the bottom left. 
        BottomCenter,   // The text should be aligned to the bottom center. 
        BottomRight     // The text should be aligned to the bottom right. 
    }

    /// <summary>
    /// A struct that allows you to choose whether to use TMP_Text or UnityEngine.UI.Text.
    /// </summary>
    [Serializable]
    public struct Text
    {
        [Tooltip("Unity Engine UI Text.")]
        [SerializeField] private UnityEngine.UI.Text m_UnityText;

        public UnityEngine.UI.Text UnityText
        {
            get { return m_UnityText; }
            set => m_UnityText = value;
        }

#if TEXTMESH_PRO_PRESENT
        [Tooltip("Text Mesh Pro Text.")]
        [SerializeField] private TMPro.TMP_Text m_TextMeshProText;

        public TMPro.TMP_Text TextMeshProText
        {
            get { return m_TextMeshProText; }
            set => m_TextMeshProText = value;
        }
#endif

        /// <summary>
        /// The struct constructor.
        /// </summary>
        /// <param name="unityText">The unity text.</param>
        public Text(UnityEngine.UI.Text unityText)
        {
            m_UnityText = unityText;
#if TEXTMESH_PRO_PRESENT
            m_TextMeshProText = null;
#endif
        }

#if TEXTMESH_PRO_PRESENT
        /// <summary>
        /// The struct constructor.
        /// </summary>
        /// <param name="textMeshProText">The text mesh pro tex.</param>
        public Text(TMPro.TMP_Text textMeshProText)
        {
            m_UnityText = null;
            m_TextMeshProText = textMeshProText;
        }
#endif

        public string text
        {
            get {
#if TEXTMESH_PRO_PRESENT
                return m_TextMeshProText?.text ?? m_UnityText?.text;
#else
                return m_UnityText?.text ?? null;
#endif
            }
            set => SetText(value);
        }

        public GameObject gameObject
        {
            get {
#if TEXTMESH_PRO_PRESENT
                return m_TextMeshProText?.gameObject ?? m_UnityText?.gameObject;
#else
                return m_UnityText?.gameObject;
#endif
            }
        }

        public bool enabled
        {
            get {
#if TEXTMESH_PRO_PRESENT
                if (m_TextMeshProText != null) {
                    return m_TextMeshProText.enabled;
                }
#endif
                if (m_UnityText != null) {
                    return m_UnityText.enabled;
                }

                return false;
            }
            set {
#if TEXTMESH_PRO_PRESENT
                if (m_TextMeshProText != null) {
                    m_TextMeshProText.enabled = value;
                }
#endif
                if (m_UnityText != null) {
                    m_UnityText.enabled = value;
                }
            }

        }

        public Color color
        {
            get {
#if TEXTMESH_PRO_PRESENT
                return m_TextMeshProText?.color ?? m_UnityText?.color ?? Color.black;
#else
                return m_UnityText?.color ?? Color.black;
#endif
            }
            set => SetColor(value);
        }

        /// <summary>
        /// Set the text of the components.
        /// </summary>
        /// <param name="newText">The new text.</param>
        public void SetText(string newText)
        {
#if TEXTMESH_PRO_PRESENT
            if (m_TextMeshProText != null) { m_TextMeshProText.text = newText; return; }
#endif
            if (m_UnityText != null) { m_UnityText.text = newText; return; }
        }

        /// <summary>
        /// Set the text of the components.
        /// </summary>
        /// <param name="newColor">The new text.</param>
        public void SetColor(Color newColor)
        {
#if TEXTMESH_PRO_PRESENT
            if (m_TextMeshProText != null) { m_TextMeshProText.color = newColor; return; }
#endif
            if (m_UnityText != null) { m_UnityText.color = newColor; return; }
        }

        public float fontSize
        {
            get
            {
#if TEXTMESH_PRO_PRESENT
                return m_TextMeshProText?.fontSize ?? m_UnityText?.fontSize ?? 0;
#else
                return m_UnityText?.fontSize ?? 0;
#endif
            }
            set
            {
#if TEXTMESH_PRO_PRESENT
                if (m_TextMeshProText != null) { m_TextMeshProText.fontSize = value; return; }
#endif
                if (m_UnityText != null) { m_UnityText.fontSize = (int)value; }
            }
        }

        public TextAlignment alignment
        {
            get
            {
#if TEXTMESH_PRO_PRESENT
                if (m_TextMeshProText != null)
                {
                    switch (m_TextMeshProText.alignment)
                    {
                        case TMPro.TextAlignmentOptions.TopLeft:
                            return TextAlignment.TopLeft;
                        case TMPro.TextAlignmentOptions.Top:
                            return TextAlignment.TopCenter;
                        case TMPro.TextAlignmentOptions.TopRight:
                            return TextAlignment.TopRight;
                        case TMPro.TextAlignmentOptions.Left:
                            return TextAlignment.MiddleLeft;
                        case TMPro.TextAlignmentOptions.Midline:
                            return TextAlignment.MiddleCenter;
                        case TMPro.TextAlignmentOptions.Right:
                            return TextAlignment.MiddleRight;
                        case TMPro.TextAlignmentOptions.BottomLeft:
                            return TextAlignment.BottomLeft;
                        case TMPro.TextAlignmentOptions.Bottom:
                            return TextAlignment.BottomCenter;
                        case TMPro.TextAlignmentOptions.BottomRight:
                            return TextAlignment.BottomRight;
                    }
                }
#endif
                if (m_UnityText != null)
                {
                    switch (m_UnityText.alignment)
                    {
                        case TextAnchor.UpperLeft:
                            return TextAlignment.TopLeft;
                        case TextAnchor.UpperCenter:
                            return TextAlignment.TopCenter;
                        case TextAnchor.UpperRight:
                            return TextAlignment.TopRight;
                        case TextAnchor.MiddleLeft:
                            return TextAlignment.MiddleLeft;
                        case TextAnchor.MiddleCenter:
                            return TextAlignment.MiddleCenter;
                        case TextAnchor.MiddleRight:
                            return TextAlignment.MiddleRight;
                        case TextAnchor.LowerLeft:
                            return TextAlignment.BottomLeft;
                        case TextAnchor.LowerCenter:
                            return TextAlignment.BottomCenter;
                        case TextAnchor.LowerRight:
                            return TextAlignment.BottomRight;
                    }
                }
                return TextAlignment.MiddleCenter;
            }

            set
            {
#if TEXTMESH_PRO_PRESENT
                if (m_TextMeshProText != null)
                {
                    switch (value)
                    {
                        case TextAlignment.TopLeft:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.TopLeft;
                            break;
                        case TextAlignment.TopCenter:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.Top;
                            break;
                        case TextAlignment.TopRight:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.TopRight;
                            break;
                        case TextAlignment.MiddleLeft:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.Left;
                            break;
                        case TextAlignment.MiddleCenter:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.Midline;
                            break;
                        case TextAlignment.MiddleRight:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.Right;
                            break;
                        case TextAlignment.BottomLeft:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.BottomLeft;
                            break;
                        case TextAlignment.BottomCenter:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.Bottom;
                            break;
                        case TextAlignment.BottomRight:
                            m_TextMeshProText.alignment = TMPro.TextAlignmentOptions.BottomRight;
                            break;
                    }

                    return;
                }
#endif
                if (m_UnityText)
                {
                    switch (value)
                    {
                        case TextAlignment.TopLeft:
                            m_UnityText.alignment = TextAnchor.UpperLeft;
                            break;
                        case TextAlignment.TopCenter:
                            m_UnityText.alignment = TextAnchor.UpperCenter;
                            break;
                        case TextAlignment.TopRight:
                            m_UnityText.alignment = TextAnchor.UpperRight;
                            break;
                        case TextAlignment.MiddleLeft:
                            m_UnityText.alignment = TextAnchor.MiddleLeft;
                            break;
                        case TextAlignment.MiddleCenter:
                            m_UnityText.alignment = TextAnchor.MiddleCenter;
                            break;
                        case TextAlignment.MiddleRight:
                            m_UnityText.alignment = TextAnchor.MiddleRight;
                            break;
                        case TextAlignment.BottomLeft:
                            m_UnityText.alignment = TextAnchor.LowerLeft;
                            break;
                        case TextAlignment.BottomCenter:
                            m_UnityText.alignment = TextAnchor.LowerCenter;
                            break;
                        case TextAlignment.BottomRight:
                            m_UnityText.alignment = TextAnchor.LowerRight;
                            break;
                    }
                }
            }
        }
    }
}