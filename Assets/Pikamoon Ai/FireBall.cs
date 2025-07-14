using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 5f; // auto-destroy after time

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after x seconds if it doesn't hit
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the hit object is the player
        if (other.CompareTag("Player"))
        {
            // Apply damage to the player
            //PlayerHealth health = other.GetComponent<PlayerHealth>();
            //if (health != null)
            //{
            //    health.TakeDamage(damage);
            //}
            Debug.Log("hit player");
            // Destroy the fireball
            Destroy(gameObject);
        }
        else if (!other.isTrigger) // Optional: destroy if it hits a wall or non-trigger
        {
            Destroy(gameObject);
        }
    }
}
