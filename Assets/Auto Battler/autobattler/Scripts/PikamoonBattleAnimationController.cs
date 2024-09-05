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
    public PikamoonHealth pikamoonHealth;
    private CharacterAudioManager audioManager;

    [SerializeField] private GameObject Deathparticales;
    private void Awake()
    {
        pikamoonController = this.gameObject.GetComponent<PikamoonController>();
        pikamoonHealth = this.gameObject.GetComponent<PikamoonHealth>();
        audioManager = GetComponent<CharacterAudioManager>();
    }
    public void SetAttackVisualPooler(AttackVisualPooler pooler)
    {
        attackPooler = pooler;
    }


    public void PikamoonDethVisuals()
    {
        Destroy(this.gameObject);
        print("pika moon die");
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
        float randomWaitTime = Random.Range(2f, 2.8f);
        yield return new WaitForSeconds(randomWaitTime);
        if(pikamoonController.isBattleStarted && pikamoonController.isPikamoonLive)
        {
            StartCoroutine(StartAttacking());
        }
        else if(!pikamoonController.isBattleStarted && pikamoonController.isPikamoonLive)
        {
            print("battle ended");
            PlayVictoryAnimation();
        }
            
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
            audioManager.PlayDeathSound();
            attackObject.SetActive(true);
        }
    }


    public void castStartAttackVisuls()
    {
        print("castStartAttackVisuls");
    }

    public void PlayVictoryAnimation()
    {
        audioManager.PlayVictorySound();
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
        pikamoonHealth.TakeDamage(100f);
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
        Deathparticales.SetActive(true);
        //audioManager.PlayDeathSound();
        pikamoonController.isPikamoonLive = false;
        ResetTriggers();
        _animator.SetTrigger(Death);
        //Destroy(this.gameObject,2.5f);
        print("charater die");
    }

    private void ResetTriggers()
    {
        _animator.ResetTrigger(Knockback);
        _animator.ResetTrigger(Knockdown);
    }
}
