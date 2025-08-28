using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PikamoonAiHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public GameObject Healths;
    public Transform StunParticle;
    [SerializeField] private PikamoonAi pikamoonAi;
    [SerializeField] Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;

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
        healthBar.fillAmount = currentHealth / maxHealth;     

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
        TakeDamage(hitter);
    }


    public void TakeDamage(Transform _attacker) // Function to reduce health
    {
        // pikamoonHealth.ReduceHealth(damage);
        if (IsDead()) 
        { 
            Die(); 
            return; 
        } // If Pikamoon's health is 0, trigger death

        StartCoroutine(StopMovementForHit());

    }
    private void Die()
    {
        Healths.gameObject.SetActive(false);
        animator.SetTrigger("Stunned"); // Play death animation
        StunParticle.gameObject.SetActive(true);
        //Destroy(gameObject, 2f); // Destroy after 3 seconds
    }
    private IEnumerator StopMovementForHit()
    {
        // Play hit animation
        animator.SetTrigger("Hit");
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(1); // Adjust delay as needed
        navMeshAgent.isStopped = false;
        animator.ResetTrigger("Hit");
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
