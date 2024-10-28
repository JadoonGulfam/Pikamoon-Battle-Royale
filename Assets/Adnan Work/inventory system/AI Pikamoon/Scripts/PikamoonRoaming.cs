using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PikamoonRoaming : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isRoaming = false;
    private float idleTimer;
    private bool isIdle = false;

    public float idleTime = 2f; // Time Pikamoon stays idle before moving to the next position

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.enabled = true;
        idleTimer = idleTime;
        EnableRoaming();
    }

    private void Update()
    {
        if (isRoaming)
        {
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
                        GetRandomDestination();
                    }
                }
            }
            else
            {
                // Pikamoon is moving
                float moveBlend = navMeshAgent.velocity.magnitude > 0.1f ? 1.0f : 0.0f;
                animator.SetFloat("Move", Mathf.MoveTowards(animator.GetFloat("Move"), moveBlend, Time.deltaTime * 3));
                isIdle = false;
            }
        }
    }

    public void EnableRoaming()
    {
        isRoaming = true;
        GetRandomDestination();
    }

    public void DisableRoaming()
    {
        isRoaming = false;
        navMeshAgent.ResetPath();
        animator.SetFloat("Move", 0); // Reset to idle animation
        isIdle = false;
        idleTimer = idleTime;
    }

    private void StartIdle()
    {
        isIdle = true;
        animator.SetFloat("Move", 0); // Set idle animation
        idleTimer = idleTime;
    }

    private void GetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 5f + transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas);
        navMeshAgent.SetDestination(hit.position);
    }
}
