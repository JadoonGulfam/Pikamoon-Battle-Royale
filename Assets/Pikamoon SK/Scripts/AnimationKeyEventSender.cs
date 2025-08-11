using UnityEngine;
namespace Pikamoon.Controller
{
    public class AnimationKeyEventSender : MonoBehaviour
    {
        [SerializeField] Combat combat;
        [SerializeField] Shooting shootingManager;
        [SerializeField] Throwing _throwManager;
        [SerializeField] PlayerController Controller;
        [SerializeField] AnimationController AC;


        private void Start()
        {
            //combat = GetComponentInParent<Combat>();
            //shootingManager = GetComponentInParent<Shooting>();
            //_throwManager = GetComponentInParent<Throwing>();
        }

        void GiveImapact()
        {
        }

        public void AllowCombo()
        {
            combat.ToggleNextComboAttckStatus(true);
        }

        public void DenyComboInput()
        {
            combat.ToggleNextComboAttckStatus(false);
            combat.AttackEnd();
        }

        public void Shoot()
        {
            shootingManager.ShootArrow();
        }

        public void Throw()
        {
            _throwManager.ThrowFromAimPoint();
        }

        public void EquipWepon()
        {
            Controller.HandOverWeapon();
        }
        public void UnEquipWepon()
        {
            Controller.RestActiveWeapon();
        }

        public void PlayShootSound()
        {
            //shootingManager.PlayAttackSound();
        }

        public void PlayThrowSound()
        {
            _throwManager.ThrowFromAimPoint();
        }

        public void PlayCombatSound()
        {
            combat.PlayAttackSound();
        }

        public void DisableRootMotion()
        {
            Controller.IsRootMotionEnabled = false;
        }

        public void ChangeLayerIndex(int layerIndex, float weight)
        {
            AC.SetAnimatorLayer(layerIndex, weight);
        }

        public void ResetEquipSettings()
        {
            Controller.inventory.WeaponSwitchingComplete();
            AC.SetAnimatorLayer(3, 0);
        }

        private void OnAnimatorMove()
        {
            if (Controller.IsRootMotionEnabled)
            {
                Vector3 velocity = AC.PAnimator.deltaPosition;

                //apply velocity for straight Y attacks, because by not doing this during attack the player will float in air
                if (velocity.y >= -0.0005f && velocity.y <= 0.0005f)
                {
                    velocity.y = Controller.input.JumpVelocity/10;
                }

                Controller.RootMove(velocity);

            }
        }
    }

}