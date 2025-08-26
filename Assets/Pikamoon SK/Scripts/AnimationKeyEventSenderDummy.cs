using UnityEngine;
namespace Pikamoon.Controller
{
    public class AnimationKeyEventSenderDummy : MonoBehaviour
    {
        [SerializeField] DummyEnemyController Controller;
        [SerializeField] AnimationController AC;


        public void DisableRootMotion()
        {
            Controller.IsRootMotionEnabled = false;
        }

        public void AllowCombo()
        {
            Controller.ToggleNextComboAttckStatus(true);
        }

        public void DenyComboInput()
        {
            Controller.ToggleNextComboAttckStatus(false);
            Controller.AttackEnd();
        }



        public void PlayShootSound()
        {
            //shootingManager.PlayAttackSound();
        }





        public void ChangeLayerIndex(int layerIndex, float weight)
        {
            AC.SetAnimatorLayer(layerIndex, weight);
        }

        public void ResetEquipSettings()
        {
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
                    velocity.y = Controller.JumpVelocity/10;
                }

                Controller.RootMove(velocity);

            }
        }
    }

}