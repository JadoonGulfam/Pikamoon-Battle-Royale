using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class PikamoonRoaming : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator; // Reference to the Animator
    private bool isRoaming = false;

    // New variables for idle behavior
    public float idleTime = 2f; // Time to stay idle before moving again
    private float idleTimer;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Initialize the Animator
        navMeshAgent.enabled = true;
        idleTimer = idleTime; // Initialize the idle timer
    }

    private void Update()
    {
        if (isRoaming)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (idleTimer <= 0f)
                {
                    GetRandomDestination(); // Get a new random destination
                }
                else
                {
                    idleTimer -= Time.deltaTime; // Decrease the idle timer
                    animator.SetFloat("Move", 0); // Set walk animation to idle
                }
            }
            else
            {
                // Moving to the destination
                animator.SetFloat("Move", navMeshAgent.velocity.magnitude > 0.1f ? 1 : 0); // Enable walk animation
            }
        }
    }

    public void EnableRoaming()
    {
        isRoaming = true;
        GetRandomDestination(); // Get initial roaming destination
    }

    public void DisableRoaming()
    {
        isRoaming = false;
        navMeshAgent.ResetPath(); // Reset the NavMeshAgent path when stopped
        animator.SetFloat("Move", 0); // Stop walk animation
        idleTimer = idleTime; // Reset idle timer
    }

    private void GetRandomDestination()
    {
        // Set the idle timer for a defined period before moving again
        idleTimer = idleTime;

        Vector3 randomDirection = Random.insideUnitSphere * 5f; // Adjust roaming radius
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas);
        navMeshAgent.SetDestination(hit.position);
    }
}
