using System.Collections;
using UnityEngine;

public class AttackParticles : MonoBehaviour
{
    public enum CasterType
    {
        Player,
        AI
    }

    [Header("Particle Settings")]
    public float speed = 10f; // Speed of the movement


    [Header("Attack Data")]
    public CasterType casterType; // Enum to indicate if the particle was cast by a player or AI
    public float damageStrength; // The damage strength of the attack

    private Transform target; // The target to move towards

    private void OnEnable()
    {
        StartCoroutine(MoveTowardsTarget());
    }

    public void Initialize(Transform targetTransform, float speed, bool isAi, float damage)
    {

        target = targetTransform;
        this.speed = speed;
        casterType = isAi ? CasterType.AI : CasterType.Player;
        damageStrength = damage;

        print("is ai cast" + isAi);
        
    }
    public bool IsAICast()
    {
        return casterType == CasterType.AI;
    }

    public bool IsPlayerCast()
    {
        return casterType == CasterType.Player;
    }
    private IEnumerator MoveTowardsTarget()
    {
        float duration = 2f; // Duration to move towards the target (2 seconds)
        float elapsedTime = 0f;

        while (target != null && elapsedTime < duration)
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

        // Deactivate the particle after 2 seconds
        gameObject.SetActive(false);
    }
}
