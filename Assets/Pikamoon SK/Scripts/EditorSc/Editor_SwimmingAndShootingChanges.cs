using Pikamoon.Controller;
using UnityEngine;
using UnityEngine.ProBuilder;



public class Editor_SwimmingAndShootingChanges : MonoBehaviour
{
    [Header("Reference Character")]
    [Space]
    public PlayerController ReferenceCharacters;
    [SerializeField] Swimming R_Swimming;
    [SerializeField] Airbourne R_Air;
    [SerializeField] Shooting R_Shooting;
    [SerializeField] Transform R_ArrowHolder;


    [Header("New Character")]
    [Space]
    public PlayerController NewCharacters; 
    [SerializeField] Swimming N_Swimming;
    [SerializeField] Airbourne N_Air;
    [SerializeField] Shooting N_Shooting;
    [SerializeField] Transform N_ArrowHolder;

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

    [ContextMenu("Change All Values")]
    public void GetReferences()
    {
        //for (int i = 0; i < ReferenceCharacters.states.Length; i++)
        //{
        //    if (ReferenceCharacters.states[i] is Swimming)
        //    {
        //        R_Swimming = ReferenceCharacters.states[i] as Swimming;
        //    }

        //    if (ReferenceCharacters.states[i] is Airbourne)
        //    {
        //        R_Air = ReferenceCharacters.states[i] as Airbourne;
        //    }

        //    if (ReferenceCharacters.states[i] is Shooting)
        //    {
        //        R_Shooting = ReferenceCharacters.states[i] as Shooting;
        //    }

        //}

        //R_ArrowHolder = ReferenceCharacters.holdingPoints[1].Point;


        //for (int i = 0; i < NewCharacters.states.Length; i++)
        //{

        //    if (NewCharacters.states[i].GetStateType() == StateType.Swimming)
        //    {
        //        N_Swimming = NewCharacters.states[i].GetComponent<Swimming>();

        //        N_Swimming.SwimmingMaxLedgeClimbHeight = R_Swimming.SwimmingMaxLedgeClimbHeight;
        //        N_Swimming.LedgeDetectDistance = R_Swimming.LedgeDetectDistance;
        //        N_Swimming.LedgeCheckRadius = R_Swimming.LedgeCheckRadius;
        //        N_Swimming.SurfaceOffset = R_Swimming.SurfaceOffset;

        //        N_Swimming.ClimbSphere1 = R_Swimming.ClimbSphere1;
        //        N_Swimming.ClimbSphere2 = R_Swimming.ClimbSphere2;
        //        N_Swimming.ClimbSphere3 = R_Swimming.ClimbSphere3;
        //    }

        //    if (NewCharacters.states[i].GetStateType() == StateType.Air)
        //    {
        //        N_Air = NewCharacters.states[i].GetComponent<Airbourne>();

        //        N_Air.CoyoteJumpTime = R_Air.CoyoteJumpTime;
        //        N_Air.ApexGravityMultiplier = R_Air.ApexGravityMultiplier;
        //        N_Air.ApexHungTime = R_Air.ApexHungTime;
        //    }

        //    if (NewCharacters.states[i].GetStateType() == StateType.Shooting)
        //    {
        //        N_Shooting = NewCharacters.states[i].GetComponent<Shooting>();


        //        N_Shooting.isChargingAttack = R_Shooting.isChargingAttack;
        //        N_Shooting.ChargedScalingCurve = R_Shooting.ChargedScalingCurve;
        //        N_Shooting.chargeAttackDamageMultiplier = R_Shooting.chargeAttackDamageMultiplier;
        //        N_Shooting.SpeedOfCharge = R_Shooting.SpeedOfCharge;
        //        N_Shooting.PerfectRange = R_Shooting.PerfectRange;

        //        N_Shooting.ArrowHoldingPoint = N_ArrowHolder;
        //    }

        //}

        N_ArrowHolder = NewCharacters.holdingPoints[1].Point;

    }




}
