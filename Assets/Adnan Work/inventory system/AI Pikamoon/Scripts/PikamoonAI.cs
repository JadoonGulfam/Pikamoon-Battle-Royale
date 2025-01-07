using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PikamoonAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform player;
    private bool isFollowingPlayer = false;
    private bool isRoaming = false;

    public float followDistance = 2f;
    public float attackDistance = 1f;
    public float idleTimeBetweenRoaming = 1f; // Delay before moving to the next roaming point

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.enabled = true;
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            FollowPlayer();
        }
        else if (isRoaming)
        {
            if (!navMeshAgent.hasPath)
            {
                //no path found 
                StartCoroutine(IdleBeforeNextRoam());
            }
        }
    }

    public void FollowPlayer()
    {
        if (player != null)
        {
            isFollowingPlayer = true;
            navMeshAgent.SetDestination(player.position);
            animator.SetFloat("Move", Mathf.MoveTowards(animator.GetFloat("Move"), 1.0f, Time.deltaTime * 3));
            //animator.SetFloat("Move", 1); // Walking animation
        }

        // player is null 
    }

    public void AttackEnemy(Transform enemy)
    {
        // add attach logic here
        if (enemy != null)
        {
            navMeshAgent.SetDestination(enemy.position);
            animator.SetTrigger("Attack");
        }
    }

    public void StartRoaming()
    {
        // move random in enve
        isFollowingPlayer = false;
        isRoaming = true;
        SetNewRoamDestination();
    }

    // release pikamoon logic here 
    public void ReleasePikamoon()
    {

        isFollowingPlayer = false;
        isRoaming = true;
        navMeshAgent.ResetPath();
        SetNewRoamDestination();
        animator.SetFloat("Move", 0); // Idle animation
    }

    // find new positioin in open world  to go 
    private void SetNewRoamDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 50f;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10f, 1))
        {
            navMeshAgent.SetDestination(hit.position);
            animator.SetFloat("Move", 1); // Walking animation
        }
    }

    private IEnumerator IdleBeforeNextRoam()
    {
        animator.SetFloat("Move", 0); // Idle animation
        yield return new WaitForSeconds(idleTimeBetweenRoaming);
        SetNewRoamDestination();
    }
}
