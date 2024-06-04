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
    private float speed = 0.5f; // Movement speed
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
        print("StartFindingOpponent event raised");
        Initialize();
        isBattleStarted = true;
;    }

    public void Initialize()
    {
        if(isAIPikamood)
        {
            opponents = PlacementSystem.Instance.playerPika;
            print(this.gameObject.name + " total oponents " + opponents.Count);
        }
        else
        {
            opponents = PlacementSystem.Instance.aIPikas;
            print(this.gameObject.name + " total oponents " + opponents.Count);
        }
           
        FindAndSetNearestOpponent();
    }

    private void Update()
    {
        if(isBattleStarted)
        {
            if (nearestOpponent == null)
            {
                print("nearest opponent destroyed");
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
        print(this.gameObject.name + "nearest oppoentent " + nearestOpponent.name);
    }

    private GameObject FindNearestOpponent()
    {
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject opponent in opponents)
        {
            if (opponent != null && opponent != gameObject)
            {
                float distance = Vector3.Distance(transform.position, opponent.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = opponent.transform.GetChild(0).gameObject;
                    print("nearest name " + nearest.name);
                }
            }
        }

        return nearest;
    }

    private void MoveTowardsOpponent(GameObject opponent)
    {
        if (opponent == null) return;

        float distance = Vector3.Distance(transform.position, opponent.transform.position);
        if (distance < 2f)
        {
            return;
        }
        print(this.gameObject.name + this.gameObject.transform.position + "  vs  " + opponent.name + opponent.transform.position);

        Vector3 direction = (opponent.transform.position - transform.position).normalized;
        Vector3 offset = transform.GetChild(0).position - transform.GetChild(0).position;
        Vector3 targetPosition = opponent.transform.position - offset;
        direction = (targetPosition - transform.GetChild(0).position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        // Rotate to face the opponent
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
        }
    }
}
