using UnityEngine;

namespace Pikamoon.Controller
{
    public class HitPoint : MonoBehaviour
    {
        [SerializeField] CombatMoveEffectPoint effectPoint;
        public Collider collider;


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("1");
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                Debug.Log("2");
                damageable.OnDamage(10, this.transform);
            }
        }
    }

}