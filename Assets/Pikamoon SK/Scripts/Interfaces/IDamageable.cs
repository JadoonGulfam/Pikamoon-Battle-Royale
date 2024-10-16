using UnityEngine;

public interface IDamageable
{
    void OnDamage();
    void OnDamage(float damageAmount);
    void OnDamage(float damageAmount, Transform hitter);
    bool isKilled();
}
