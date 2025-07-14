using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PikamoonAiHealth))]
[RequireComponent(typeof(PikamoonAiFollow))]
public class PikamoonAi : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorOverrideController overrideControllers; // Different Pikamoon animations
    [SerializeField] private PikamoonAiHealth pikamoonHealth;
    [SerializeField] private PikamoonAiFollow pikamoonFollow;

    public LayerMask playerLayer; // Layer mask to detect the player
    private PikamoonState pikaState;
    public PikamoonType pikaType;
    public GameObject alertMark;
    public GameObject stunnedMark;
    public Transform alertMarkTransform;

    private bool isRoaming = false;
   // private bool isIdle = false;
    private bool isAlert = false; // New alert state
    private bool isAlertDuration = false; // New alert state
    private bool isAttacking = false; // Track attack state
    private bool isFleeing = false; // New fleeing state
    private bool isStunned = false;

    private int friendlyHitCount = 0;
    private int friendlyAttackThreshold; // Random hit threshold
                                         //  private float attackTimer; // Timer for attack trigger
    private float idleTimer;
    private float maxRoamingAngle = 90f;
    private float alertTimer;
    private float idleTimemin = 2f, idleTimemax = 5f;
    private float minRange = 12f, maxRange = 15f;


    private float fleeThreshold = 40;
    private float stunThreshold = 10;
    private float agroRange = 8f; // New agro range
    [SerializeField] private float protectionRadius = 40f;
    [SerializeField] private float fleeDistance = 20f; // Distance to run away
    [SerializeField] private float fleeSpeed = 8f; // Speed when fleeing
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float attackRange = 2f; // Distance at which Pikamoon stops to attack
    [SerializeField] private float stunDuration = 10f;
    [SerializeField] private float alertDuration = 10f; // Time Pikamoon stays in alert state
    [SerializeField] private float alertRange = 15f; // Detection range for player

    enum PikamoonAnimState { Idle = 0, Walk = 1, Run = 2, Alert = 3 }

    private Transform player;

    private void Start()
    {
        friendlyAttackThreshold = Random.Range(2, 4);
        if (!pikamoonFollow.isCapture)
            EnableRoaming();
    }
    private void Update()
    {
        if (!navMeshAgent.enabled || !navMeshAgent.isOnNavMesh || pikamoonFollow.isCapture)
            return;

        // Check if the player is in range
        bool playerDetected = PlayerDetected();
        agro = PlayerInAgroRange();
        if (!playerDetected) isAlertDuration = false;

        if (isFleeing)
        {
            if (navMeshAgent.remainingDistance > alertRange)
            {
                isFleeing = false;
                navMeshAgent.speed = walkSpeed;
                navMeshAgent.ResetPath();
                EnableRoaming(); // Resume normal behavior
            }
        }

        if (pikamoonHealth.currentHealth <= stunThreshold && !isStunned && playerDetected && !isFleeing)
        {
            StartCoroutine(Stun());
            return;
        }
        if (pikamoonHealth.currentHealth <= fleeThreshold && playerDetected && !isStunned)
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
                else if (pikaType == PikamoonType.Protective)
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
            switch (pikaState)
            {
                case PikamoonState.Walk:
                case PikamoonState.Run:
                    EnableRoaming(); // Transition back to Idle
                    break;

                case PikamoonState.Idle:
                    idleTimer -= Time.deltaTime;
                    if (idleTimer <= 0.5f)
                    {
                        StartMove(); // Transition to Walk or Run
                    }
                    break;
            }

        }
    }
    public void EnableRoaming()
    {
        isRoaming = true;
        //isIdle = true;
        pikaState = PikamoonState.Idle;
        animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Idle); // 0 = Idle
        navMeshAgent.ResetPath();
        idleTimer = Random.Range(idleTimemin, idleTimemax);
    }

    private void StartMove()
    {
       // isIdle = false;
        // Randomly decide to walk or run
        bool shouldRun = Random.value < 0.3f; // 30% chance to run

        if (shouldRun) // Run
        {
            pikaState = PikamoonState.Run;
            animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Run);
            navMeshAgent.speed = fleeSpeed;
        }
        else // Walk
        {
            pikaState = PikamoonState.Walk;
            animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Walk);
            navMeshAgent.speed = walkSpeed;
        }
        SetRandomDestination();
    }
    GameObject alertMarkExclamation;
    private void EnterAlertState()
    {
        isAlert = true;
        isRoaming = false;
       // isIdle = false;
        pikaState = PikamoonState.Alert;
        navMeshAgent.ResetPath();
        animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Alert);
        alertTimer = alertDuration;
        //attackTimer = 0f; // Reset attack timer
        agro = false;
        if (alertMark != null)
        {
            if (alertMarkExclamation == null)
                alertMarkExclamation = Instantiate(alertMark, alertMarkTransform);
            alertMarkExclamation.SetActive(true);
        }
    }
    private void ExitAlertState()
    {
        isAlert = false;
        if(alertMarkExclamation != null )
        alertMarkExclamation.SetActive(false);
        EnableRoaming(); // Start moving immediately after alert
    }
    private void StartAttack()
    {
        isAttacking = true;
        isAlert = false;
        pikaState = PikamoonState.Run;
        navMeshAgent.speed = fleeSpeed;
        animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Run);
        navMeshAgent.SetDestination(player.position);
        alertTimer = 0f;
    }
    Coroutine temp;
    bool agro;
    private void AttackPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (player == null || distance > alertRange)
        {
            isAttacking = false;
            ExitAlertState();
            return;
        }
        if (distance > agroRange && agro)
        {
            isAttacking = false;
            EnterAlertState();
            return;
        }
        if (distance <= attackRange && temp == null)
        {
            navMeshAgent.ResetPath();
            pikaState = PikamoonState.Attack;
            isAttacking = true;
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

            // Rotate towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Keep only horizontal rotation
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // smooth turn
            }

            animator.SetTrigger("Attack");
            animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Idle);
            yield return new WaitForSeconds(2f); // Adjust attack interval as needed
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange) // Stop attacking if player moves out
            {
                navMeshAgent.isStopped = false;
                isAttacking = false;
                EnterAlertState();
                //temp = null;
                StopAttackCoroutine();
                yield break;
            }
            if (distanceToPlayer > attackRange && distanceToPlayer > agroRange && agro) // Stop attacking if player moves out
            {
                navMeshAgent.isStopped = false;
                isAttacking = false;
                // animator.ResetTrigger("Attack");
                EnterAlertState();
                // temp = null;
                StopAttackCoroutine();
                yield break;
            }
        }
        // temp = null;
        StopAttackCoroutine();
    }

    private void StopAttackCoroutine()
    {
        if (temp != null)
        {
            StopCoroutine(temp);
            temp = null;
        }
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

    public void TakeDamage(float damage, Transform _attacker) // Function to reduce health
    {
        if (/*isDead || */isStunned) return;
        pikamoonHealth.ReduceHealth(damage);
        if (pikamoonHealth.IsDead()) { Die(); return; } // If Pikamoon's health is 0, trigger death

        StartCoroutine(StopMovementForHit());
        // if (pikamoonHealth.currentHealth <= fleeHealthThreshold) StartFleeing();
        // If Pikamoon is in alert state and gets attacked, react based on type
        if (isAlert || isRoaming)
        {
                switch (pikaType)
                {
                    case PikamoonType.Aggressive:
                    if (PlayerDetected()) StartAttack(); // Attack immediately
                        break;

                    case PikamoonType.Friendly:
                        friendlyHitCount++;
                        if (friendlyHitCount >= friendlyAttackThreshold)
                        {
                        if (PlayerDetected()) StartAttack(); // Attack after enough hits
                        }
                        break;
                    case PikamoonType.Protective:
                    if (PlayerDetected()) StartAttack();
                        break;
                    case PikamoonType.Cowardly:
                    if (PlayerDetected()) StartFleeing(); // Run away immediately
                        break;
                }
            //Notify nearby protective Pikamoons
            NotifyNearbyProtectivePikamoons(_attacker);
        }
    }
    private void NotifyNearbyProtectivePikamoons(Transform _attacker)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, protectionRadius); // Adjust as needed
        foreach (Collider col in colliders)
        {
            PikamoonAi nearbyPikamoon = col.GetComponent<PikamoonAi>();
            if (nearbyPikamoon != null && nearbyPikamoon != this)
            {
                if (nearbyPikamoon.pikaType == PikamoonType.Protective && !nearbyPikamoon.isStunned && !nearbyPikamoon.isFleeing)
                {
                    float dist = Vector3.Distance(nearbyPikamoon.transform.position, this.transform.position);
                    if (dist <= nearbyPikamoon.protectionRadius)
                    {
                        nearbyPikamoon.OnAllyAttacked(_attacker);
                    }
                }
            }
        }
    }
    public void OnAllyAttacked(Transform _attacker)
    {
        Debug.Log("yaha aya ha bhai");
        StartAttackOnPlayer(_attacker);
    }
    private void StartAttackOnPlayer(Transform _player)
    {
        isAttacking = true;
        isAlert = false;
        pikaState = PikamoonState.Run;
        navMeshAgent.speed = fleeSpeed;
        animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Run);
        navMeshAgent.SetDestination(_player.position);
        alertTimer = 0f;
    }
    GameObject stunnedMarkInstance;
    private IEnumerator Stun()
    {
        isStunned = true;
        isRoaming = false;
        isAlert = false;
       // isIdle = false;
        isAttacking = false;
        isFleeing = false;

        pikaState = PikamoonState.Stunned;
        navMeshAgent.isStopped = true; // Stop movement
                                       //  animator.ResetTrigger("Attack");
                                       // animator.ResetTrigger("Walk");
                                       // animator.ResetTrigger("Alert");
                                       // animator.ResetTrigger("Idle");
                                       //  animator.ResetTrigger("Run");
        animator.SetTrigger("Stunned"); // Play stunned animation
        if (stunnedMarkInstance == null)
            stunnedMarkInstance = Instantiate(stunnedMark, alertMarkTransform);
        stunnedMarkInstance.SetActive(true);
        yield return new WaitForSeconds(stunDuration); // Wait for stun duration

        isStunned = false;
        navMeshAgent.isStopped = false;

        //// Resume behavior after stun
        if (pikamoonHealth.currentHealth <= fleeThreshold)
        {
            animator.ResetTrigger("Stunned");
            StartFleeing(); // If health is still low, flee
        }
        else
        {
            animator.ResetTrigger("Stunned");
            EnableRoaming(); // Otherwise, resume roaming
        }
    }
    private void StartFleeing()
    {
        isFleeing = true;
        isRoaming = false;
        isAlert = false;
       // isIdle = false;
        isAttacking = false;
        pikaState = PikamoonState.Run;
        navMeshAgent.speed = fleeSpeed; // Increase speed
        if (alertMarkExclamation != null && alertMarkExclamation.activeInHierarchy)                                // animator.ResetTrigger("Attack");
            alertMarkExclamation.SetActive(false);                                // animator.ResetTrigger("Alert");
                                                                                  // animator.ResetTrigger("Idle");
        animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Run); // Play flee animation

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
       // isIdle = false;
        isAlert = false;
        if (alertMarkExclamation != null && alertMarkExclamation.activeInHierarchy)                                // animator.ResetTrigger("Attack");
            Destroy(alertMarkExclamation);
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
            //SetRandomDestination(); // Retry if not found
            // Fallback in case no valid NavMesh position was found
            navMeshAgent.SetDestination(transform.position);
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
        if (attackVFX != null)
        {
            GameObject Vfx = Instantiate(attackVFX, initPosition.position, initPosition.rotation);
            Vfx.GetComponent<FireProjectile>().target = player;
        }
    }

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 15f;
    public void LaunchProjectileAtPlayer()
    {
        if (fireballPrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Vector3 direction = (player.position + Vector3.up * 1.2f - firePoint.position).normalized;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }

        Debug.Log("Fireball launched!");
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