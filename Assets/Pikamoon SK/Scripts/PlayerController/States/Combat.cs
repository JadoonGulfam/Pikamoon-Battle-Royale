using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace Pikamoon.Controller
{
    public enum CombatAction
    {
        Idle,
        Prepairing,
        InAttack,
        CoolingDown
    }

    [System.Serializable]
    public struct Combo
    {
        public CombatMoveType MoveType;
        public ComboMoveSO[] combatMoves;
    }

    public class Combat : State
    {
        MeleeWeapon ActiveWeapon;

        [Header("Movement")]
        [Space]
        public Transform DummyCircle;

        public Image comboStatusImage;
        public Image AttackStatusImage;

        public TextMeshProUGUI ActiveStateName;
        public TextMeshProUGUI ActiveStateProgress;

        [Space]
        [Header("Animation")]
        [SerializeField] AnimatorOverrideController animatorOverrideController;

        [SerializeField] bool ReadyToAttack;

        int DoNextComboAction_Hash;

        int Attack1_Hash;
        int Attack2_Hash;
        int Attack3_Hash;
        int Attack4_Hash;
        int idle_Hash;
        int AttackCooldown_Hash;



        [Header("EnemyRelated")]
        Collider[] EnemiesInRange;
        [SerializeField] float RadiusToFindEnemy;
        public LayerMask EnemyLayer;
        [SerializeField] float MaxDistanceToLoseLocking;
        [SerializeField] Transform lockedEnemy;


        [Space]
        [Header("Attack")]
        [SerializeField] float AttackCoolDownTime;

        [Space]


        [Space]
        [Header("Combo")]
        [SerializeField] float ComboCoolDownTime;
        bool inCombo;
        [SerializeField] int comboMoveCounter;
        float comboInputTimer;

        HitBehaviour hitBehaviour;
        Coroutine combatCoroutine;
        bool ComboNextAttckTrigger;
        CombatMoveType CurrentcomboType;
        [SerializeField] MeleeWeaponDataSO weapon;



        private void Start()
        {
            base.Initialize();

            comboMoveCounter = 1;

            hitBehaviour = GetComponent<HitBehaviour>();

            SettingHashes();

            comboInputTimer = Time.time;

            ActiveWeapon = Controller.ActiveWeapon.Prefab as MeleeWeapon;

            //playerInput.onAttack1_Clicked += DoHorizontalAttack;
            //playerInput.onAttack2_Clicked += DoVerticalAttack;
        }

        private void Update()
        {
            ActiveStateName.text = AC.PAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash.ToShortString();
            ActiveStateProgress.text = AC.PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime.ToString("f2");
        }


        void SettingHashes()
        {

            Attack1_Hash = Animator.StringToHash("Attack1");
            Attack2_Hash = Animator.StringToHash("Attack2");
            Attack3_Hash = Animator.StringToHash("Attack3");
            Attack4_Hash = Animator.StringToHash("Attack4");

            AttackCooldown_Hash = Animator.StringToHash("Attack_CoolDown");
        }

        public void DoHorizontalAttack()
        {
            if(Controller.ActiveWeapon.Data.Type == WeaponType.Melee)
                Attack(CombatMoveType.Horizontal);
        }
        public void DoVerticalAttack()
        {
            if (Controller.ActiveWeapon.Data.Type == WeaponType.Melee)
                Attack(CombatMoveType.Vertical);
        }

        void Attack(CombatMoveType combatMoveType)
        {
            if (!Controller.IsInAttack)
            {
                StartAttack(combatMoveType);
            }
            else
            {
                if (ComboNextAttckTrigger)
                {
                    if(comboMoveCounter < weapon.MaxMovesInCombo)
                    {
                        KeepAttacking(combatMoveType);
                    }
                    else
                    {
                        StartAttack(combatMoveType);
                    }
                }
            }
        }



        void StartAttack(CombatMoveType combatMoveType)
        {
            CurrentcomboType = combatMoveType;
            ComboNextAttckTrigger = false;
            Controller.IsInAttack = true;

            comboMoveCounter = 1;
            AttackStatusImage.enabled = true;



            if (GetNearestEnemyToLock())
            {
                RotateTowardsNearestEnemy();
            }
            else
            {
                //playerController.RotateTowardsCameraForwardDirection(10);
            }


            AC.PAnimator.SetInteger(AC.Parameters.ComboAttackType.Hash, (int)combatMoveType);
            AC.PAnimator.SetBool(AC.Parameters.inCombat.Hash, true);

            if (combatCoroutine != null)
                StopCoroutine(combatCoroutine);
            combatCoroutine = StartCoroutine(StartAttacking(combatMoveType));
        }

        void KeepAttacking(CombatMoveType combatMoveType)
        {
            comboMoveCounter++;

            ToggleNextComboAttckStatus(false);
            //if(comboMoveCounter>weapon.MaxMovesInCombo)
            //{
            //    comboMoveCounter = 1;
            //}

            if (GetNearestEnemyToLock())
            {
                RotateTowardsNearestEnemy();
            }
            else
            {
                //playerController.RotateTowardsCameraForwardDirection(10);
            }


            if (combatCoroutine != null)
                StopCoroutine(combatCoroutine);
            combatCoroutine = StartCoroutine(StartAttacking(combatMoveType));
        }



        IEnumerator StartAttacking(CombatMoveType combatMoveType)
        {
            ToggleNextComboAttckStatus(false);

            DecideAttackAccordingToInputQueue(combatMoveType);

            if (comboMoveCounter <= weapon.combos[(int)combatMoveType].moves.Length)
            {
                int looplength = weapon.combos[(int)combatMoveType].moves[comboMoveCounter-1].combatMoveEffectPoint.Length;
                for (int i = 0; i < looplength; i++)
                {
                    hitBehaviour.EnableHitPoint(weapon.combos[(int)combatMoveType].moves[comboMoveCounter - 1].combatMoveEffectPoint[i]);
                }
            }

            AC.PAnimator.SetInteger(AC.Parameters.ComboAttackType.Hash, (int)combatMoveType);
            
            if (comboMoveCounter == 1)
            {
                AC.PAnimator.SetInteger(AC.Parameters.AttackState.Hash, comboMoveCounter);
            }
            else
            {
                AC.PAnimator.SetInteger(AC.Parameters.AttackState.Hash, 0);

                AC.PAnimator.SetTrigger(AC.Parameters.NexComboAttack.Hash);
            }

            yield return new WaitUntil(() => AC.PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > .95f);

            ComboNextAttckTrigger = false;

            yield return new WaitForSeconds(ComboCoolDownTime);
            ComboEnd();


            yield return new WaitForSeconds(AttackCoolDownTime);
            AttackEnd();
        }




        void DecideAttackAccordingToInputQueue(CombatMoveType combatMoveType)
        {
            //playerAC.PAnimator.runtimeAnimatorController = weapon.combos[(int)combatMoveType].AnimOC;
        }

        void ComboEnd()
        {
            AC.PAnimator.SetInteger(AC.Parameters.AttackState.Hash, 1);
            comboMoveCounter = 1;
        }
        void AttackEnd()
        {
            AttackStatusImage.enabled = false;

            Controller.IsInAttack = false;

            AC.PAnimator.SetBool(AC.Parameters.inCombat.Hash, false);
        }

        bool GetNearestEnemyToLock()
        {
            EnemiesInRange = Physics.OverlapSphere(this.transform.position, RadiusToFindEnemy, EnemyLayer);

            // this is because player is also of same layermask as enemy,
            // thats why player is also considering himself enemy
            if (EnemiesInRange.Length < 2)
                return false;

            float dis = 1000;
            int indexOfNearest = 0;

            for (int i = 0; i < EnemiesInRange.Length; i++)
            {
                if (EnemiesInRange[i].transform != this.transform)
                {
                    float distance = Vector3.Distance(EnemiesInRange[i].transform.position, this.transform.position);
                    if (dis > distance)
                    {
                        dis = distance;
                        indexOfNearest = i;
                    }
                }
            }

            lockedEnemy = EnemiesInRange[indexOfNearest].transform;

            if (indexOfNearest == -1)
                return false;


            return true;
        }

        void RotateTowardsNearestEnemy()
        {
            Vector3 direction = (transform.localPosition + lockedEnemy.localPosition) / 2;

            //direction.y = transform.position.y;

            direction.y = this.transform.position.y;


            transform.LookAt(direction);
        }

        void RotateTowardsNearestEnemy(Transform Target)
        {
            Controller.RotatePlayerTowardDirection(Target.position - this.transform.position, 50);
        }

        void GiveDamageToEnemy()
        {

        }

        public void ToggleNextComboAttckStatus(bool flag)
        {

            hitBehaviour.DisableAllHitPoints();
            ComboNextAttckTrigger = flag;

            comboStatusImage.enabled = flag;
        }


        private void OnDestroy()
        {
            //playerInput.onAttack1_Clicked -= DoHorizontalAttack;
            //playerInput.onAttack2_Clicked -= DoVerticalAttack;
        }

        public override void OnEnd()
        {
        }

        public override void OnStart()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}