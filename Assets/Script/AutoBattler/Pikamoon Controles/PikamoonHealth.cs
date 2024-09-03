using UnityEngine;

public class PikamoonHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxMana;

    private float currentHealth;
    public float currentMana { get; private set; }
    private PikamoonBattleAnimationController animationController;
    private HealthManaBar healthManaBar;

    

    private void Awake()
    {
        animationController = GetComponent<PikamoonBattleAnimationController>();
        healthManaBar = GetComponent<HealthManaBar>();
    }

    public void Initialize(float hp, float mana)
    {
        print("hp" + hp);
        maxHealth = hp;
        currentHealth = maxHealth;
        maxMana = mana;
        currentMana = 0f;
    }

    public float GetHealthPercentage()
    {
        return Mathf.Clamp01(currentHealth / maxHealth);
    }

    public float GetManaPercentage()
    {
        return Mathf.Clamp01(currentMana / maxMana);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthManaBar.UpdateHealthBar(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        print("current health" + currentHealth);
    }

    private void Die()
    {
        animationController.PlayDeathAnimation();
        // Additional death logic here
    }
}
