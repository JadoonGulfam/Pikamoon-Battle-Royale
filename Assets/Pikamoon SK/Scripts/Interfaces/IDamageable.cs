using UnityEngine;

public interface IDamageable
{
    float Health { get; }

    void OnDamage();
    void OnDamage(float damageAmount);
    void OnDamage(float damageAmount, Transform hitter);
    bool isKilled();
}
