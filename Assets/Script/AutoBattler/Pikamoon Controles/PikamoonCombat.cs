using System.Collections;
using UnityEngine;

public class PikamoonCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackDamage = 10f;
    public float attackRange = 1.5f;
    public float manaRegenRate = 1f;
    private float currentMana;
    private bool isAttacking = false;
    private GameObject currentTarget;
    [SerializeField]
    private PikamoonAnimation animationController;

    private void Awake()
    {
       // animationController = GetComponent<PikamoonAnimation>();
    }

    private void Start()
    {
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
