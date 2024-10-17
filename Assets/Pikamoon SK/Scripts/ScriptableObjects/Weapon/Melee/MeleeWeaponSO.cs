using UnityEngine;


[System.Serializable]
public struct MeleeCombo
{
    public CombatMoveType combatMoveType;

    public ComboMoveSO[] moves;
}
public enum WeaponType
{
    Melee,
    Ranged
}



[CreateAssetMenu(fileName = "new Melee Weapon", menuName = "Pikamoon/Weapon/Create new Melee weapon")]
public class MeleeWeaponSO : WeaponSO
{
    [Space]
    public int MaxMovesInCombo;
    [Space]
    public AnimatorOverrideController AnimOC;
    [Space]
    public MeleeCombo[] combos;

}
