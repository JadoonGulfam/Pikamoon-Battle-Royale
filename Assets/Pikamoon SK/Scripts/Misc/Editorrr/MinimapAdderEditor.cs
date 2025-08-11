using Unity.VisualScripting;
using UnityEngine;

public class MinimapAdderEditor : MonoBehaviour
{
    public Transform AllMinimapPointers;
    public Transform Parent;

    [ContextMenu("Add Minimap In All Childs")]
    public void AddMinimapInAllChilds()
    {

        for (int i = 0; i < Parent.childCount; i++)
        {
            Transform minimapPointer = AllMinimapPointers.GetChild(0);
            minimapPointer.transform.name = "Minimap Pointer";

            MiniMapHandler Player =  Parent.GetChild(i).AddComponent<MiniMapHandler>();

            minimapPointer.parent = Player.transform;

            minimapPointer.localPosition = Vector3.zero;
            minimapPointer.localRotation = Quaternion.identity;
            minimapPointer.localScale = Vector3.one;

            Player.MyMMPointer = minimapPointer;
            Player.CamOffsetFromPlayer = new Vector3(0, 1000, 0);
        }
    }

}
