using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PikamoonController : MonoBehaviour
{
    public bool isAIPikamood = false;
    private List<GameObject> opponents;
    private GameObject nearestOpponent; 
    private float speed = 0.5f;
    private bool isBattleStarted;

    private void OnEnable()
    {
        PlacementSystem.OnPlacementComplete += StartFindingOpponent;
    }

    private void OnDisable()
    {
        PlacementSystem.OnPlacementComplete -= StartFindingOpponent;
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

            MoveTowardsOpponent();
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

    private void MoveTowardsOpponent()
    {
        if (nearestOpponent == null) return;

        Vector3 opponentPosition = nearestOpponent.transform.position;
        float distance = Vector3.Distance(transform.position, opponentPosition);

        if (distance < 2f)
        {
            return;
        }

        Debug.Log($"{gameObject.name} {transform.position}  vs  {nearestOpponent.name} {opponentPosition}");

        Vector3 direction = (opponentPosition - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
        }
    }
}
