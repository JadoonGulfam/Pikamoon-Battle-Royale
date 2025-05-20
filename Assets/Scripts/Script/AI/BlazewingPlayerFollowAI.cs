using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazewingPlayerFollowAI : NetworkBehaviour
{
    public Transform player;
    public float followDistance = 5f;
    public float followSpeed = 2f;
    public float heightOffset = 2f;
    public float randomRange = 1f;
    public bool shouldLand = false; // Whether the bird should land
    public float RotationOffSet;
    private Vector3 targetPosition;
    private Vector3 randomOffset;

    void Start()
    {
        if (!HasStateAuthority)
        {
            GetComponent<BlazewingPlayerFollowAI>().enabled = false;
            return; 
        }

            randomOffset = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));
    }

    void Update()
    {
        if (shouldLand)
        {
            targetPosition = player.position + Vector3.up * heightOffset; // Bird lands at player's position
        }
        else
        {
            targetPosition = player.position + player.forward * followDistance + randomOffset;
            targetPosition.y += heightOffset;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * followSpeed);
    }

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetPosition);
        }
    }
}
