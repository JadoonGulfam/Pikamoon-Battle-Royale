using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public Transform target;  // The target to hit
    public float speed = 10f; // Movement speed
    public GameObject hitEffect; // Effect when fire hits target


    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy if no target
            return;
        }

        // Move fire towards target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if reached the target
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        Destroy(gameObject); // Destroy fire projectile
    }
}
