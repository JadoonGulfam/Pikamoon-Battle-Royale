using UnityEngine;

public class PikamoonHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;
    private PikamoonAnimation animationController;

    private void Awake()
    {
        currentHealth = maxHealth;
        animationController = GetComponent<PikamoonAnimation>();
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
