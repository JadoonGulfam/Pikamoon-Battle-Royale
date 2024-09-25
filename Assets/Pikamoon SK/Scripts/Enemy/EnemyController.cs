using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IDamageable
{
    [SerializeField] float Health;


    public void OnDamage()
    {
    }

    public void OnDamage(float damageAmount)
    {
    }

    public bool isKilled()
    {
        return Health < 0;
    }
}
