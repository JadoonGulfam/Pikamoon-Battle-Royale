using DG.DemiLib;
using UnityEngine;
using UnityEngine.AI;

public class PikamoonAiMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private PikamoonAi pikamoonAi;

    private float idleTimer;
    private float maxRoamingAngle = 90f;
    private float idleTimemin = 2f, idleTimemax = 5f;
    private float minRange = 30f, maxRange = 40f;

    public float runSpeed = 8f; // Speed when fleeing
    public float walkSpeed = 1f;
    public bool isRoaming = false;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        pikamoonAi = GetComponent<PikamoonAi>();
        EnableRoaming();
    }
    private void Update()
    {
        if(pikamoonAi.pikamoonFollow.isCapture) return;
        if (!isRoaming) return;

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            switch (pikamoonAi.CurrentState)
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
        pikamoonAi.SetState(PikamoonState.Idle);
        navMeshAgent.ResetPath();
        idleTimer = Random.Range(idleTimemin, idleTimemax);
    }

    private void StartMove()
    {
        bool shouldRun = Random.value < 0.3f; // 30% chance to run

        if (shouldRun) // Run
        {
            pikamoonAi.SetState(PikamoonState.Run);
            navMeshAgent.speed = runSpeed;
        }
        else // Walk
        {
            pikamoonAi.SetState(PikamoonState.Walk);
            navMeshAgent.speed = walkSpeed;
        }
        SetRandomDestination();
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
}
