namespace Opsive.UltimateCharacterController.Editor.Inspectors.Items.Actions
{
    using Opsive.Shared.Editor.UIElements;
    using Opsive.UltimateCharacterController.Items.Actions.Impact;
    using UnityEditor;

    /// <summary>
    /// Draws a custom inspector for the ImpactActionObject class.
    /// </summary>
    [CustomEditor(typeof(ImpactActionObject), true)]
    public class ImpactActionObjectInspector : UIElementsInspector
    {
        protected ImpactActionObject m_ImpactActionObject;

        /// <summary>
        /// Initialize the inspector when it is first selected.
        /// </summary>
        protected override void InitializeInspector()
        {
            m_ImpactActionObject = target as ImpactActionObject;
            base.InitializeInspector();
        }
    }
}