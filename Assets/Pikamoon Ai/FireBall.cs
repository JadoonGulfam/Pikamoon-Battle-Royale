using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Transform pikamoon;
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
            if (pikamoon.transform == damageable.GetTransform())
                return;
            else
            damageable.OnDamage(damage, this.transform);
            Destroy(gameObject);
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
    }
}
