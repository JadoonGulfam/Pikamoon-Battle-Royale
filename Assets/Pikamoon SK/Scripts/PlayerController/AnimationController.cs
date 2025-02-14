using UnityEngine;


namespace Pikamoon.Controller
{
    [System.Serializable]
    public struct AnimatorParameters
    {
        [System.Serializable]
        public struct ParamFloat
        {
            public string Name;
            public float Value;
            [HideInInspector]
            public int Hash;

        }


        [System.Serializable]
        public struct ParamBool
        {
            public string Name;
            public bool Value;
            [HideInInspector]
            public int Hash;

        }


        [System.Serializable]
        public struct ParamInt
        {
            public string Name;
            public int Value;
            [HideInInspector]
            public int Hash;

        }


        [System.Serializable]
        public struct ParamTrigger
        {
            public string Name;
            public bool Value;
            [HideInInspector]
            public int Hash;

        }


        public ParamBool isWalkRun;
        public ParamBool inAir;
        public ParamBool isCrouch;
        public ParamBool inCombat;
        public ParamBool isSwim;
        public ParamInt AttackState;
        public ParamBool isSlide;
        public ParamFloat Speed;
        public ParamTrigger NexComboAttack;
        public ParamInt ComboAttackType;
        public ParamFloat XVal;
        public ParamFloat YVal;
        public ParamTrigger GetHit;
        public ParamTrigger Shoot;
        public ParamBool isAiming;
        public ParamInt SecondaryState;
        public ParamTrigger EndCombat;

    }
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] Animator animator;

        [Header("Parameters")]
        public AnimatorParameters Parameters;

        public  Animator PAnimator
        {
            get 
            {
                return animator;
            }
        }

        public void Awake()
        {
            MakeHashesForParameters();
        }

        void MakeHashesForParameters()
        {
            Parameters.isWalkRun.Hash = Animator.StringToHash(Parameters.isWalkRun.Name);
            Parameters.inAir.Hash = Animator.StringToHash(Parameters.inAir.Name);
            Parameters.isCrouch.Hash = Animator.StringToHash(Parameters.isCrouch.Name);
            Parameters.inCombat.Hash = Animator.StringToHash(Parameters.inCombat.Name);
            Parameters.isSwim.Hash = Animator.StringToHash(Parameters.isSwim.Name);
            Parameters.AttackState.Hash = Animator.StringToHash(Parameters.AttackState.Name);
            Parameters.isSlide.Hash = Animator.StringToHash(Parameters.isSlide.Name);
            Parameters.Speed.Hash = Animator.StringToHash(Parameters.Speed.Name);
            Parameters.NexComboAttack.Hash = Animator.StringToHash(Parameters.NexComboAttack.Name);
            Parameters.ComboAttackType.Hash = Animator.StringToHash(Parameters.ComboAttackType.Name);
            Parameters.XVal.Hash = Animator.StringToHash(Parameters.XVal.Name);
            Parameters.YVal.Hash = Animator.StringToHash(Parameters.YVal.Name);
            Parameters.GetHit.Hash = Animator.StringToHash(Parameters.GetHit.Name);
            Parameters.Shoot.Hash = Animator.StringToHash(Parameters.Shoot.Name);
            Parameters.isAiming.Hash = Animator.StringToHash(Parameters.isAiming.Name);
            Parameters.SecondaryState.Hash = Animator.StringToHash(Parameters.SecondaryState.Name);
            Parameters.EndCombat.Hash = Animator.StringToHash(Parameters.EndCombat.Name);
        }

        public void SetAnimationState(string stateName, float transitionDuration = 0.1f)
        {
            if (animator.HasState(0, Animator.StringToHash(stateName)))
                animator.CrossFadeInFixedTime(stateName, transitionDuration, 0);
        }
        public void SetAnimationState(int stateHash, float transitionDuration = 0.1f)
        {
            if (animator.HasState(0, stateHash))
                animator.CrossFadeInFixedTime(stateHash, transitionDuration, 0);
        }

    }
}
