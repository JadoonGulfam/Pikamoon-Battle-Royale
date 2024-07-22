using UnityEngine;

public class PikamoonAnimation : MonoBehaviour
{
    [Header("Animation Components")]
    private Animator animator;

    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem fireParticles;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAttackAnimation(bool isAttacking)
    {
        if (animator != null)
        {
            animator.SetBool("IsAttacking", isAttacking);
        }
    }

    public void SetDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        if (fireParticles != null)
        {
            fireParticles.Play();
        }
    }
}
