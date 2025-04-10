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