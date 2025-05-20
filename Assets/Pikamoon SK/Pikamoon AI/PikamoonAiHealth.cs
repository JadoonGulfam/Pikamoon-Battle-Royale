using UnityEngine;

public class PikamoonAiHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;
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
