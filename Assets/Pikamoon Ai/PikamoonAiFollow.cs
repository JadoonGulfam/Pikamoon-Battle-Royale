using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PikamoonAiFollow : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private PikamoonAi pikamoonAi;
    public Transform player;
    public Transform attackTarget;

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

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public bool isAttacking = false;
    private bool Attack = false;
    private bool reachedPlayer = false;

    private bool isFollowing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pikamoonAi = GetComponent<PikamoonAi>();
        navMeshAgent.speed = walkSpeed;
    }

    void Update()
    {
        if (player == null || !pikamoonAi.isCaptured) return;

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (attackTarget != null)
                {
                    SetAttackTarget(attackTarget);
                }
            }
            if (Input.GetKeyDown(KeyCode.X) && isAttacking)
            {
                if (attackTarget != null)
                {
                    StopAttack();
                }
            }

            if (attackTarget != null && isAttacking)
            {
                AttackBehavior();
                return;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > followStartDistance)
            {
                // Player is far -> Start following
                StartFollowing();
            }
            else if (distanceToPlayer < followStopDistance)
            {
                // Player is close -> Start roaming
                if (!isRoaming && !reachedPlayer)
                {
                    //isFollowing = false;
                   // isRoaming = true;
                    reachedPlayer = true;
                    StartCoroutine(WaitBeforeRoaming());
                   // StartCoroutine(RoamBehavior());
                }
            }

            if (isFollowing)
            {
                FollowPlayer(distanceToPlayer);
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
            animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Run);
           // animator.SetTrigger("Run");
            //animator.ResetTrigger("Walk");
        }
        else if (distanceToPlayer > followDistance)
        {
            // Walk towards the player
            navMeshAgent.speed = walkSpeed;
            animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Walk);
        }
        else
        {
            // Stop moving
            navMeshAgent.speed = 0;
            animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Idle);
            //if (!reachedPlayer)
            //{
            //    reachedPlayer = true;
            //    StartCoroutine(WaitBeforeRoaming());
            //}
        }

        navMeshAgent.SetDestination(player.position);
    }
    private IEnumerator WaitBeforeRoaming()
    {
        yield return new WaitForSeconds(10f); // Wait 3 seconds (Adjust as needed)

        // If the player is still nearby, start roaming
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < followStopDistance)
        {
            isFollowing = false;
            isRoaming = true;
            StartCoroutine(RoamBehavior());
        }

        reachedPlayer = false; // Reset flag
    }
    private IEnumerator RoamBehavior()
    {
        while (isRoaming)
        {
            Vector3 roamPoint = GetRandomPoint();

            if (roamPoint != Vector3.zero)
            {
                navMeshAgent.SetDestination(roamPoint);
                navMeshAgent.speed = walkSpeed;
                animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Walk);

                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
                {
                    yield return null;
                }

                animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Idle);

                yield return new WaitForSeconds(roamIdleTime);
            }
        }
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }
    public void SetAttackTarget(Transform target)
    {
        attackTarget = target;
        isAttacking = true;
    }
    private void AttackBehavior()
    {
        if (attackTarget == null)
        {
            StopAttack();
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, attackTarget.position);

        if (distanceToTarget > attackRange)
        {
            navMeshAgent.SetDestination(attackTarget.position);
            navMeshAgent.speed = runSpeed;
            animator.SetTrigger("Run");
            animator.ResetTrigger("Attack");
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
            animator.ResetTrigger("Run");

            if (!Attack)
            {
                StartCoroutine(AttackRoutine());
            }
        }
    }
    private IEnumerator AttackRoutine()
    {
        Attack = true;
       // animator.SetTrigger("Attack");

        while (attackTarget != null)
        {
            animator.SetTrigger("Attack");

            // Simulate attack delay
            yield return new WaitForSeconds(attackCooldown);
        }

       // animator.ResetTrigger("Attack");
        //StopAttack();
    }

    public void StopAttack()
    {
        animator.ResetTrigger("Attack");
        attackTarget = null;
        Attack = false;
        isAttacking = false;
        navMeshAgent.speed = walkSpeed;
    }
    public void AssignPlayerToFollow(Transform playerTransform)
    {
        player = playerTransform;    
    }
}
