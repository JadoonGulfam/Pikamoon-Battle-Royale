using UnityEngine;

namespace Pikamoon.Controller
{
    public class HitPoint : MonoBehaviour
    {
        public Transform Attacker;
        [SerializeField] CombatMoveEffectPoint effectPoint;
        public Collider collider;

        public void EnableCollider()
        {
            collider.enabled = true;
        }

        public void DisableCollider()
        {
            collider.enabled = false;
        }



        private void OnTriggerEnter(Collider other)
        {
            print("hitpoint triger");
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                if (Attacker.transform != damageable.GetTransform())
                {
                    DisableCollider();
                    damageable.OnDamage(10, this.transform);
                    
                }
            }
        }
    }

}