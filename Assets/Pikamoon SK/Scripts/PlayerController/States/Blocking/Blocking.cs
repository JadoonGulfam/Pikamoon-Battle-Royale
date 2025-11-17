using UnityEngine;
using System.Collections;

namespace Pikamoon.Controller
{
    public class Blocking : State
    {
        [Header("Block Settings")]
        [SerializeField] private float lightAttackDurabilityLoss = 5f;
        [SerializeField] private float heavyAttackDurabilityLoss = 15f;
        [SerializeField] private float blockCooldown = 0.5f;
        [SerializeField] private float blockDuration = 2f;

        [Header("References")]
        private MeleeWeapon activeWeapon;
        private MeleeWeaponDataSO weaponData;
        private bool isBlocking = false;
        private bool canBlock = true;

        public PlayerSetupForMultiplayer MP_Setup;

        public override void Initialize()
        {
            base.Initialize();
            activeWeapon = Controller.ActiveWeapon.Prefab as MeleeWeapon;
            weaponData = activeWeapon?.GetItemDataAs<MeleeWeaponDataSO>();

            // Bind player input
            playerInput.onBlock_Down += StartBlock;
            playerInput.onBlock_Up += StopBlock;
        }

        public override StateType GetStateType()
        {
            return StateType.Combat; // Or create a StateType.Blocking if you prefer
        }

        void StartBlock()
        {
            if (!canBlock || weaponData == null || activeWeapon.Health <= 0)
                return;

            isBlocking = true;
            //////AC.PAnimator.SetBool(AC.Parameters.isBlocking.Hash, true);
            // Optionally disable movement or reduce speed while blocking
        }

        void StopBlock()
        {
            if (!isBlocking)
                return;

            isBlocking = false;
            //////AC.PAnimator.SetBool(AC.Parameters.isBlocking.Hash, false);
            StartCoroutine(BlockCooldown());
        }

        IEnumerator BlockCooldown()
        {
            canBlock = false;
            yield return new WaitForSeconds(blockCooldown);
            canBlock = true;
        }

        // Called externally when the player is hit while blocking
        public void OnBlockedHit(bool isHeavyAttack)
        {
            if (!isBlocking || weaponData == null)
                return;

            float durabilityLoss = isHeavyAttack ? heavyAttackDurabilityLoss : lightAttackDurabilityLoss;
            activeWeapon.Health -= Mathf.CeilToInt(durabilityLoss);

            if (activeWeapon.Health <= 0)
                BreakWeapon();
        }

        void BreakWeapon()
        {
            activeWeapon.Health = 0;
            Debug.Log("Weapon broken!");

            // Disable blocking or attacks
            StopBlock();

            // Optionally play break animation / sound
            //////AC.PAnimator.SetTrigger(AC.Parameters.weaponBroken.Hash);

        }

        private void OnDestroy()
        {
            playerInput.onBlock_Down -= StartBlock;
            playerInput.onBlock_Up -= StopBlock;
        }

        public override void OnStart() { }
        public override void OnEnd() { }
        public override void OnUpdate()
        {
            // You could also allow a perfect block window here
        }
    }
}
