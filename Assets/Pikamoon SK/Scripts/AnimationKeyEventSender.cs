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
                Debug.Log("AnimationKeyEventSender Animator Move");

                Vector3 velocity = AC.PAnimator.deltaPosition;

                Controller.Move(velocity, 20);
            }
        }
    }

}