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
        animator?.SetBool("IsAttacking", isAttacking);
    }

    public void SetDeathAnimation()
    {
        animator?.SetTrigger("Die");
        fireParticles?.Play();
    }
}
