using System.Collections;
using UnityEngine;

public class AttackParticales : MonoBehaviour
{
     float speed = 10f; // Speed of the movement along the Z-axis
    public bool isAiCast; // Variable to indicate if AI cast the particle

    private Transform target; // The target to move towards

    private void OnEnable()
    {
        StartCoroutine(MoveTowardsTarget());
    }
   
    // Initialize the particle with a target and speed
    public void Initialize(Transform targetTransform, float speed)
    {
        target = targetTransform;
        //this.speed = speed;
    }

    private IEnumerator MoveTowardsTarget()
    {
        float elapsedTime = 0f;

        while (target != null && elapsedTime < 1)
        {
            // Calculate direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Rotate the particle to face the direction of movement
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
            }

            // Move the particle towards the target at constant speed
            transform.position += direction * speed * Time.deltaTime;

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Deactivate the particle after 1.5 seconds
        gameObject.SetActive(false);
    }
}
