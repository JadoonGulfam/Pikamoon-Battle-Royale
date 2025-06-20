using UnityEngine;
namespace Pikamoon.Controller
{

    public class CharacterCreatorEditor : MonoBehaviour
    {
        [Header("Reference Character")]
        public PlayerController referenceCharacter;
        [SerializeField] Transform R_Root;
        [SerializeField] Transform R_Geometry;
        [SerializeField] Transform R_Graphics;
        [SerializeField] Transform R_DeformationSystem;




        [Header("New Preset Character")]
        public AvatarController presetController;
        [SerializeField] Transform Prst_Root;
        [SerializeField] Transform Prst_Geometry;
        [Space]
        [SerializeField] Transform Preset_Chest;
        [SerializeField] Transform Preset_Wrist_L;
        [SerializeField] Transform Preset_Wrist_R;
        [SerializeField] Transform Preset_Head_M;
        [SerializeField] Transform Preset_Knee_L;
        [SerializeField] Transform Preset_Knee_R;



        Transform LHHP;
        Transform RHHP;
                      
                      
        Transform LBRP;
        Transform RBRP;

        Transform Head_HP;
        Transform LeftHand_HP;
        Transform RightHand_HP;
        Transform LeftFoot_HP;
        Transform RIghtFoot_HP;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }


        public void EmptyReferences()
        {
            R_Root = null;
            R_Geometry = null;
            R_DeformationSystem = null;

            Prst_Root = null;
            Prst_Geometry = null;
        }


        [ContextMenu("0_Empty References")]
        public void EmptyTransforms()
        {

        }


        [ContextMenu("1_Get References")]
        public void GetReferences()
        {
            R_Root                  = FindDeepChildByPartialName(referenceCharacter.transform,"Root");
            R_Geometry              = FindDeepChildByPartialName(referenceCharacter.transform, "Geometry");
            R_Graphics              = FindDeepChildByPartialName(referenceCharacter.transform, "Graphic");
            R_DeformationSystem     = FindDeepChildByPartialName(referenceCharacter.transform, "DeformationSystem");


            Prst_Root          = FindDeepChildByPartialName(presetController.transform, "Root");
            Prst_Geometry      = FindDeepChildByPartialName(presetController.transform, "Geometry");




            LHHP = referenceCharacter.holdingPoints[0].Point;
            RHHP = referenceCharacter.holdingPoints[1].Point;

            LBRP = referenceCharacter.restingPoints[6].Point;
            RBRP = referenceCharacter.restingPoints[7].Point;

            Head_HP       = FindDeepChildByPartialName(referenceCharacter.transform, "Head_Hit");
            LeftHand_HP   = FindDeepChildByPartialName(referenceCharacter.transform, "Hand_L_Hit");
            RightHand_HP  = FindDeepChildByPartialName(referenceCharacter.transform, "Hand_R_Hit");
            LeftFoot_HP   = FindDeepChildByPartialName(referenceCharacter.transform, "Foot_L_Hit");
            RIghtFoot_HP  = FindDeepChildByPartialName(referenceCharacter.transform, "Foot_L_Hit");


            Preset_Chest =  FindDeepChildByPartialName  (Prst_Root.transform, "Chest_");
            Preset_Wrist_L = FindDeepChildByPartialName(Prst_Root.transform, "Wrist_L");
            Preset_Wrist_R = FindDeepChildByPartialName(Prst_Root.transform, "Wrist_R");
            Preset_Head_M = FindDeepChildByPartialName(Prst_Root.transform, "Head_M");
            Preset_Knee_L = FindDeepChildByPartialName(Prst_Root.transform, "Knee_L");
            Preset_Knee_R = FindDeepChildByPartialName(Prst_Root.transform, "Knee_R");

        }

        [ContextMenu("2_Move All Transforms")]
        public void MoveAllTransforms()
        {
            Prst_Root.parent = R_DeformationSystem;
            Prst_Geometry.parent = R_Graphics;

            MoveTransform(LHHP, Preset_Chest);
        }



        void MoveTransform(Transform Item, Transform Parent)
        {
            Vector3 Position;
            Quaternion Rotation;
            Vector3 Scale;

            Position = Item.position;
            Rotation = Item.rotation;
            Scale = Item.localScale;


            Item.parent = Parent;

            Item.localPosition = Position;
            Item.localRotation = Rotation;
            Item.localScale = Scale;

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