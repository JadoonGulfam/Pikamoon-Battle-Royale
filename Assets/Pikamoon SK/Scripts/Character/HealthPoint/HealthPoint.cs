using UnityEngine;
namespace Pikamoon.Controller
{

    public enum HealthPointType
    {
        Head,
        UpperBody,
        LowerBody
    }

    public class HealthPoint : MonoBehaviour, IDamageable
    {
        public HealthController player;
        public HealthPointType type;

        public float Health => player.Health;

        public Transform GetTransform()
        {
            return player.transform;
        }

        public bool isKilled()
        {
            return player.isKilled();
        }

        public void OnDamage()
        {
        }

        public void OnDamage(float damageAmount)
        {
        }

        public void OnDamage(float damageAmount, Transform hitter)
        {
            player.TakeDamage(type,damageAmount,hitter);
        }
    }
}