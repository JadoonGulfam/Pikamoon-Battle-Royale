using Unity.VisualScripting;
using UnityEngine;
namespace Pikamoon.Controller
{
    public class WeaponHitBox : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        public Collider _collider;

        public void Enable()
        {
            _collider.enabled = true;
        }

        public void Disable()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.OnDamage(10, this.transform);

                weapon.OnHit(Vector3.zero);
            }
        }
        //private void OnCollisionEnter(Collision collision)
        //{

        //    Debug.Log("1");

        //    IDamageable damageable = collision.transform.GetComponent<IDamageable>();

        //    if (damageable != null)
        //    {
        //        Debug.Log("2");

        //        damageable.OnDamage(10, this.transform);

        //        weapon.OnHit(collision.contacts[0].point);
        //    }
        //}

        private void OnCollisionEnter(Collision collision)
        {

            Debug.Log("1");

            IDamageable damageable = collision.transform.GetComponent<IDamageable>();

            if (damageable != null)
            {
                Debug.Log("2");

                damageable.OnDamage(10, this.transform);

                weapon.OnHit(collision.contacts[0].point);
            }
        }
    }

}
