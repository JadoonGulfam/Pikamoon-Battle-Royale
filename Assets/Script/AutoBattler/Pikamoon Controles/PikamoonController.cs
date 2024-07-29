using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PikamoonMovement))]
[RequireComponent(typeof(PikamoonCombat))]
public class PikamoonController : MonoBehaviour
{
    [Header("General Settings")]
    public bool isAIPikamood = false;
    public int pikamoonID; // Added this field to store the Pikamoon ID
    private List<GameObject> opponents;
    private GameObject nearestOpponent;
    private bool isBattleStarted;

    [Header("Components")]
    private PikamoonMovement movement;
    private PikamoonCombat combat;

    [SerializeField]
    private ObjectDatabaseSO objectDatabase; // Serialized field to assign ObjectDatabaseSO

    private void OnEnable()
    {
        PlacementSystem.OnPlacementComplete += StartFindingOpponent;
    }

    private void OnDisable()
    {
        PlacementSystem.OnPlacementComplete -= StartFindingOpponent;
    }

    private void Awake()
    {
        // Initialize components
        movement = GetComponent<PikamoonMovement>();
        combat = GetComponent<PikamoonCombat>();

        // Initialize attributes
        InitializeAttributes();
    }
    private void Start()
    {
        PrintPikamoonAttributes();
    }
    private void InitializeAttributes()
    {
        if (objectDatabase == null)
        {
            Debug.LogError("ObjectDatabase is not assigned in PikamoonController");
            return;
        }

        var pikamoonData = objectDatabase.objectData.Find(data => data.ID == pikamoonID);
        if (pikamoonData == null)
        {
            Debug.LogError($"No Pikamoon data found with ID: {pikamoonID}");
            return;
        }
        movement.Initialize(pikamoonData.MoveSpeed);
        // Here you can initialize your Pikamoon attributes with pikamoonData
        // For example:
       // Debug.Log($"Initializing Pikamoon {pikamoonData.DisplayName} with ID {pikamoonData.ID}");
        // Set attributes like HP, Attack, etc.
        // hp = pikamoonData.Hp;
        // attack = pikamoonData.Attack;
        // And so on...
    }

    public void PrintPikamoonAttributes()
    {
        if (objectDatabase == null)
        {
            Debug.LogError("ObjectDatabase is not assigned in PikamoonController");
            return;
        }

        var pikamoonData = objectDatabase.objectData.Find(data => data.ID == pikamoonID);
        if (pikamoonData == null)
        {
            Debug.LogError($"No Pikamoon data found with ID: {pikamoonID}");
            return;
        }
        

        // Print Pikamoon attributes
        //Debug.Log($"Pikamoon Attributes for ID {pikamoonID}:");
        //Debug.Log($"DisplayName: {pikamoonData.DisplayName}");
        //Debug.Log($"Rarity: {pikamoonData.Rarity}");
        //Debug.Log($"Level: {pikamoonData.Level}");
        //Debug.Log($"ElementalClass: {pikamoonData.ElementalClass}");
        //Debug.Log($"CombatClass: {pikamoonData.CombatClass}");
        //Debug.Log($"Attack: {pikamoonData.Attack}");
        //Debug.Log($"Magic: {pikamoonData.Magic}");
        //Debug.Log($"Hp: {pikamoonData.Hp}");
        //Debug.Log($"PhysicalDefense: {pikamoonData.PhysicalDefense}");
        //Debug.Log($"MagicalDefense: {pikamoonData.MagicalDefense}");
        //Debug.Log($"Evasion: {pikamoonData.Evasion}");
        //Debug.Log($"MoveSpeed: {pikamoonData.MoveSpeed}");
        //Debug.Log($"AttackInterval: {pikamoonData.AttackInterval}");
        //Debug.Log($"AttackRange: {pikamoonData.AttackRange}");
        //Debug.Log($"MaxMana: {pikamoonData.MaxMana}");
        //Debug.Log($"BaseManaGeneration: {pikamoonData.BaseManaGeneration}");
        //Debug.Log($"ManaGainAttackMultiplier: {pikamoonData.ManaGainAttackMultiplier}");
        //Debug.Log($"ManaGainDamageMultiplier: {pikamoonData.ManaGainDamageMultiplier}");
        //Debug.Log($"CriticalChance: {pikamoonData.CriticalChance}");
        //Debug.Log($"SpecialAbility: {pikamoonData.SpecialAbility}");
        //Debug.Log($"Description: {pikamoonData.Description}");
    }

    private void StartFindingOpponent()
    {
        Debug.Log("StartFindingOpponent event raised");
        Initialize();
        isBattleStarted = true;
    }

    public void Initialize()
    {
        opponents = isAIPikamood ? PlacementSystem.Instance.playerPika : PlacementSystem.Instance.aIPikas;
        Debug.Log($"{gameObject.name} total opponents {opponents.Count}");
        FindAndSetNearestOpponent();
    }

    private void Update()
    {
        if (isBattleStarted)
        {
            if (nearestOpponent == null)
            {
                Debug.Log("Nearest opponent destroyed");
                FindAndSetNearestOpponent();
                if (nearestOpponent == null)
                {
                    return; // No opponents to follow
                }
            }

            if (movement.MoveTowardsOpponent(nearestOpponent))
            {
                combat.StartCombat(nearestOpponent);
            }
        }
    }

    private void FindAndSetNearestOpponent()
    {

        nearestOpponent = FindNearestOpponent();
        if (nearestOpponent != null)
        {
            Debug.Log($"{gameObject.name} nearest opponent {nearestOpponent.name}");
        }
    }

    private GameObject FindNearestOpponent()
    {
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject opponent in opponents)
        {
            if (opponent != null && opponent != gameObject)
            {
                float distance = Vector3.Distance(currentPosition, opponent.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = opponent;
                }
            }
        }

        return nearest?.transform.GetChild(0).gameObject;
    }
}
