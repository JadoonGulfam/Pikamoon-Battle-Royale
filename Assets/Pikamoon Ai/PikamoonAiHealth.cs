using UnityEngine;
using UnityEngine.UI;

public class PikamoonAiHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    [SerializeField] private PikamoonAi pikamoonAi;
    public float Health 
    {
        get 
        {
            return currentHealth;
        }
        set 
        {
            currentHealth = value;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth/ maxHealth;
    }

    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth/ maxHealth;     

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public float GetHealthPercentage()
    {
        return (currentHealth / maxHealth) * 100f;
    }

    public void OnDamage()
    {
       
    }

    public void OnDamage(float damageAmount)
    {
        
    }

    public void OnDamage(float damageAmount, Transform hitter)
    {
        ReduceHealth(damageAmount);
        pikamoonAi.TakeDamage(hitter);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public bool isKilled()
    {
        return IsDead();
    }
}
