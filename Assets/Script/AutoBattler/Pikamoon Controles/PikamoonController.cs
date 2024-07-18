using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PikamoonMovement))]
[RequireComponent(typeof(PikamoonCombat))]
[RequireComponent(typeof(PikamoonAnimation))]
public class PikamoonController : MonoBehaviour
{
    [Header("General Settings")]
    public bool isAIPikamood = false;
    private List<GameObject> opponents;
    private GameObject nearestOpponent;
    private bool isBattleStarted;

    [Header("Components")]
    private PikamoonMovement movement;
    private PikamoonCombat combat;

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
