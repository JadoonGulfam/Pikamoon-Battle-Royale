using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class PikamoonRoaming : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isRoaming = false;
    private float idleTimer;
    private float moveTimer;
    private bool isIdle = false;

    public float idleTimeMin = 1f; // Minimum idle time
    public float idleTimeMax = 2.5f; // Maximum idle time
    public float moveTimeMin = 5f; // Minimum move time
    public float moveTimeMax = 8f; // Maximum move time
    public float maxRoamingAngle = 90f; // Maximum angle for random roaming positions

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.enabled=false;
        // Delay enabling the agent to ensure it is placed on the NavMesh
        StartCoroutine(InitializeNavMeshAgent());
    }

    private IEnumerator InitializeNavMeshAgent()
    {
        yield return new WaitForSeconds(0.1f); // Allow time for object initialization

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position; // Snap to closest NavMesh point
            yield return new WaitForSeconds(0.1f); // Ensure transform updates before enabling agent
            navMeshAgent.enabled = true;
        }
        else
        {
            Debug.LogError("Pikamoon was spawned outside of NavMesh!");
        }

        idleTimer = Random.Range(idleTimeMin, idleTimeMax);
        EnableRoaming();
    }

    private void Update()
    {
        if (!navMeshAgent || !navMeshAgent.isOnNavMesh)
            return;


        if (isRoaming)
        {
            print("i am roaming");
            // Check if Pikamoon has reached the current destination
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!isIdle)
                {
                    StartIdle();
                }
                else
                {
                    idleTimer -= Time.deltaTime;
                    if (idleTimer <= 0f)
                    {
                        StartMove();
                    }
                }
            }
            else
            {
                // Pikamoon is moving
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0f)
                {
                    StartIdle();
                }
                else
                {
                    float moveBlend = navMeshAgent.velocity.magnitude > 0.1f ? 1.0f : 0.0f;
                    animator.SetFloat("Move", Mathf.MoveTowards(animator.GetFloat("Move"), moveBlend, Time.deltaTime * 10));
                    isIdle = false;
                }
            }
        }
    }

    public void EnableRoaming()
    {
        isRoaming = true;
        StartIdle();
    }

    public void DisableRoaming()
    {
        isRoaming = false;
        navMeshAgent.ResetPath();
        animator.SetFloat("Move", 0); // Reset to idle animation
        isIdle = false;
        idleTimer = Random.Range(idleTimeMin, idleTimeMax);
    }

    private void StartIdle()
    {
        isIdle = true;
        animator.SetFloat("Move", 0); // Set idle animation
        idleTimer = Random.Range(idleTimeMin, idleTimeMax);
    }

    private void StartMove()
    {
        isIdle = false;
        moveTimer = Random.Range(moveTimeMin, moveTimeMax);
        GetRandomDestination();
    }

    private void GetRandomDestination()
    {
        Vector3 forward = transform.forward;
        float angle = Random.Range(-maxRoamingAngle, maxRoamingAngle); // Limit the random angle to within maxRoamingAngle
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 randomDirection = rotation * forward;

        randomDirection *= Random.Range(2f, 5f); // Scale the direction to a random distance
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            GetRandomDestination(); // Retry if the position is invalid
        }
    }
}
