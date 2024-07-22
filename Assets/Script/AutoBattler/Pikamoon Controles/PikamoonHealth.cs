using UnityEngine;

public class PikamoonHealth : MonoBehaviour
{
    [Header("Health Settings")]
    private float maxHealth;
    private float currentHealth;
    private PikamoonAnimation animationController;

    private void Awake()
    {
        animationController = GetComponent<PikamoonAnimation>();
    }

    public void Initialize(float hp)
    {
        maxHealth = hp;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animationController.SetAttackAnimation(false);
        animationController.SetDeathAnimation();
        // Additional death logic here
    }
}
