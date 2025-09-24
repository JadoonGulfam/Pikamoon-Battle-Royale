using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PikamoonAiCombat : MonoBehaviour
{
    //private NavMeshAgent navMeshAgent;
    //private PikamoonAiMovement movement;
    //private PikamoonAi pikamoonAi;
    //[SerializeField] private float attackRange = 2f;
    //[SerializeField] private float protectionRadius = 40f;

    //public bool isAttacking = false; // Track attack state

    //public Transform player;
    //public LayerMask playerLayer;
    //public float alertRange = 15f;
    //public float agroRange = 8f; // New agro range
    //private void Start()
    //{
    //    navMeshAgent = GetComponent<NavMeshAgent>();
    //    pikamoonAi = GetComponent<PikamoonAi>();
    //    movement = GetComponent<PikamoonAiMovement>();
    //}
    //public bool PlayerDetected()
    //{
    //    Collider[] hits = Physics.OverlapSphere(transform.position, alertRange, playerLayer);
    //    if (hits.Length > 0)
    //    {
    //        player = hits[0].transform; // Assign the player
    //        return true;
    //    }
    //    else if (player != null)
    //    {
    //        player = null;
    //    }
    //    return false;
    //}
    //public bool PlayerInAgroRange()
    //{
    //    return player != null && Vector3.Distance(transform.position, player.position) <= agroRange;
    //}
    //public void StartAttack()
    //{
    //    isAttacking = true;
    //    pikamoonAi.isAlert = false;
    //   // movement.pikaState = PikamoonState.Run;
    //    pikamoonAi.SetState(PikamoonState.Run);
    //    navMeshAgent.speed = movement.runSpeed;
    //    //pikamoonAi.animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Run);
    //    navMeshAgent.SetDestination(player.position);
    //    pikamoonAi.alertTimer = 0f;
    //}
    //Coroutine temp;
    
    //public void AttackPlayer()
    //{
    //    float distance = Vector3.Distance(transform.position, player.position);
    //    if (player == null || distance > alertRange)
    //    {
    //        isAttacking = false;
    //        pikamoonAi.ExitAlertState();
    //        return;
    //    }
    //    if (distance > agroRange && pikamoonAi.agro)
    //    {
    //        isAttacking = false;
    //        pikamoonAi.EnterAlertState();
    //        return;
    //    }
    //    if (distance <= attackRange && temp == null)
    //    {
    //        navMeshAgent.ResetPath();
    //        //movement.pikaState = PikamoonState.Attack;
    //        //pikamoonAi.SetState(PikamoonState.Attack);
    //        isAttacking = true;
    //        temp = StartCoroutine(ContinuousAttack()); // Start continuous attack
    //    }
    //    else
    //    {
    //        navMeshAgent.SetDestination(player.position);
    //    }
    //}
    //private IEnumerator ContinuousAttack()
    //{
    //    while (isAttacking)
    //    {
    //        navMeshAgent.isStopped = true;

    //        // Rotate towards the player
    //        Vector3 direction = (player.position - transform.position).normalized;
    //        direction.y = 0; // Keep only horizontal rotation
    //        if (direction != Vector3.zero)
    //        {
    //            Quaternion lookRotation = Quaternion.LookRotation(direction);
    //            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // smooth turn
    //        }
    //        pikamoonAi.SetState(PikamoonState.Attack);
    //        //pikamoonAi.animator.SetTrigger("Attack");
    //        //pikamoonAi.SetState(PikamoonState.Idle);
    //        //pikamoonAi.animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Idle);
    //        yield return new WaitForSeconds(2f); // Adjust attack interval as needed
    //        pikamoonAi.SetState(PikamoonState.Idle);
    //        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    //        if (distanceToPlayer > attackRange) // Stop attacking if player moves out
    //        {
    //            navMeshAgent.isStopped = false;
    //            isAttacking = false;
    //            pikamoonAi.EnterAlertState();
    //            //temp = null;
    //            StopAttackCoroutine();
    //            yield break;
    //        }
    //        if (distanceToPlayer > attackRange && distanceToPlayer > agroRange && pikamoonAi.agro) // Stop attacking if player moves out
    //        {
    //            navMeshAgent.isStopped = false;
    //            isAttacking = false;
    //            // animator.ResetTrigger("Attack");
    //            pikamoonAi.EnterAlertState();
    //            // temp = null;
    //            StopAttackCoroutine();
    //            yield break;
    //        }
    //    }
    //    // temp = null;
    //    StopAttackCoroutine();
    //}

    //private void StopAttackCoroutine()
    //{
    //    if (temp != null)
    //    {
    //        StopCoroutine(temp);
    //        temp = null;
    //    }
    //}
    //public void NotifyNearbyProtectivePikamoons(Transform _attacker)
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, protectionRadius); // Adjust as needed
    //    foreach (Collider col in colliders)
    //    {
    //        PikamoonAiCombat nearbyPikamoon = col.GetComponent<PikamoonAiCombat>();
    //        PikamoonAi nearbyPikamoonmov = col.GetComponent<PikamoonAi>();
    //        if (nearbyPikamoon != null && nearbyPikamoon != this && nearbyPikamoonmov != null)
    //        {
    //            if (nearbyPikamoonmov.pikaType == PikamoonType.Protective) //&& !nearbyPikamoon.isStunned && !nearbyPikamoon.isFleeing)
    //            {
    //                float dist = Vector3.Distance(nearbyPikamoon.transform.position, this.transform.position);
    //                if (dist <= nearbyPikamoon.protectionRadius)
    //                {
    //                    nearbyPikamoon.OnAllyAttacked(_attacker);
    //                }
    //            }
    //        }
    //    }
    //}
    //public void OnAllyAttacked(Transform _attacker)
    //{
    //    Debug.Log("yaha aya ha bhai");
    //    StartAttackOnPlayer(_attacker);
    //}
    //private void StartAttackOnPlayer(Transform _player)
    //{
    //    isAttacking = true;
    //    pikamoonAi.isAlert = false;
    //   // movement.pikaState = PikamoonState.Run;
    //    pikamoonAi.SetState(PikamoonState.Run);
    //    navMeshAgent.speed = movement.runSpeed;
    //   //pikamoonAi.animator.SetFloat("Pikamoon", (int)PikamoonAnimState.Run);
    //    navMeshAgent.SetDestination(_player.position);
    //    pikamoonAi.alertTimer = 0f;
    //}
    //public Transform initPosition;


    //[SerializeField] private GameObject fireballPrefab;
    //[SerializeField] private float projectileSpeed = 15f;
    //public void LaunchProjectileAtPlayer()
    //{
    //    if (fireballPrefab == null || initPosition == null) return;

    //    GameObject projectile = Instantiate(fireballPrefab, initPosition.position, Quaternion.identity);
    //    projectile.GetComponent<FireBall>().pikamoon = this.transform;
    //    Vector3 direction = (player.position + Vector3.up * 1.2f - initPosition.position).normalized;

    //    Rigidbody rb = projectile.GetComponent<Rigidbody>();
    //    if (rb != null)
    //    {
    //        rb.linearVelocity = direction * projectileSpeed;
    //    }
    //}
}
