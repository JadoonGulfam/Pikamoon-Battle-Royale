using System.Collections;
using UnityEngine;
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

        //public Image comboStatusImage;
        //public Image AttackStatusImage;

        //public TextMeshProUGUI ActiveStateName;
        //public TextMeshProUGUI ActiveStateProgress;

        [Space]
        [Header("Animation")]
        [SerializeField] AnimatorOverrideController animatorOverrideController;

        [SerializeField] bool ReadyToAttack;

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
        [SerializeField] MeleeWeaponDataSO meleeWeapnonData;



        public override void Initialize()
        {
            base.Initialize();

            comboMoveCounter = 1;

            hitBehaviour = GetComponent<HitBehaviour>();

            SettingHashes();

            comboInputTimer = Time.time;

            ActiveWeapon = Controller.ActiveWeapon.Prefab as MeleeWeapon;

            playerInput.onAttack1_Clicked += DoHorizontalAttack;
            playerInput.onAttack2_Clicked += DoVerticalAttack;
        }
        public override void Initialize(Transform Root)
        {
            base.Initialize(Root);

            comboMoveCounter = 1;

            hitBehaviour = Root.GetComponent<HitBehaviour>();

            SettingHashes();

            comboInputTimer = Time.time;

            ActiveWeapon = Controller.ActiveWeapon.Prefab as MeleeWeapon;

            playerInput.onAttack1_Clicked += DoHorizontalAttack;
            playerInput.onAttack2_Clicked += DoVerticalAttack;
        }

        public override StateType GetStateType()
        {
            return StateType.Combat;
        }
        public void ActivateWeapon(Weapon _weapon)
        {
            ActiveWeapon = _weapon as MeleeWeapon;

            meleeWeapnonData = ActiveWeapon.GetWeaponDataAs<MeleeWeaponDataSO>();

            AC.ChangeOverrideController(meleeWeapnonData.AnimOC);
        }


        public void ActivatingFistNoWeapon(WeaponDataSO _weaponData)
        {
            AC.ChangeOverrideController(_weaponData.AnimOC);
        }


        public PlayerSetupForMultiplayer MP_Setup;
        private void Update()
        {
            //if (Controller.MP_Setup != null && !Controller.MP_Setup.isMinePlayer)
            //    return;

            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;



            //ActiveStateName.text = AC.PAnimator?.GetCurrentAnimatorStateInfo(0).shortNameHash.ToShortString();
            //ActiveStateProgress.text = AC.PAnimator?.GetCurrentAnimatorStateInfo(0).normalizedTime.ToString("f2");
        }

        void SettingHashes()
        {

        }

        public void DoHorizontalAttack()
        {
            if(Controller.InAir || Controller.IsSwimming)
                return; 

            if (Controller.ActiveWeapon.Data.Type == WeaponType.Melee || Controller.ActiveWeapon.Data.Type == WeaponType.None)
                Attack(CombatMoveType.Horizontal);
        }
        
        public void DoVerticalAttack()
        {
            if (Controller.InAir || Controller.IsSwimming)
                return;

            if (Controller.ActiveWeapon.Data.Type == WeaponType.Melee || Controller.ActiveWeapon.Data.Type == WeaponType.None)
                Attack(CombatMoveType.Vertical);
        }

        void Attack(CombatMoveType combatMoveType)
        {
           
            if (!Controller.IsInAttack)
            {
                DoFirstAttack(combatMoveType);
            }
            else
            {
                if (ComboNextAttckTrigger)
                {
                    if(comboMoveCounter < meleeWeapnonData.MaxMovesInCombo)
                    {
                        ContinueComboAttack(combatMoveType);
                    }
                    //else
                    //{
                    //    DoFirstAttack(combatMoveType);
                    //}
                }
            }
        }

        public void PlayAttackSound()
        {
            SFX.PlayShootSound(ActiveWeapon.SwingSound);
        }

        void DoFirstAttack(CombatMoveType combatMoveType)
        {
            ComboNextAttckTrigger = false;
            Controller.IsInAttack = true;

            comboMoveCounter = 1;
            //AttackStatusImage.enabled = true;

            if (GetNearestEnemyToLock())
            {
                RotateTowardsNearestEnemy();
            }
            else
            {
                //playerController.RotateTowardsCameraForwardDirection(10);
            }


            AC.PAnimator.SetBool(AC.Parameters.inCombat.Hash, true);
            AC.PAnimator.SetInteger(AC.Parameters.ComboAttackType.Hash, (int)combatMoveType);


            StartAttack(combatMoveType);

            Controller.IsRootMotionEnabled = true;
            //if (combatCoroutine != null)
            //    StopCoroutine(combatCoroutine);
            //combatCoroutine = StartCoroutine(Attacking(combatMoveType));
        }

        void ContinueComboAttack(CombatMoveType combatMoveType)
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


            StartAttack(combatMoveType);

            Controller.IsRootMotionEnabled = true;
            //if (combatCoroutine != null)
            //    StopCoroutine(combatCoroutine);
            //combatCoroutine = StartCoroutine(Attacking(combatMoveType));
        }

        IEnumerator Attacking(CombatMoveType combatMoveType)
        {
            ToggleNextComboAttckStatus(false);

            
            // Enables Concenrned Hit Boxes for Attack 
            if (comboMoveCounter <= meleeWeapnonData.combos[(int)combatMoveType].moves.Length)
            {
                int _effectPointsLength = meleeWeapnonData.combos[(int)combatMoveType].moves[comboMoveCounter - 1].combatMoveEffectPoint.Length;
                
                for (int i = 0; i < _effectPointsLength; i++)
                {
                    if(meleeWeapnonData.combos[(int)combatMoveType].moves[comboMoveCounter - 1].combatMoveEffectPoint[i] != CombatMoveEffectPoint.Weapon)
                    {
                        hitBehaviour.EnableHitPoint(meleeWeapnonData.combos[(int)combatMoveType].moves[comboMoveCounter - 1].combatMoveEffectPoint[i]);
                    }
                    else
                    {
                    }
                }
            }


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


            //AnimatorStateInfo animatorStateInfo = AC.PAnimator.GetNextAnimatorStateInfo(0);

            //yield return new WaitUntil(() => animatorStateInfo.normalizedTime > .95f);


            //waiting to complete the transition, the wait time should be greater then transition time 
            yield return new WaitForSeconds(.1f);

            if(AC.PAnimator.IsInTransition(0))
            {
                yield return new WaitUntil(() => !AC.PAnimator.IsInTransition(0));
            }


            //Debug.LogError(AC.PAnimator.GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitUntil(() => AC.PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > .55f);

            ComboNextAttckTrigger = false;

            //yield return new WaitForSeconds(ComboCoolDownTime);
            //ComboEnd();

            Controller.IsRootMotionEnabled = false;

            yield return new WaitForSeconds(AttackCoolDownTime);
            AttackEnd();
        }

        void StartAttack(CombatMoveType combatMoveType)
        {
            ToggleNextComboAttckStatus(false);

            // Enables Concenrned Hit Boxes for Attack 
            if (comboMoveCounter <= meleeWeapnonData.combos[(int)combatMoveType].moves.Length)
            {
                if(ActiveWeapon == null)
                {
                    int _effectPointsLength = meleeWeapnonData.combos[(int)combatMoveType].moves[comboMoveCounter - 1].combatMoveEffectPoint.Length;
                    for (int i = 0; i < _effectPointsLength; i++)
                    {
                        hitBehaviour.EnableHitPoint(meleeWeapnonData.combos[(int)combatMoveType].moves[comboMoveCounter - 1].combatMoveEffectPoint[i]);
                    }
                }
                else if (ActiveWeapon.Type == WeaponType.Melee)
                {
                    ActiveWeapon.HitBox.Enable();
                }
            }


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

        void ComboEnd()
        {
            AC.PAnimator.SetInteger(AC.Parameters.AttackState.Hash, 1);
            comboMoveCounter = 1;
        }

        public void AttackEnd()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            if (ActiveWeapon != null && ActiveWeapon.Type == WeaponType.Melee)
            {
                ActiveWeapon.HitBox.Disable();
            }

            comboMoveCounter = 1;
            Controller.IsRootMotionEnabled = false;

            //AttackStatusImage.enabled = false;
            //Debug.LogError("Attack End");

            Controller.IsInAttack = false;

            AC.PAnimator.SetBool(AC.Parameters.inCombat.Hash, false);
            AC.PAnimator.SetBool(AC.Parameters.isWalkRun.Hash, playerInput.isMoving);
            AC.PAnimator.SetTrigger(AC.Parameters.EndCombat.Hash);
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

            direction.y = transform.position.y;

            transform.LookAt(direction);
        }

        void RotateTowardsNearestEnemy(Transform Target)
        {
            Controller.RotatePlayerTowardDirection(Target.position - this.transform.position, 50);
        }


        void GiveDamageToEnemy()
        {

        }

        //void OnAnimatorMove()
        //{
        //    Debug.Log("Animator Move");
        //    //if(Controller.IsInAttack && Controller.IsRootMotionEnabled)
        //    //{
        //        Vector3 velocity = AC.PAnimator.deltaPosition;

        //        Controller.Move(velocity, 20);
        //    //}
        //}

        public void ToggleNextComboAttckStatus(bool flag)
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;


            hitBehaviour.DisableAllHitPoints();
            ComboNextAttckTrigger = flag;
          
            //comboStatusImage.enabled = flag;
        }


        private void OnDestroy()
        {
            if (MP_Setup != null && !MP_Setup.isMinePlayer)
                return;

            playerInput.onAttack1_Clicked -= DoHorizontalAttack;
            playerInput.onAttack2_Clicked -= DoVerticalAttack;
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