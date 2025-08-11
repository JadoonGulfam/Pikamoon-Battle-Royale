using UnityEngine;
namespace Pikamoon.Controller
{

    public class NewHoldingPointsAddition : MonoBehaviour
    {
        public PlayerController ReferencedController;

        public PlayerController NewController;

        [ContextMenu("Assign Resting Point")]
        public void AssignRestingPoint()
        {
            for (int i = 0; i < ReferencedController.restingPoints.Length;i++)
            {
                if (ReferencedController.restingPoints[i].Point != null)
                {
                    Transform point = FindDeepChildByPartialName(NewController.transform, ReferencedController.restingPoints[i].Point.name);
                    if (point == null)
                    {
                        Transform Referencedparent = FindDeepChildByPartialName(ReferencedController.transform, ReferencedController.restingPoints[i].Point.parent.name);

                        Transform newParent = FindDeepChildByPartialName(NewController.transform, Referencedparent.name);
                        GameObject obj = new GameObject();

                        obj.transform.parent = newParent;
                        obj.name                    = ReferencedController.restingPoints[i].Point.name;
                        obj.transform.localPosition = ReferencedController.restingPoints[i].Point.transform.localPosition;
                        obj.transform.localRotation = ReferencedController.restingPoints[i].Point.transform.localRotation;
                        obj.transform.localScale    = ReferencedController.restingPoints[i].Point.transform.localScale;


                        NewController.restingPoints[i].Point = obj.transform;
                        NewController.restingPoints[i].Type = ReferencedController.restingPoints[i].Type;
                    }
                }
            }
        }


        Transform FindDeepChildByPartialName(Transform parent, string partialName)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Contains(partialName))
                    return child;

                Transform result = FindDeepChildByPartialName(child, partialName);
                if (result != null)
                    return result;
            }
            return null;
        }


    }

}