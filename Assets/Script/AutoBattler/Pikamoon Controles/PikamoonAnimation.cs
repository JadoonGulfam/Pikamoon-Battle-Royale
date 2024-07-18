using UnityEngine;

public class PikamoonAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    private Animator animator;
    [SerializeField] private ParticleSystem fireParticles;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError($"Animator component missing on {gameObject.name}. Please add an Animator component.");
        }
    }

    public void SetAttackAnimation(bool isAttacking)
    {
        if (animator == null) return;

        if (isAttacking)
        {
            animator.SetTrigger("Attack");
            if (fireParticles != null)
            {
                fireParticles.Play();
            }
        }
        else
        {
            animator.SetTrigger("Idle");
            if (fireParticles != null)
            {
                fireParticles.Stop();
            }
        }
    }

    public void SetDeathAnimation()
    {
        if (animator == null) return;

        animator.SetTrigger("Die");
        if (fireParticles != null)
        {
            fireParticles.Stop();
        }
    }
}
