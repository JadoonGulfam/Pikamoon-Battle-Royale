using Fusion;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PikamoonFollow : MonoBehaviour
{
    public Transform followMaster;
    private NavMeshAgent navMeshAgent;
    private bool isFollowing = false;
    private Animator animator; // Reference to the Animator

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Initialize the Animator
        navMeshAgent.enabled = true;
    }

    private void Update()
    {
        if (isFollowing && followMaster != null)
        {
            navMeshAgent.SetDestination(followMaster.position);
            animator.SetFloat("Move", navMeshAgent.velocity.magnitude > 0.1f ? 1 : 0); // Enable walk animation
        }
    }

    public void EnableFollowing()
    {
        isFollowing = true;
    }

    public void DisableFollowing()
    {
        isFollowing = false;
        navMeshAgent.ResetPath(); // Reset the NavMeshAgent path when stopped
        animator.SetFloat("Move", 0); // Stop walk animation
    }
}
