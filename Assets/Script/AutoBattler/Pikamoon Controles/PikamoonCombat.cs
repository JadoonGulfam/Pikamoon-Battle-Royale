using System.Collections;
using UnityEngine;

public class PikamoonCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float manaRegenRate;

    private float currentMana;
    private bool isAttacking;
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
        currentMana = 0f;
    }

    public void StartCombat(GameObject target)
    {
        if (isAttacking || target == null) return;

        currentTarget = target;
       // StartCoroutine(CombatRoutine());
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
        if (currentTarget == null) return;

        var health = currentTarget.GetComponent<PikamoonHealth>();
        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
    }
}
