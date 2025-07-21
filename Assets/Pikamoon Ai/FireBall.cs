using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 5f; // auto-destroy after time
    IDamageable damageable;
    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after x seconds if it doesn't hit
    }

    private void OnTriggerEnter(Collider other)
    {

        damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            //if (RootWeapon.Holder.transform == damageable.GetTransform())
            //    return;
            //else
                damageable.OnDamage(damage, this.transform);
        }
        //if (HitParticle)
        //{
        //    HitParticle.transform.parent = null;
        //    HitParticle.gameObject.SetActive(true);
        //}
        //RootWeapon.Holder._cameraController.DisableBulletActionCam();
        //rigidBody.linearVelocity = Vector3.zero;
        //rigidBody.isKinematic = true;
        //transform.position = transform.position + transform.forward.normalized;
        //_collider.enabled = false;


        // Check if the hit object is the player
        //if (other.CompareTag("Player"))
        //{
        //    // Apply damage to the player
        //    //PlayerHealth health = other.GetComponent<PlayerHealth>();
        //    //if (health != null)
        //    //{
        //    //    health.TakeDamage(damage);
        //    //}
        //    Debug.Log("hit player");
        //    // Destroy the fireball
        //    Destroy(gameObject);
        //}
        //else if (!other.isTrigger) // Optional: destroy if it hits a wall or non-trigger
        //{
        //    Destroy(gameObject);
        //}
    }
}
