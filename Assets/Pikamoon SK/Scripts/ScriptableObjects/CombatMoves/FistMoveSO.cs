using UnityEngine;


namespace Pikamoon.Controller
{
    public enum CombatMoveEffectPoint
    {
        Head,

        LeftHand,
        RightHand,

        leftFoot,
        RightFoot,

        LeftHandWeapon,
        RightHandWeapon,

        RangedWeapon
    }


    public enum CombatMoveType
    {
        Horizontal,
        Vertical,
        Counter,
        Special,
    }

    [CreateAssetMenu(fileName = "New Fist Move", menuName = "Pikamoon/Combat/Combo Move/Create New Fist Move")]
    public class FistMoveSO : ComboMoveSO
    {
        public CombatMoveEffectPoint[] combatMoveEffectPoint;
        public CombatMoveType combatMoveType;
    }
}