using UnityEngine;

namespace Pikamoon.Controller
{

    [System.Serializable]
    public struct MeleeCombo
    {
        public CombatMoveType combatMoveType;

        public ComboMoveSO[] moves;
    }
    public enum WeaponType
    {
        None,
        Melee,
        Ranged,
        Throwable
    }



    [CreateAssetMenu(fileName = "new Melee Weapon", menuName = "Pikamoon/Item/Weapon/Create new Melee weapon")]
    public class MeleeWeaponDataSO : WeaponDataSO
    {
        [Header("Core Data")]
        [Space]
        [Space]
        public int MaxMovesInCombo;
        [Space]
        public MeleeCombo[] combos;
    }

}