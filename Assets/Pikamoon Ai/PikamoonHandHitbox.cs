using UnityEngine;

public class PikamoonHandHitbox : MonoBehaviour
{
    public float damage = 10f;
    public bool canDamage = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit");
            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.TakeDamage(damage);
                canDamage = false; // avoid multiple hits per swing
            //}
        }
    }
}
