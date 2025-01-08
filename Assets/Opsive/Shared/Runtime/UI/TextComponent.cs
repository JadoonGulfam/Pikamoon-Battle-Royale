/// ---------------------------------------------
/// Opsive Shared
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.Shared.UI
{
    using UnityEngine;

    /// <summary>
    /// A wrapper component referencing a text component.
    /// </summary>
    public class TextComponent : MonoBehaviour
    {
        [Tooltip("The text.")]
        [SerializeField] private Text m_Text;

        public string text { get => m_Text.text; set => m_Text.text = value; }

        public Color color { get => m_Text.color; set => m_Text.color = value; }

        public TextAlignment alignment { get => m_Text.alignment; set => m_Text.alignment = value; }

        public float fontSize { get => m_Text.fontSize; set => m_Text.fontSize = value; }
    }
}