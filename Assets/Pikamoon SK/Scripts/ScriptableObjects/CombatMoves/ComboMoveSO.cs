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

        Weapon
    }


    public enum CombatMoveType
    {
        Horizontal,
        Vertical,
        Counter,
        Special,
    }


    [CreateAssetMenu(fileName = "New Move", menuName = "Pikamoon/Combat/Create new Move")]
    public class ComboMoveSO : ScriptableObject
    {
        public CombatMoveEffectPoint[] combatMoveEffectPoint;
        public CombatMoveType combatMoveType;
        [Space]
        public float Damage;
    }
}