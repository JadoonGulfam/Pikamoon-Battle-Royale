using UnityEngine;
using UnityEngine.UI;

public class PikamoonAiHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth/ maxHealth;
    }

    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth/ maxHealth;
        Debug.Log("Pikamoon Health: " + currentHealth);

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
}
