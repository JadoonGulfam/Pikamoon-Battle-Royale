using UnityEngine;
namespace Pikamoon.Controller
{
    public class WeaponHitBoxExtension : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        public Collider _collider;

        public void EnableCollider()
        {
            _collider.enabled = true;
        }

        public void DisableCollider()
        {
            _collider.enabled = false;
        }

        public void AssignWeapon(Weapon _weapon)
        {
            weapon = _weapon;
        }
        public void UnAssignWeapon()
        {
            weapon = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                Debug.Log("Triggered Hitted Object is = " + damageable.GetTransform().name);

                if (weapon.Holder.transform != damageable.GetTransform())
                {
                    DisableCollider();
                    damageable.OnDamage(10, this.transform);
                    weapon.OnHit(Vector3.zero);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable damageable = collision.transform.GetComponent<IDamageable>();

            if (damageable != null)
            {
                Debug.Log("Collision Hitted Object is = " + damageable.GetTransform().name);

                if (weapon.Holder.transform != damageable.GetTransform())
                {
                    damageable.OnDamage(10, this.transform);
                    weapon.OnHit(collision.contacts[0].point);
                }
            }
        }
    }

}
