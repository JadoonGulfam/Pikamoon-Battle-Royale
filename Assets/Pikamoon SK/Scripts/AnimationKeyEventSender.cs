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

        private void OnAnimatorMove()
        {
            if (Controller.IsRootMotionEnabled)
            {
                Vector3 velocity = AC.PAnimator.deltaPosition;

                //Debug.Log("Velocity = " + velocity.y);

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