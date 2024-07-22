using System.Collections;
using UnityEngine;

public class PikamoonCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    private float attackDamage;
    private float attackRange;
    private float manaRegenRate;

    private float currentMana;
    private bool isAttacking = false;
    private GameObject currentTarget;
    private PikamoonAnimation animationController;

    private void Awake()
    {
        animationController = GetComponent<PikamoonAnimation>();
    }

    public void Initialize(float attack, float range, float manaRegen)
    {
        attackDamage = attack;
        attackRange = range;
        manaRegenRate = manaRegen;
        currentMana = manaRegenRate;
    }

    public void StartCombat(GameObject target)
    {
        if (!isAttacking)
        {
            currentTarget = target;
            StartCoroutine(CombatRoutine());
        }
    }

    private IEnumerator CombatRoutine()
    {
        isAttacking = true;

        while (currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distance <= attackRange)
            {
                animationController.SetAttackAnimation(true);
                Attack();
                yield return new WaitForSeconds(1 / manaRegenRate);
                currentMana = 0;
            }
            else
            {
                animationController.SetAttackAnimation(false);
                yield return null;
            }

            currentMana += Time.deltaTime;
        }

        isAttacking = false;
    }

    private void Attack()
    {
        if (currentTarget != null)
        {
            var health = currentTarget.GetComponent<PikamoonHealth>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }
}
