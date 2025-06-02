using UnityEngine;

public class PlayerLookAtTarget : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private Transform target; // The target the player should look at.

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 5f; // Speed of rotation to smooth the movement.

    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned in PlayerLookAtTarget script.");
            return;
        }

        LookAtTarget();
    }

    private void LookAtTarget()
    {
        // Calculate the direction to the target
        Vector3 directionToTarget = target.position - transform.position;

        // Calculate the desired rotation
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

        // Smoothly rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
