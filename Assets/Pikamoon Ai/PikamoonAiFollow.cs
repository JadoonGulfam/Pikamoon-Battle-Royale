using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PikamoonAiFollow : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public Transform player;

    [Header("Following Settings")]
    public float followStartDistance = 10f; // When to start following
    public float followStopDistance = 5f; // When to stop following
    public float followDistance = 2f; // Distance to maintain
    public float runThreshold = 8f; // Distance at which Pikamoon runs
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    [Header("Roaming Settings")]
    public float roamRadius = 5f; // Maximum distance Pikamoon can roam
    public float roamWalkTime = 4f; // Time Pikamoon will walk before stopping
    public float roamIdleTime = 3f; // Time Pikamoon will stay idle before walking again
    private bool isRoaming = false;

    public bool isFollowing = false;
    public bool isCapture = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = walkSpeed;
    }

    void Update()
    {
        if (player == null) return;
        if (isCapture)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > followStartDistance)
            {
                // Player is far -> Start following
                StartFollowing();
            }
            else if (distanceToPlayer < followStopDistance)
            {
                // Player is close -> Start roaming
                if (!isRoaming)
                {
                    isFollowing = false;
                    isRoaming = true;
                    StartCoroutine(RoamBehavior());
                }
            }

            if (isFollowing)
            {
                FollowPlayer(distanceToPlayer);
            }
        }
    }

    void StartFollowing()
    {
        isFollowing = true;
        isRoaming = false;
        StopCoroutine(RoamBehavior()); // Stop roaming
    }

    void FollowPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > runThreshold)
        {
            // Run towards the player
            navMeshAgent.speed = runSpeed;
            animator.SetTrigger("Run");
            animator.ResetTrigger("Walk");
        }
        else if (distanceToPlayer > followDistance)
        {
            // Walk towards the player
            navMeshAgent.speed = walkSpeed;
            animator.ResetTrigger("Run");
            animator.SetTrigger("Walk");
        }
        else
        {
            // Stop moving
            navMeshAgent.speed = 0;
            animator.ResetTrigger("Run");
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Idle");
        }

        navMeshAgent.SetDestination(player.position);
    }

    private IEnumerator RoamBehavior()
    {
        while (isRoaming)
        {
            // Pick a random point within roamRadius
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(hit.position);
                animator.ResetTrigger("Idle");
                animator.SetTrigger("Walk");

                // Wait until Pikamoon reaches the target
                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
                {
                    yield return null; // Wait for next frame
                }

                // Pikamoon reached the target → Go idle
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Idle");

                // Idle for some time before walking again
                yield return new WaitForSeconds(roamIdleTime);
            }
        }
    }

    public void CapturePikamoon(Transform playerTransform)
    {
        player = playerTransform;
        isFollowing = false; // Start with roaming mode
    }
}
