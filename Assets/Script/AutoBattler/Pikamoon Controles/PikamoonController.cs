using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(PikamoonMovement))]
[RequireComponent(typeof(PikamoonCombat))]
[RequireComponent(typeof(PikamoonHealth))]
[RequireComponent(typeof(HealthManaBar))]
public class PikamoonController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] public bool isAIPikamoon = false;
    [SerializeField] public int pikamoonID;
    [SerializeField] private ObjectDatabaseSO objectDatabase;

    [Header("State")]
    private List<GameObject> opponents;
    private GameObject nearestOpponent;
    private bool isBattleStarted;

    [Header("Components")]
    private PikamoonMovement movement;
    private PikamoonCombat combat;
    private PikamoonHealth health;

    //private void OnEnable()
    //{
    //    PlacementSystem.OnPlacementComplete += StartFindingOpponent;
    //}

    private void OnDisable()
    {
        PlacementSystem.OnPlacementComplete -= StartFindingOpponent;
    }

    private void Start()
    {
        PlacementSystem.OnPlacementComplete += StartFindingOpponent;
    }
    private void Awake()
    {
        InitializeComponents();
        InitializePikamoonAttributes();
    }

    private void InitializeComponents()
    {
        movement = GetComponent<PikamoonMovement>();
        combat = GetComponent<PikamoonCombat>();
        health = GetComponent<PikamoonHealth>();
    }

    private void InitializePikamoonAttributes()
    {
        try
        {
            if (objectDatabase == null)
                throw new ArgumentNullException(nameof(objectDatabase), "ObjectDatabase is not assigned.");

            var pikamoonData = objectDatabase.GetPikamoonDataByID(pikamoonID);
            if (pikamoonData == null)
                throw new KeyNotFoundException($"No Pikamoon data found with ID: {pikamoonID}");

            movement.Initialize(pikamoonData.MoveSpeed);
            health.Initialize(pikamoonData.Hp, pikamoonData.MaxMana);
            combat.Initialize(pikamoonData.Attack, pikamoonData.AttackRange, pikamoonData.ManaRegenRate);
        }
        catch (ArgumentNullException ex)
        {
            Debug.LogError(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            Debug.LogError(ex.Message);
        }
        catch (Exception ex) // General exception handling for unexpected errors
        {
            Debug.LogError($"An unexpected error occurred: {ex.Message}");
        }
    }


    private void StartFindingOpponent()
    {
        Debug.Log("Battle started. Finding opponents...");
        InitializeOpponents();
        isBattleStarted = true;
        
    }

    private void InitializeOpponents()
    {
        opponents = isAIPikamoon ? PlacementSystem.Instance.playerPika : PlacementSystem.Instance.aIPikas;
        FindAndSetNearestOpponent();
    }

    private void Update()
    {
        if (isBattleStarted)
        {
            if (nearestOpponent == null)
            {
                FindAndSetNearestOpponent();
                if (nearestOpponent == null)
                {
                    return;
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
            Debug.Log($"{gameObject.name} found nearest opponent: {nearestOpponent.name}");
        }
    }

    private GameObject FindNearestOpponent()
    {
        if (opponents == null || opponents.Count == 0) return null;

        GameObject nearest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject opponent in opponents)
        {
            if (opponent == null || opponent == gameObject) continue;

            float distance = Vector3.Distance(currentPosition, opponent.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = opponent;
            }
        }

        return nearest?.transform.GetChild(0).gameObject;
    }

   
}
