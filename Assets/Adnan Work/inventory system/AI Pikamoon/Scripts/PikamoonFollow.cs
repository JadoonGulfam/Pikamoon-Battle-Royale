using Fusion;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PikamoonFollow : MonoBehaviour
{
    public Transform followMaster;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isFollowing = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.enabled = true;
        
    }

    private void Update()
    {
        if (isFollowing && followMaster != null)
        {
            navMeshAgent.SetDestination(followMaster.position);

            // Smooth animation blend between walking and idle based on speed
            float moveBlend = navMeshAgent.velocity.magnitude > 0.1f ? 1.0f : 0.0f;
            animator.SetFloat("Move", Mathf.MoveTowards(animator.GetFloat("Move"), moveBlend, Time.deltaTime * 3));
        }
    }

    public void EnableFollowing()
    {
        isFollowing = true;
    }

    public void DisableFollowing()
    {
        isFollowing = false;
        navMeshAgent.ResetPath(); // Stop the NavMeshAgent
        animator.SetFloat("Move", 0); // Set to idle animation
    }
}
