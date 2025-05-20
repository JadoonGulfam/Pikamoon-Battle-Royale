using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDatabase", menuName = "Database/ObjectDatabase")]
public class ObjectDatabaseSO : ScriptableObject
{
    [Header("Pikamoon Data")]
    public List<ObjectData> objectData;

    /// <summary>
    /// Retrieves Pikamoon data by its ID.
    /// </summary>
    /// <param name="id">The ID of the Pikamoon.</param>
    /// <returns>The ObjectData associated with the given ID, or null if not found.</returns>
    public ObjectData GetPikamoonDataByID(int id)
    {
        try
        {
            return objectData.Find(data => data.ID == id);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving Pikamoon data by ID {id}: {ex.Message}");
            return null;
        }
    }
}

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
    public float ManaRegenRate;
}
