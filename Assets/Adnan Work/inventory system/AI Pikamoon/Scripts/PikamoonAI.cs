using System.Collections;
using Fusion;
using UnityEngine;
using UnityEngine.AI;
using static PikamoonPopulationManager;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PikamoonAI : NetworkBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public Transform player;
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
       // player=GameObject.FindWithTag("Player").transform;
    }
    public void setPlayer(Transform _player)
    {
        player = _player;
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            //print("player following");
            FollowPlayer();
        }
        else if (isRoaming)
        {
            //print("roaming");
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
        else 
            {
                print("player is null");
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
        animator.SetTrigger("StopMove");
        animator.ResetTrigger("StartMove");

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
            animator.SetTrigger("StartMove");
            animator.ResetTrigger("StopMove");
        }
    }

    private IEnumerator IdleBeforeNextRoam()
    {
        animator.SetFloat("Move", 0); // Idle animation
        animator.SetTrigger("StopMove");
        animator.ResetTrigger("StartMove");
        yield return new WaitForSeconds(idleTimeBetweenRoaming);
        SetNewRoamDestination();
    }

    public void Call_RPC_AddPikamoon(GameObject pikamoon)
    {
        NetworkObject networkObject = pikamoon.GetComponent<NetworkObject>();

        if (networkObject != null && Runner.FindObject(networkObject.Id) != null)
        {
            RPC_AddPikamoon(networkObject.Id);
        }
        else
        {
            Debug.LogError("Pikamoon NetworkObject is not spawned in the Fusion Runner!");
        }
        print("Call_RPC_AddPikamoon called");
    }
    public void Call_RPC_SpawnPikamoon(GameObject pikamoonToSpawn, Vector3 spawnPosition)
    {
       
        NetworkObject networkObject = pikamoonToSpawn.GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            RPC_SpawnPikamoon(networkObject.Id, spawnPosition);
        }
        print("Call_RPC_SpawnPikamoon called");

    }


    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_AddPikamoon(NetworkId pikamoonId, RpcInfo info = default)
    {
        NetworkObject netObj = Runner.FindObject(pikamoonId);
        if (netObj == null)
        {
            Debug.LogError($"RPC_AddPikamoon failed: Pikamoon with ID {pikamoonId} not found!");
            return;
        }

        GameObject pikamoon = netObj.gameObject;
        pikamoon.SetActive(false);
        print("RPC_AddPikamoon called");
    }


    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SpawnPikamoon(NetworkId pikamoonId, Vector3 spawnPosition, RpcInfo info = default)
    {
        GameObject pikamoonToSpawn = Runner.FindObject(pikamoonId)?.gameObject;
        if (pikamoonToSpawn != null)
        {
            pikamoonToSpawn.transform.position = spawnPosition;
            pikamoonToSpawn.SetActive(true);
        }
        print("RPC_SpawnPikamoon called");
    }
}
