using UnityEngine;
using UnityEngine.AI;

public class navmeshtestscript : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = Vector3.one;
        
    }
}
