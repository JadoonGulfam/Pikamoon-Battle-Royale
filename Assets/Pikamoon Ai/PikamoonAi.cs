using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PikamoonAi : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private PikamoonAiHealth pikamoonHealth;
    private PikamoonAiFollow pikamoonFollow;
    PikamoonAiMovement movement;

    public LayerMask playerLayer; // Layer mask to detect the player
    public PikamoonType pikaType;
    public GameObject alertMark;
    public GameObject stunnedMark;
    public Transform alertMarkTransform;
    private bool isAlert = false; // New alert state
    private bool isAlertDuration = false; // New alert state
    private bool isAttacking = false; // Track attack state
    private bool isFleeing = false; // New fleeing state
    private bool isStunned = false;

    private int friendlyHitCount = 0;
    private int friendlyAttackThreshold; // Random hit threshold
                                         //  private float attackTimer; // Timer for attack trigger
    private float alertTimer;
    public bool isCaptured = false;
    private float fleeThreshold = 40;
    private float stunThreshold = 10;
    private float agroRange = 8f; // New agro range
    [SerializeField] private float protectionRadius = 40f;
    [SerializeField] private float fleeDistance = 20f; // Distance to run away
    [SerializeField] private float attackRange = 2f; // Distance at which Pikamoon stops to attack
    [SerializeField] private float stunDuration = 10f;
    [SerializeField] private float alertDuration = 10f; // Time Pikamoon stays in alert state
    [SerializeField] private float alertRange = 15f; // Detection range for player

    //enum PikamoonAnimState { Idle = 0, Walk = 1, Run = 2, Alert = 3 }

    public Transform player;
    private PikamoonAiSound sounds;

    public PikamoonState CurrentState { get; private set; } = PikamoonState.Idle;
    public event System.Action<PikamoonState> OnStateChanged;

    public bool Stunned 
    {
        get { return isStunned; }
        set { isStunned = value; }
    }
    private void Start()
    {
        sounds = GetComponent<PikamoonAiSound>();
        movement = GetComponent<PikamoonAiMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pikamoonHealth = GetComponent<PikamoonAiHealth>();
        pikamoonFollow = GetComponent<PikamoonAiFollow>();
        friendlyAttackThreshold = Random.Range(2, 4);
    }
    private void Update()
    {
        if (!navMeshAgent.enabled || !navMeshAgent.isOnNavMesh || isCaptured)
            return;

        // Check if the player is in range
        bool playerDetected = IsPlayerInRange(alertRange);
        agro = IsPlayerInRange(agroRange);
        if (!playerDetected) isAlertDuration = false;

        if (isFleeing)
        {
            if (navMeshAgent.remainingDistance > alertRange)
            {
                isFleeing = false;
                navMeshAgent.speed = movement.WalkSpeed;
                navMeshAgent.ResetPath();
                movement.EnableRoaming(); // Resume normal behavior
            }
        }

        if (pikamoonHealth.currentHealth <= stunThreshold && !Stunned && playerDetected && !isFleeing)
        {
            StartCoroutine(Stun());
            return;
        }
        if (pikamoonHealth.currentHealth <= fleeThreshold && playerDetected && !Stunned)
        {
            StartFleeing();
            return;
        }
        if (isAttacking) // Pikamoon is already attacking
        {
            AttackPlayer();
            return;
        }

        if (isAlert && !isFleeing && !Stunned)
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
        if (playerDetected && !isAlert && !isAlertDuration && !isFleeing && !Stunned)
        {
            EnterAlertState();
            return;
        }
    }

    GameObject alertMarkExclamation;
    private void EnterAlertState()
    {
        isAlert = true;
        movement.IsRoaming = false;
        SetState(PikamoonState.Alert);
        navMeshAgent.ResetPath();
        sounds.PlaySound(sounds.alertClip, true);
        alertTimer = alertDuration;
        agro = false;
        if (alertMarkExclamation != null)
        {
            alertMarkExclamation.SetActive(true);
        }
        else
        {
            alertMarkExclamation = Instantiate(alertMark, alertMarkTransform);
        }
    }
    private void ExitAlertState()
    {
        isAlert = false;
        if (alertMarkExclamation != null)
            alertMarkExclamation.SetActive(false);
        movement.EnableRoaming(); // Start moving immediately after alert
    }
    private void StartAttack()
    {
        isAttacking = true;
        isAlert = false;
        SetState(PikamoonState.Run);
        navMeshAgent.speed = movement.RunSpeed;
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
            isAttacking = true;
            FacePlayer();
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

            FacePlayer();

            SetState(PikamoonState.Attack);
            yield return new WaitForSeconds(2f);
            SetState(PikamoonState.Idle);// Adjust attack interval as needed
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
    void FacePlayer()
    {
        // Rotate towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep only horizontal rotation
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // smooth turn
        }
    }
    private void StopAttackCoroutine()
    {
        if (temp != null)
        {
            StopCoroutine(temp);
            temp = null;
        }
    }
    private PikamoonState previousState; // store state before getting hit
    bool isGettingHit = false;
    private IEnumerator StopMovementForHit()
    {
        if (isGettingHit) yield break; // avoid overlapping hit reactions
        isGettingHit = true;

        // Save current state
        previousState = CurrentState;

        // Stop everything
        navMeshAgent.isStopped = true;
        movement.IsRoaming = false;
        isAttacking = false;
        isAlert = false;
        isFleeing = false;

        // Play hit reaction
        //SetState(PikamoonState.Idle); // optional: reset to idle
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Stunned");
        animator.SetTrigger("Hit");
        sounds.PlaySound(sounds.hitClip, false);

    }
    public void ResumePreviousState()
    {
        animator.ResetTrigger("Hit");
        navMeshAgent.isStopped = false;
        isGettingHit = false;

        switch (previousState)
        {
            case PikamoonState.Alert:
                EnterAlertState();
                break;

            case PikamoonState.Attack:
                StartAttack();
                break;

            case PikamoonState.Run:
            case PikamoonState.Walk:
            case PikamoonState.Idle:
                movement.EnableRoaming();
                if (isFleeing) StartFleeing();
                break;

            default:
                movement.EnableRoaming();
                break;
        }
    }

    public void TakeDamage(Transform _attacker) // Function to reduce health
    {
        //if (/*isDead || */Stunned) return;
        // pikamoonHealth.ReduceHealth(damage);
        if (pikamoonHealth.IsDead()) { Die(); return; } // If Pikamoon's health is 0, trigger death

        StartCoroutine(StopMovementForHit());
        // if (pikamoonHealth.currentHealth <= fleeHealthThreshold) StartFleeing();
        // If Pikamoon is in alert state and gets attacked, react based on type
        if (isAlert || movement.IsRoaming && !isCaptured)
        {
            switch (pikaType)
            {
                case PikamoonType.Aggressive:
                    if (IsPlayerInRange(alertRange)) StartAttack(); // Attack immediately
                    break;

                case PikamoonType.Friendly:
                    friendlyHitCount++;
                    if (friendlyHitCount >= friendlyAttackThreshold)
                    {
                        if (IsPlayerInRange(alertRange)) StartAttack(); // Attack after enough hits
                    }
                    break;
                case PikamoonType.Protective:
                    if (IsPlayerInRange(alertRange)) StartAttack();
                    break;
                case PikamoonType.Cowardly:
                    if (IsPlayerInRange(alertRange)) StartFleeing(); // Run away immediately
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
                if (nearbyPikamoon.pikaType == PikamoonType.Protective && !nearbyPikamoon.Stunned && !nearbyPikamoon.isFleeing)
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
        SetState(PikamoonState.Run);
        navMeshAgent.speed = movement.RunSpeed;
        navMeshAgent.SetDestination(_player.position);
        alertTimer = 0f;
    }
    GameObject stunnedMarkInstance;
    private IEnumerator Stun()
    {
        Stunned = true;
        movement.IsRoaming = false;
        isAlert = false;
        isAttacking = false;
        isFleeing = false;
        SetState(PikamoonState.Stunned);
        navMeshAgent.isStopped = true; // Stop movement                     
        sounds.PlaySound(sounds.stunClip, false);
        if (alertMarkExclamation != null)
            alertMarkExclamation.SetActive(false);
        if (stunnedMarkInstance == null)
            stunnedMarkInstance = Instantiate(stunnedMark, alertMarkTransform);
        yield return null; //new WaitForSeconds(stunDuration); // Wait for stun duration

        Stunned = false;
        navMeshAgent.isStopped = false;
        StopCoroutine(Stun());
        // Resume behavior after stun
            animator.ResetTrigger("Stunned");
            movement.EnableRoaming(); // Otherwise, resume roaming
    }
    private void StartFleeing()
    {
        isFleeing = true;
        movement.IsRoaming = false;
        isAlert = false;
        navMeshAgent.isStopped = false;
        isAttacking = false;
        SetState(PikamoonState.Run);
        navMeshAgent.speed = movement.RunSpeed; // Increase speed                                       
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
        movement.IsRoaming = false;
        isAlert = false;
        navMeshAgent.isStopped = true; // Stop movement
        SetState(PikamoonState.Killed);
        sounds.PlaySound(sounds.deathClip, false);
        Destroy(gameObject, 2f); // Destroy after 3 seconds
    }

    private bool IsPlayerInRange(float range)
    {
        // If we already have a player reference, just check distance
        if (player != null)
        {
            return Vector3.Distance(transform.position, player.position) <= range;
        }

        // Otherwise, try to detect new player in range
        Collider[] hits = Physics.OverlapSphere(transform.position, range, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform; // Cache the player
            return true;
        }

        return false;
    }

    public void SetState(PikamoonState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        UpdateAnimator(newState);
        OnStateChanged?.Invoke(newState);
    }

    private void UpdateAnimator(PikamoonState state)
    {
        switch (state)
        {
            case PikamoonState.Idle:
                animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Idle);
                break;

            case PikamoonState.Walk:
                animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Walk);
                break;

            case PikamoonState.Run:
                animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Run);
                break;

            case PikamoonState.Alert:
                animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Alert);
                break;

            case PikamoonState.Attack:
                animator.SetTrigger("Attack");
                break;

            case PikamoonState.Stunned:
                animator.SetTrigger("Stunned");
                break;

            case PikamoonState.Killed:
                animator.SetTrigger("Killed");
                break;
        }
    }
    public void CapturedByPlayer(Transform playerTransform)
    {
        isCaptured = true;

        pikamoonFollow.AssignPlayerToFollow(playerTransform);

        if (stunnedMarkInstance != null)
            stunnedMarkInstance.SetActive(false);
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
public enum PikamoonAnimState { Idle = 0, Walk = 1, Run = 2, Alert = 3 }