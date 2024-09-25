using UnityEngine;


public class Combat : MonoBehaviour
{
    [SerializeField] float RadiusToFindEnemy;
    [SerializeField] IDamageable lockedEnemy;
    [SerializeField] bool isInBattle;

    [Header("Animation")]
    [Space]
    [SerializeField] int Attack1_Hash;

    void SettingHashes()
    {
        Attack1_Hash = Animator.StringToHash("");
    }
    
    void SearchForEnemies()
    {

    }

    void GetNearestEnemy()
    {

    }

    void MoveTowardsNearestEnemy()
    {

    }

    void GiveDamageToEnemy()
    {

    }
}
