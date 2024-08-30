using System.Collections;
using UnityEngine;

public class PikamoonBattleAnimationController : MonoBehaviour
{
    private const string Idle = "Idle";
    private const string Attack = "Attack";
    private const string Cast = "Cast";
    private const string Knockback = "Knockback";
    private const string Knockdown = "Knockdown";
    private const string VICTORY = "Victory";
    private const string Death = "Death";
    private const string CAST_ANIM_ID = "CastAnim";
    private string _stateName;

    [Header("Component References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _projectileCreationPoint;
    private AttackVisualPooler attackPooler;  // Reference to AttackVisualPooler

    [Header("Attack Configuration")]
    [SerializeField] private string attackVisualTag;

    public PikamoonController pikamoonController;
     
    public void SetAttackVisualPooler(AttackVisualPooler pooler)
    {
        attackPooler = pooler;
    }
    public void StartAttack()
    {
        StartCoroutine(StartAttacking());
    }

    private IEnumerator StartAttacking()
    {
        Debug.Log("Starting attack sequence");
        PlayCastAnimation(2);

        // Wait for a random time between 1 and 4 seconds
        float randomWaitTime = Random.Range(2f, 4f);
        yield return new WaitForSeconds(randomWaitTime);

        StartCoroutine(StartAttacking());
    }

    public void PlayCastAnimation(int animationNumber)
    {
        Debug.Log("Casting attack animation");
        switch (animationNumber)
        {
            case 0:
                _stateName = Attack;
                break;
            case 1:
                _stateName = Cast;
                break;
        }

        ResetTriggers();

        _animator.SetInteger(CAST_ANIM_ID, animationNumber);
        _animator.SetTrigger(Cast);
    }
    
    public void castAttackVisuls()
    {
        GameObject attackObject = attackPooler.GetPooledObject(attackVisualTag);
        if (attackObject != null)
        {
            
            attackObject.transform.position = _projectileCreationPoint.position;
            attackObject.transform.rotation = Quaternion.identity;

            if (pikamoonController.isAIPikamoon)
            {
                // Rotate by 180 degrees on the Y-axis if this is an AI Pikamoon
                attackObject.transform.Rotate(0, 180, 0);
              
            }
            else
            {
                attackObject.transform.Rotate(0, 0, 0);
            }
            attackObject.GetComponent<AttackParticles>().Initialize(pikamoonController.targetPosition, 5, pikamoonController.isAIPikamoon, 1);                        
            attackObject.SetActive(true);
        }
    }


    public void castStartAttackVisuls()
    {
        print("castStartAttackVisuls");
    }

    public void PlayVictoryAnimation()
    {
        ResetTriggers();
        _animator.SetTrigger(VICTORY);
    }

    public void PlayIdleAnimation()
    {
        ResetTriggers();
        _animator.SetTrigger(Idle);
    }

    public void PlayHitAnimation()
    {
        ResetTriggers();
        if (Random.Range(0, 100) >= 50)
        {
            PlayKnockdownAnimation();
        }
        else
        {
            PlayKnockbackAnimation();
        }
    }

    private void PlayKnockdownAnimation()
    {
        _animator.SetTrigger(Knockdown);
    }

    private void PlayKnockbackAnimation()
    {
        _animator.SetTrigger(Knockback);
    }

    public void PlayDeathAnimation()
    {
        ResetTriggers();
        _animator.SetTrigger(Death);
    }

    private void ResetTriggers()
    {
        _animator.ResetTrigger(Knockback);
        _animator.ResetTrigger(Knockdown);
    }
}
