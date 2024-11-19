using UnityEngine;
namespace Pikamoon.Controller
{
    public class AnimationKeyEventSender : MonoBehaviour
    {
        Combat combat;
        Shooting shootingManager;
        Throwing _throwManager;

        private void Start()
        {
            combat = GetComponentInParent<Combat>();
            shootingManager = GetComponentInParent<Shooting>();
            _throwManager = GetComponentInParent<Throwing>();
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
    }

}