using UnityEngine;

public class NeckRigCorrecterEditor : MonoBehaviour
{
    [Header("Reference Character ")]
    public Transform RefCharacter;


    [Header("Preset character")]
    public Transform PresetCharacter;

    [ContextMenu("Perform Addition Of Necks Transforms")]
    public void PerformAdditoinOfNecksTransforms()
    {

        Transform Neck = FindDeepChildByPartialName(RefCharacter, "Neck_M");

        Transform PNeck = FindDeepChildByPartialName(PresetCharacter, "Neck_M");

        Transform PHead;

        if(PNeck.GetChild(0).name != Neck.GetChild(0).name)
        {
            PHead = PNeck.GetChild(0);

            GameObject ob = new GameObject();

            ob.transform.parent = PNeck;

            ob.transform.localPosition = Neck.GetChild(0).transform.localPosition;
            ob.transform.localRotation = Neck.GetChild(0).transform.localRotation;
            ob.transform.localScale = Neck.GetChild(0).transform.localScale;


            ob.transform.name = Neck.GetChild(0).name;


            GameObject ob1 = new GameObject();

            ob1.transform.parent = ob.transform;

            ob1.transform.localPosition = Neck.GetChild(0).GetChild(0).transform.localPosition;
            ob1.transform.localRotation = Neck.GetChild(0).GetChild(0).transform.localRotation;
            ob1.transform.localScale    = Neck.GetChild(0).GetChild(0).transform.localScale;


            ob1.transform.name = Neck.GetChild(0).GetChild(0).name;

            PHead.parent = ob1.transform;

            PHead.transform.localPosition = Neck.GetChild(0).GetChild(0).GetChild(0).transform.localPosition;
            PHead.transform.localRotation = Neck.GetChild(0).GetChild(0).GetChild(0).transform.localRotation;
            PHead.transform.localScale    = Neck.GetChild(0).GetChild(0).GetChild(0).transform.localScale;


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
