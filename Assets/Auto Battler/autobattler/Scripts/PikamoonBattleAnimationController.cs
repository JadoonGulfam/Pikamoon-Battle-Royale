using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _projectileCreationPoint;


    [SerializeField] private GameObject attack;
    private void Start()
    {
        PlayCastAnimation(2);
    }
    public void PlayCastAnimation(int animationNumber)
    {
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
        Instantiate(attack, _projectileCreationPoint.position, Quaternion.identity);
    }

    public void CastEmptyVisuals()
    {
        print("empty visuals");
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
