using UnityEngine;

public class PikamoonAiCombat : MonoBehaviour
{


    [SerializeField] private PikamoonHandHitbox pikamoonHandHitbox;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float projectileSpeed = 15f;
    PikamoonAi PikamoonAi;
    public Transform initPosition;
    private void Start()
    {
        PikamoonAi = GetComponent<PikamoonAi>();
    }
    public void LaunchProjectileAtPlayer()
    {
        if (fireballPrefab == null || initPosition == null) return;

        GameObject projectile = Instantiate(fireballPrefab, initPosition.position, Quaternion.identity);
        projectile.GetComponent<FireBall>().pikamoon = this.transform;
        Vector3 direction = (PikamoonAi.player.position + Vector3.up * 1.2f - initPosition.position).normalized;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }

    public void EnableHitbox()
    {
        pikamoonHandHitbox.canDamage = true;
        pikamoonHandHitbox._collider.enabled = true;
    }
    public void DisableHitbox()
    {
        pikamoonHandHitbox.canDamage = false;
        pikamoonHandHitbox._collider.enabled = false;
    }
}
