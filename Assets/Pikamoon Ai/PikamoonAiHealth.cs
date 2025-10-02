using UnityEngine;
using UnityEngine.UI;
using Fusion;
using Pikamoon.Controller;
public class PikamoonAiHealth : NetworkBehaviour, IDamageable
{
    [Header("Health Settings")]
    public float maxHealth = 100f;

    [Networked]
    public float currentHealth { get; set; }

    [Header("UI")]
    public Image healthBar;

    [SerializeField]
    private PikamoonAi pikamoonAi;

    private ChangeDetector _changeDetector;

    public float Health
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            UpdateHealthUI();
        }
    }

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

        if (Object.HasStateAuthority)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            if (change == nameof(currentHealth))
            {
                UpdateHealthUI();
            }
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null && maxHealth > 0f)
            healthBar.fillAmount = currentHealth / maxHealth;
    }

    // Only StateAuthority applies the damage
    public void ReduceHealth(float amount)
    {
        if (!Object.HasStateAuthority) return;

        currentHealth -= amount;
        if (currentHealth <= 0) currentHealth = 0;
        UpdateHealthUI();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_RequestDamage(float amount, PlayerRef attacker)
    {
        ReduceHealth(amount);
        Debug.Log($"Damage {amount} requested by {attacker} and applied by authority.");
    }

    public bool IsDead() => currentHealth <= 0;

    public float GetHealthPercentage() => (currentHealth / maxHealth) * 100f;

    public void OnDamage() { }

    public void OnDamage(float damageAmount) =>
        RPC_RequestDamage(damageAmount, Runner.LocalPlayer);

    public void OnDamage(float damageAmount, Transform hitter)
    {
        RPC_RequestDamage(damageAmount, Runner.LocalPlayer);
        if (Object.HasStateAuthority)
            pikamoonAi?.TakeDamage(hitter);
    }

    public Transform GetTransform() => transform;

    public bool isKilled() => IsDead(); 

}
