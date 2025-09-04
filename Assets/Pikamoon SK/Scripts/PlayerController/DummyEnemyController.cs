using DG.Tweening;
using Fusion;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Pikamoon.Controller
{
    public class DummyEnemyController : MonoBehaviour,IDamageable
    {
        AnimationController AC;
        SFXController SFX;
        [SerializeField] AudioClip damage;
        [SerializeField] HitPoint HitBox;
        public bool IsRootMotionEnabled;

        public float health;
        public float JumpVelocity;

        [SerializeField] Transform DummyAttacker;
        [SerializeField] bool AllowDummyInputHit;
        CharacterController characterController;

        [Header("UI")]
        public Image healthFiller;
        public Image ShieldFiller;


        private bool ComboNextAttckTrigger;
        private Collider[] EnemiesInRange;
        private Transform lockedEnemy;
        [Space]
        [SerializeField] float RadiusToFindEnemy;
        [SerializeField] LayerMask EnemyLayer;

        public int comboMoveCounter { get; private set; }

        public void Start()
        {
            AC = GetComponent<AnimationController>();
            characterController = GetComponent<CharacterController>();
            SFX = GetComponent<SFXController>();
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                DoHorizontalAttack();
            }
        }


        Vector2 GetHitDirection(Transform hit)
        {
            // Get the direction the enemy is facing
            Vector3 enemyForward = transform.forward;

            // Get the direction from the enemy to the attacker
            Vector3 directionToAttacker = (hit.position - transform.position).normalized;

            // Calculate the dot product between the enemy's forward direction and the direction to the attacker
            float dotProduct = Vector3.Dot(enemyForward, directionToAttacker);

            // Calculate the cross product to determine if the hit came from the left or right
            Vector3 crossProduct = Vector3.Cross(enemyForward, directionToAttacker);

            // Initialize XVal and YVal
            float XVal = 0f;
            float YVal = 1f;

            // Determine if the hit came from the front, back, left, or right and set XVal/YVal accordingly
            if (dotProduct > 0.5f)
            {
                // Hit from the front
                YVal = 1f;
                XVal = 0f;
            }
            else if (dotProduct < -0.5f)
            {
                // Hit from the back
                YVal = -1f;
                XVal = 0f;
            }
            else
            {
                // Hit from the sides
                if (crossProduct.y > 0)
                {
                    // Hit from the right
                    XVal = 1f;
                    YVal = 0f;
                }
                else
                {
                    // Hit from the left
                    XVal = -1f;
                    YVal = 0f;
                }
            }

            return new Vector2(XVal, YVal);

        }

        void GetHit(Transform hit)
        {
            // Get the direction the enemy is facing
            Vector3 enemyForward = transform.forward;

            // Get the direction from the enemy to the attacker
            Vector3 directionToAttacker = (hit.position - transform.position).normalized;

            // Calculate the dot product between the enemy's forward direction and the direction to the attacker
            float dotProduct = Vector3.Dot(enemyForward, directionToAttacker);

            // Calculate the cross product to determine if the hit came from the left or right
            Vector3 crossProduct = Vector3.Cross(enemyForward, directionToAttacker);

            // Initialize XVal and YVal
            float XVal = 0f;
            float YVal = 1f;

            // Determine if the hit came from the front, back, left, or right and set XVal/YVal accordingly
            if (dotProduct > 0.5f)
            {
                // Hit from the front
                XVal = 0f;
                YVal = 1f;
            }
            else if (dotProduct < -0.5f)
            {
                // Hit from the back
                XVal = 0f; 
                YVal = -1f;
            }
            else
            {
                // Hit from the sides
                if (crossProduct.y > 0)
                {
                    // Hit from the right
                    XVal = 1f;
                    YVal = 0f;
                }
                else
                {
                    // Hit from the left
                    XVal = -1f;
                    YVal = 0f;
                }
            }

            AC.PAnimator.SetFloat(AC.Parameters.XVal.Hash, XVal);
            AC.PAnimator.SetFloat(AC.Parameters.YVal.Hash, YVal);
            AC.PAnimator.CrossFadeInFixedTime(AC.Parameters.GetHit.Hash, 0.1f);

            SFX.PlayDamageSound(damage);

            //if (dotProduct > 0.5f)
            //{
            //    // Hit from the front

            //    AC.PAnimator.CrossFadeInFixedTime("Hit.Front", 0.1f);
            //}
            //else if (dotProduct < -0.5f)
            //{
            //    // Hit from the back

            //    AC.PAnimator.CrossFadeInFixedTime("Hit.Back", 0.1f);
            //}
            //else
            //{
            //    // Hit from the sides
            //    if (crossProduct.y > 0)
            //    {
            //        // Hit from the right

            //        AC.PAnimator.CrossFadeInFixedTime("Hit.Right", 0.1f);
            //    }
            //    else
            //    {
            //        // Hit from the left

            //        AC.PAnimator.CrossFadeInFixedTime("Hit.Left", 0.1f);
            //    }
            //}
        }

        public void RootMove(Vector3 direction)
        {
            characterController.Move(direction);
        }

        #region IDamageable Properties

        public float Health
        {
            get { return health; }
            private set { health = value; }
        }

        public bool isKilled()
        {
            if(health <= 0)
                return true;

            return false;
        }

        public void OnDamage()
        {
        }

        public void OnDamage(float damageAmount)
        {
        }

        public void OnDamage(float damageAmount, Transform hitPoint)
        {
            GetHit(DummyAttacker);


            health -= damageAmount;
            //healthFiller.DOFillAmount(health / 100, .1f);

            healthFiller.fillAmount = health / 100;

            //if (isKilled())
            //    this.gameObject.SetActive(false);

        }

        public Transform GetTransform()
        {
            return transform;
        }

        #endregion

        #region Combat System
        public void DoHorizontalAttack()
        {
            HitBox.EnableCollider();
            Debug.Log("Do Horizontal Attack");
            Attack(CombatMoveType.Horizontal);
        }
        public void ToggleNextComboAttckStatus(bool flag)
        {
            ComboNextAttckTrigger = flag;
        }
        public void AttackEnd()
        {
                
            AC.PAnimator.SetFloat(AC.Parameters.SpeedMulForAnim.Hash, 1);

            HitBox.DisableCollider();

            AC.PAnimator.SetBool(AC.Parameters.inCombat.Hash, false);
            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, false);
            AC.PAnimator.SetTrigger(AC.Parameters.EndCombat.Hash);
        }
        void Attack(CombatMoveType combatMoveType)
        {
            DoFirstAttack(combatMoveType);
        }
        public void DoVerticalAttack()
        {
            HitBox.EnableCollider();
            Attack(CombatMoveType.Vertical);
        }


        void DoFirstAttack(CombatMoveType combatMoveType)
        {
            ComboNextAttckTrigger = false;

            comboMoveCounter = 1;
            //AttackStatusImage.enabled = true;


            AC.PAnimator.SetFloat(AC.Parameters.SpeedMulForAnim.Hash, 20);
            AC.PAnimator.SetBool(AC.Parameters.inCombat.Hash, true);
            AC.PAnimator.SetInteger(AC.Parameters.ComboAttackType.Hash, (int)combatMoveType);


            StartAttack(combatMoveType);

            //if (combatCoroutine != null)
            //    StopCoroutine(combatCoroutine);
            //combatCoroutine = StartCoroutine(Attacking(combatMoveType));
        }


        void StartAttack(CombatMoveType combatMoveType)
        {

            //Choose Whether Nex Attack is Horizontal or Vertical and also play it
            AC.PAnimator.SetInteger(AC.Parameters.ComboAttackType.Hash, (int)combatMoveType);


            if (comboMoveCounter <= 1)
            {
                AC.PAnimator.SetInteger(AC.Parameters.AttackState.Hash, comboMoveCounter);
            }
            else
            {
                AC.PAnimator.SetInteger(AC.Parameters.AttackState.Hash, 0);

                AC.PAnimator.SetTrigger(AC.Parameters.NexComboAttack.Hash);
            }
        }
        #endregion
    }

}