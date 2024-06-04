using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PikamoonController : MonoBehaviour
{
    public bool isAIPikamood = false;
    private List<GameObject> opponents; // List to hold the opponent characters
    private GameObject nearestOpponent; // The nearest opponent character
    private float speed = 3.0f; // Movement speed
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
        Initialize();
;    }

    public void Initialize()
    {
        if(isAIPikamood)
            opponents = PlacementSystem.Instance.aIPikas;
        else
            opponents = PlacementSystem.Instance.playerPika;
        FindAndSetNearestOpponent();
    }

    private void Update()
    {
        if(isBattleStarted)
        {
            if (nearestOpponent == null)
            {
                // Find a new nearest opponent if the current one is destroyed
                FindAndSetNearestOpponent();
                if (nearestOpponent == null)
                {
                    return; // No opponents to follow
                }
            }

            // Move towards the nearest opponent
            MoveTowardsOpponent(nearestOpponent);
        }
       
    }

    private void FindAndSetNearestOpponent()
    {
        nearestOpponent = FindNearestOpponent();
    }

    private GameObject FindNearestOpponent()
    {
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject opponent in opponents)
        {
            if (opponent != null)
            {
                float distance = Vector3.Distance(transform.position, opponent.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = opponent;
                }
            }
        }

        return nearest;
    }

    private void MoveTowardsOpponent(GameObject opponent)
    {
        Vector3 direction = (opponent.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
