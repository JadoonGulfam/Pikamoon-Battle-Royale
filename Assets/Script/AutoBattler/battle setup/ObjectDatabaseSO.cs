using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDatabase", menuName = "Database/ObjectDatabase")]
public class ObjectDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectData;
}      
//

[Serializable]
public class ObjectData
{
    public int ID;
    public string DisplayName;
    public string Rarity;
    public int Level;
    public string ElementalClass;
    public string CombatClass;
    public int Attack;
    public int Magic;
    public int Hp;
    public int PhysicalDefense;
    public int MagicalDefense;
    public int Evasion;
    public float MoveSpeed;
    public float AttackInterval;
    public int AttackRange;
    public int MaxMana;
    public int BaseManaGeneration;
    public int ManaGainAttackMultiplier;
    public int ManaGainDamageMultiplier;
    public int CriticalChance;
    public string SpecialAbility;
    public string Description;
}
