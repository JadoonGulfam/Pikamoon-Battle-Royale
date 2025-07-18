using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.LowLevelPhysics;
namespace Pikamoon.Controller
{

    public class CharacterCreatorEditor : MonoBehaviour
    {
        [Header("Reference Character")]
        public PlayerController referenceCharacter;
        [Space]
        [SerializeField] MultiAimConstraint AimerChest;
        [SerializeField] MultiAimConstraint AimerArm;
        [SerializeField] MultiAimConstraint Aimer;
        [Space]
        [SerializeField] Transform R_Root;
        [SerializeField] Transform R_Geometry;
        [SerializeField] Transform R_Graphics;
        [SerializeField] Transform R_DeformationSystem;




        [Header("New Preset Character")]
        public Transform presetController;
        [SerializeField] Transform Prst_Root;
        [SerializeField] Transform Prst_Geometry;
        [Space]
        [SerializeField] Transform Preset_Chest;
        [SerializeField] Transform Preset_Wrist_L;
        [SerializeField] Transform Preset_Wrist_R;
        [SerializeField] Transform Preset_Head_M;
        [SerializeField] Transform Preset_Knee_L;
        [SerializeField] Transform Preset_Knee_R;
        [SerializeField] Transform Preset_Elbow_L;
        [SerializeField] Transform Preset_Elbow_R;
        [Space]
        [SerializeField] Transform Preset_Spine1;
        [SerializeField] Transform Preset_Spine2;
        [SerializeField] Transform Preset_Shoulder_L;



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
            Head_HP         = null;
            LeftHand_HP     = null;
            RightHand_HP    = null;
            LeftFoot_HP     = null;
            RIghtFoot_HP    = null;


            Preset_Head_M =    null;
            Preset_Chest =     null;
            Preset_Wrist_L =   null;
            Preset_Wrist_R =   null;
            Preset_Knee_L =    null;
            Preset_Knee_R =    null;
            Preset_Elbow_L =   null;
            Preset_Elbow_R = null;


            R_Root               =null;
            R_Geometry          = null;
            R_Graphics          = null;
            R_DeformationSystem = null;


            Prst_Root            =null;
            Prst_Geometry = null;

            AimerChest = null;
            AimerArm = null;
            Aimer = null;


            Preset_Spine1 = null;
            Preset_Spine2 = null;
            Preset_Shoulder_L = null;
        }


        [ContextMenu("1_Get References")]
        public void GetReferences()
        {
            R_Root                  = FindDeepChildByPartialName(referenceCharacter.transform,"Root");
            R_Geometry              = FindDeepChildByPartialName(referenceCharacter.transform, "Geometry");
            R_Graphics              = FindDeepChildByPartialName(referenceCharacter.transform, "Graphic");
            R_DeformationSystem     = FindDeepChildByPartialName(referenceCharacter.transform, "DeformationSystem");


            Prst_Root               = FindDeepChildByPartialName(presetController.transform, "Root");
            Prst_Geometry           = FindDeepChildByPartialName(presetController.transform, "Geometry");

            AimerChest  = FindDeepChildByPartialName    (referenceCharacter.transform, "Aimer Chest") .GetComponent<MultiAimConstraint>();
            AimerArm    = FindDeepChildByPartialName    (referenceCharacter.transform, "Aimer Arm")   .GetComponent<MultiAimConstraint>();
            Aimer       = FindDeepChildByPartialName    (referenceCharacter.transform, "AimerT")       .GetComponent<MultiAimConstraint>();


            LHHP = referenceCharacter.holdingPoints[0].Point;
            RHHP = referenceCharacter.holdingPoints[1].Point;

            LBRP = referenceCharacter.restingPoints[6].Point;
            RBRP = referenceCharacter.restingPoints[7].Point;

            Head_HP       = FindDeepChildByPartialName(referenceCharacter.transform, "Head_Hit");
            LeftHand_HP   = FindDeepChildByPartialName(referenceCharacter.transform, "Hand_L_Hit");
            RightHand_HP  = FindDeepChildByPartialName(referenceCharacter.transform, "Hand_R_Hit");
            LeftFoot_HP   = FindDeepChildByPartialName(referenceCharacter.transform, "Foot_L_Hit");
            RIghtFoot_HP  = FindDeepChildByPartialName(referenceCharacter.transform, "Foot_R_Hit");


            Preset_Head_M = FindDeepChildByPartialName(Prst_Root.transform, "Head_M");
            Preset_Chest =  FindDeepChildByPartialName  (Prst_Root.transform, "Chest_");
            Preset_Wrist_L = FindDeepChildByPartialName(Prst_Root.transform, "Wrist_L");
            Preset_Wrist_R = FindDeepChildByPartialName(Prst_Root.transform, "Wrist_R");
            Preset_Knee_L = FindDeepChildByPartialName(Prst_Root.transform, "Knee_L");
            Preset_Knee_R = FindDeepChildByPartialName(Prst_Root.transform, "Knee_R");
            Preset_Elbow_L = FindDeepChildByPartialName(Prst_Root.transform, "Elbow_L");
            Preset_Elbow_R = FindDeepChildByPartialName(Prst_Root.transform, "Elbow_R");


            Preset_Spine1       = FindDeepChildByPartialName(Prst_Root.transform,     "Spine1"    );
            Preset_Spine2       = FindDeepChildByPartialName(Prst_Root.transform,     "Spine2"    );
            Preset_Shoulder_L   = FindDeepChildByPartialName(Prst_Root.transform, "Shoulder_L");

        }

        [ContextMenu("2_Move All Transforms")]
        public void MoveAllTransforms()
        {
            if(R_Root == null)
                GetReferences();


            presetController.transform.parent = referenceCharacter.transform;

            presetController.transform.localPosition = Vector3.zero;
            presetController.transform.localRotation = Quaternion.identity;
            presetController.transform.localScale = Vector3.one;

            Prst_Root.parent = R_DeformationSystem;

            Prst_Geometry.parent = R_Graphics;

            Prst_Geometry.transform.localPosition = Vector3.zero;
            Prst_Geometry.transform.localRotation = Quaternion.identity;
            Prst_Geometry.transform.localScale = Vector3.one;

            MoveTransform(LHHP, Preset_Wrist_L);
            MoveTransform(RHHP, Preset_Wrist_R);

            MoveTransform(LBRP, Preset_Chest);
            MoveTransform(RBRP, Preset_Chest);

            MoveTransform(Head_HP, Preset_Head_M);
            MoveTransform(LeftHand_HP, Preset_Elbow_L);
            MoveTransform(RightHand_HP, Preset_Elbow_R);
            MoveTransform(LeftFoot_HP, Preset_Knee_L);
            MoveTransform(RIghtFoot_HP, Preset_Knee_R);

            AimerChest.data.constrainedObject = Preset_Spine2;
            AimerArm.data.constrainedObject = Preset_Shoulder_L;
            Aimer.data.constrainedObject = Preset_Spine1;


            DestroyImmediate(R_Geometry.gameObject);
            DestroyImmediate(R_Root.gameObject);

            EmptyTransforms();
        }



        void MoveTransform(Transform Item, Transform Parent)
        {
            Vector3 Position;
            Quaternion Rotation;
            Vector3 Scale;

            Position = Item.localPosition;
            Rotation = Item.localRotation;
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