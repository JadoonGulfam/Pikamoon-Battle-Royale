using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PikamoonAi : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PikamoonAiHealth pikamoonHealth;
    [SerializeField]
    private PikamoonAiFollow pikamoonFollow;

    private bool isRoaming = false;
    private bool isIdle = false;
    private bool isAlert = false; // New alert state
    private bool isAlertDuration = false; // New alert state
    private bool isAttacking = false; // Track attack state
    private bool isFleeing = false; // New fleeing state
    private bool isStunned = false;

    private int friendlyHitCount = 0;
    private int friendlyAttackThreshold; // Random hit threshold
    public float fleeHealthThreshold = 40;


    private float attackTimer; // Timer for attack trigger
    private float idleTimer;

    public float fleeDistance = 20f; // Distance to run away
    public float fleeSpeed = 8f; // Speed when fleeing

    public float idleTimemin = 1f, idleTimemax = 2.5f;
    public float minRange = 10f, maxRange = 15f;
    public float maxRoamingAngle = 90f;
    private float alertTimer;
    public float attackTriggerTime = 2f; // Time before Pikamoon attacks
    public float attackRange = 2f; // Distance at which Pikamoon stops to attack
    public float agroRange = 5f; // New agro range
    public float stunDuration = 10f;

    public float alertDuration = 3f; // Time Pikamoon stays in alert state
    public float alertRange = 10f; // Detection range for player
    public LayerMask playerLayer; // Layer mask to detect the player
    private Transform player;

    public PikamoonState pikaState;
    public PikamoonType pikaType;

    private void Start()
    {
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        //pikamoonHealth = GetComponent<PikamoonAiHealth>(); // Get the health component
        //pikamoonFollow = GetComponent<PikamoonAiFollow>();
        friendlyAttackThreshold = Random.Range(2, 4);
        if (!pikamoonFollow.isCapture)
            EnableRoaming();
    }
    private void Update()
    {
        if (!navMeshAgent.enabled || !navMeshAgent.isOnNavMesh)
            return;

        if (pikamoonFollow.isCapture)
            return;


        // Check if the player is in range
        bool playerDetected = PlayerDetected();
        agro = PlayerInAgroRange();
        if (!playerDetected) isAlertDuration = false;



        if (isFleeing)
        {
            if (navMeshAgent.remainingDistance > alertRange)
            {
                Debug.Log("ya aya ha bhai abi tk nh aya");
                isFleeing = false;
                animator.ResetTrigger("Run");
                navMeshAgent.speed = 1;
                navMeshAgent.ResetPath();
                EnableRoaming(); // Resume normal behavior
            }
        }
       
        if (pikamoonHealth.currentHealth <= 25 && !isStunned && playerDetected && !isFleeing)
        {
            Stun();
            return;
        }
        if (pikamoonHealth.currentHealth <= fleeHealthThreshold && playerDetected && !isStunned)
        {
            StartFleeing();
            return;
        }
        if (isAttacking) // Pikamoon is already attacking
        {
            AttackPlayer();
            return;
        }

        if (isAlert && !isFleeing && !isStunned)
        {
            alertTimer -= Time.deltaTime;

            if (!playerDetected)
            {
                ExitAlertState(); // Resume roaming after alert
            }
            if (pikaType == PikamoonType.Aggressive && agro)
            {
                StartAttack();
            }
            if (alertTimer <= 1f)
            {
                if (pikaType == PikamoonType.Aggressive)
                {
                    StartAttack();
                }
                else if (pikaType == PikamoonType.Friendly)
                {
                    isAlertDuration = true;
                    ExitAlertState(); // Resume roaming after alert
                }
                else if (pikaType == PikamoonType.Cowardly)
                {
                    isAlertDuration = true;
                    StartFleeing(); // Start Fleeing after alert
                }
            }
            return;
        }
        // Enter alert state if player is detected and Pikamoon is not already alert
        if (playerDetected && !isAlert && !isAlertDuration && !isFleeing && !isStunned)
        {
            EnterAlertState();
            return;
        }
        //if (friendlyHitCount > 0 && Time.time - lastHitTime > hitResetTime)
        //{
        //    friendlyHitCount = 0;
        //    friendlyAttackThreshold = Random.Range(2, 5); // Reset with a new random value
        //}
        // Handle roaming logic
        if (!isRoaming) return;

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!isIdle) EnableRoaming();
            else if ((idleTimer -= Time.deltaTime) <= 0.5f) StartMove();
        }
        //else
        //{
        //pikaState = PikamoonState.Walk;
        //animator.ResetTrigger("Idle");
        //animator.SetTrigger("Walk");
        //}
    }
    public void EnableRoaming()
    {
        isRoaming = true;
        isIdle = true;
        pikaState = PikamoonState.Idle;
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Idle");
        idleTimer = Random.Range(idleTimemin, idleTimemax);
    }

    private void StartMove()
    {
        isIdle = false;
        animator.ResetTrigger("Idle");
        // Randomly decide to walk or run
        bool shouldRun = Random.value < 0.3f; // 30% chance to run

        if (shouldRun) // Run
        {
            pikaState = PikamoonState.Run;
            animator.SetTrigger("Run");
            navMeshAgent.speed = fleeSpeed;
        }
        else // Walk
        {
            pikaState = PikamoonState.Walk;
            animator.SetTrigger("Walk");
            navMeshAgent.speed = 1;
        }
        SetRandomDestination();
    }
    private void EnterAlertState()
    {
        isAlert = true;
        isRoaming = false;
        isIdle = false;
        pikaState = PikamoonState.Alert;
        navMeshAgent.ResetPath();
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Run");
        animator.SetTrigger("Alert");
        alertTimer = alertDuration;
        attackTimer = 0f; // Reset attack timer
        agro = false;
    }
    private void ExitAlertState()
    {
        isAlert = false;
        EnableRoaming(); // Start moving immediately after alert
    }
    private void StartAttack()
    {
        isAttacking = true;
        isAlert = false;
        pikaState = PikamoonState.Run;
        navMeshAgent.speed = fleeSpeed;
        animator.ResetTrigger("Alert");
        animator.SetTrigger("Run");
        navMeshAgent.SetDestination(player.position);
        alertTimer = 0f;
    }
    Coroutine temp;
    bool agro;
    private void AttackPlayer()
    {
        if (player == null || Vector3.Distance(transform.position, player.position) > alertRange)
        {
            isAttacking = false;
            animator.ResetTrigger("Attack");
            ExitAlertState();
            return;
        }
        if (Vector3.Distance(transform.position, player.position) > agroRange && agro)
        {
            isAttacking = false;
            animator.ResetTrigger("Attack");
            EnterAlertState();
            return;
        }
        if (Vector3.Distance(transform.position, player.position) <= attackRange && temp == null)
        {
            navMeshAgent.ResetPath();
            pikaState = PikamoonState.Attack;
            animator.ResetTrigger("Run");
            animator.ResetTrigger("Alert");
            isAttacking = true;
            // if (temp != null) {
            temp = StartCoroutine(ContinuousAttack()); // Start continuous attack
        }
        else
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
    private IEnumerator ContinuousAttack()
    {
        while (isAttacking)
        {
            navMeshAgent.isStopped = true;
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(2f); // Adjust attack interval as needed
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange) // Stop attacking if player moves out
            {
                navMeshAgent.isStopped = false;
                isAttacking = false;
                animator.ResetTrigger("Attack");
                EnterAlertState();
                temp = null;
                yield break;
            }
            if (distanceToPlayer > attackRange && distanceToPlayer > agroRange && agro) // Stop attacking if player moves out
            {
                navMeshAgent.isStopped = false;
                isAttacking = false;
                animator.ResetTrigger("Attack");
                EnterAlertState();
                temp = null;
                yield break;
            }
        }
        temp = null;
    }


    private IEnumerator StopMovementForHit()
    {
        // Play hit animation
        animator.SetTrigger("Hit");
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(0.5f); // Adjust delay as needed
        navMeshAgent.isStopped = false;
        animator.ResetTrigger("Hit");
    }

    public void TakeDamage(float damage) // Function to reduce health
    {

        pikamoonHealth.ReduceHealth(damage);
        if (pikamoonHealth.IsDead()) Die(); // If Pikamoon's health is 0, trigger death

        StartCoroutine(StopMovementForHit());
        // if (pikamoonHealth.currentHealth <= fleeHealthThreshold) StartFleeing();
        // If Pikamoon is in alert state and gets attacked, react based on type
        if (isAlert || isRoaming)
        {
            switch (pikaType)
            {
                case PikamoonType.Aggressive:
                    StartAttack(); // Attack immediately
                    break;

                case PikamoonType.Friendly:
                    friendlyHitCount++;
                    if (friendlyHitCount >= friendlyAttackThreshold)
                    {
                        StartAttack(); // Attack after enough hits
                    }
                    break;

                case PikamoonType.Cowardly:
                    StartFleeing(); // Run away immediately
                    break;
            }
        }
    }
    private void Stun()
    {
        isStunned = true;
        isRoaming = false;
        isAlert = false;
        isIdle = false;
        isAttacking = false;
        isFleeing = false;

        pikaState = PikamoonState.Stunned;
        navMeshAgent.isStopped = true; // Stop movement
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Alert");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.SetTrigger("Stunned"); // Play stunned animation

        //yield return new WaitForSeconds(stunDuration); // Wait for stun duration

        //isStunned = false;
        //navMeshAgent.isStopped = false;

        //// Resume behavior after stun
        //if (pikamoonHealth.currentHealth <= fleeHealthThreshold)
        //{
        //    animator.ResetTrigger("Stunned");
        //    StartFleeing(); // If health is still low, flee
        //}
        //else
        //{
        //    animator.ResetTrigger("Stunned");
        //    EnableRoaming(); // Otherwise, resume roaming
        //}
    }
    private void StartFleeing()
    {
        Debug.Log("ya aya ha bhai");
        isFleeing = true;
        isRoaming = false;
        isAlert = false;
        isIdle = false;
        isAttacking = false;
        pikaState = PikamoonState.Run;
        navMeshAgent.speed = fleeSpeed; // Increase speed
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Alert");
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Run"); // Play flee animation

        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleeTarget = transform.position + fleeDirection * fleeDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeTarget, out hit, 10f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position - fleeDirection * 10f); // Backup flee
        }
    }
    private void Die()
    {
        isAttacking = false;
        isRoaming = false;
        isIdle = false;
        isAlert = false;

        navMeshAgent.isStopped = true; // Stop movement
        animator.SetTrigger("Killed"); // Play death animation

        Destroy(gameObject, 2f); // Destroy after 3 seconds
    }
    private bool PlayerDetected()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, alertRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform; // Assign the player
            return true;
        }
        return false;
    }
    private void SetRandomDestination()
    {
        Vector3 direction = Quaternion.AngleAxis(Random.Range(-maxRoamingAngle, maxRoamingAngle), Vector3.up) * transform.forward;
        Vector3 targetPos = transform.position + direction * Random.Range(minRange, maxRange);

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            SetRandomDestination(); // Retry if not found
        }
    }
    private bool PlayerInAgroRange()
    {
        return player != null && Vector3.Distance(transform.position, player.position) <= agroRange;
    }
    public GameObject attackVFX;
    public Transform initPosition;
    public void Fire() 
    {
       GameObject Vfx =  Instantiate(attackVFX, initPosition.position, initPosition.rotation);
        Vfx.GetComponent<FireProjectile>().target = player;
    }
}
public enum PikamoonState
{
    Idle, Walk, Alert, Attack, Run, Stunned, Killed, Follow, LightAttack, HeavyAttack
}
public enum PikamoonType
{
    Aggressive, Friendly, Protective, Cowardly
}