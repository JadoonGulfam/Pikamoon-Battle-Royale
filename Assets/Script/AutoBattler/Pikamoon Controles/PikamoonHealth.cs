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
        print("Damage amount" + amount);
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die player died");
        animationController.SetAttackAnimation(false);
        animationController.SetDeathAnimation();
        // Additional death logic here
    }
}
