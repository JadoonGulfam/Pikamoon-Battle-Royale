using UnityEngine;

public class PikamoonHandHitbox : MonoBehaviour
{
    [SerializeField] Transform Pikamoon;
    [SerializeField] float damage = 10f;
    public bool canDamage = false;
    IDamageable damageable;

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (Pikamoon.transform == damageable.GetTransform())
                return;
            else
                damageable.OnDamage(damage, this.transform);          
        }

    }
}
