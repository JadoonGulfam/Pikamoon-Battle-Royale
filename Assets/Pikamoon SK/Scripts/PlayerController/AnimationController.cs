using UnityEngine;
using Fusion;
using System.Linq;

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
        public ParamFloat SpeedMulForAnim;
        public ParamTrigger Equip;
        public ParamTrigger UnEquip;

    }

    [System.Serializable]
    public struct AnimationClipProperties
    {
        public string Name;
        public int Hash;
    }

    public class AnimationController : NetworkBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] AnimatorOverrideController[] animatorOverrideController;
        [Header("Parameters")]
        public AnimatorParameters Parameters;

        [Header("Hit Animation")]
        public AnimationClipProperties[] HitAnimations;
        public Animator PAnimator
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
            Parameters.SpeedMulForAnim.Hash = Animator.StringToHash(Parameters.SpeedMulForAnim.Name);
            Parameters.Equip.Hash = Animator.StringToHash(Parameters.Equip.Name);
            Parameters.UnEquip.Hash = Animator.StringToHash(Parameters.UnEquip.Name);
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
        
        
        public void ChangeOverrideController(AnimatorOverrideController overrideController)
        {
            if(Object == null)
            {
                PAnimator.runtimeAnimatorController = overrideController;
            }
            else
            {
                if (Object.HasStateAuthority)
                {
                    if (overrideController.name == "_NoWeapon_AOC")
                    {
                        RPC_ChangeOverrideContorller(0, Object.Id.ToString());
                        PAnimator.runtimeAnimatorController = overrideController;
                    }
                    else if (overrideController.name == "Melee_Sword_AOC")
                    {
                        RPC_ChangeOverrideContorller(1, Object.Id.ToString());
                        PAnimator.runtimeAnimatorController = overrideController;
                    }
                    else if (overrideController.name == "Melee_Mace_AOC")
                    {
                        RPC_ChangeOverrideContorller(2, Object.Id.ToString());
                        PAnimator.runtimeAnimatorController = overrideController;
                    }

                }
            }
        }

        public void callRPC_ChangeOverrideContorller(int controllerID)
        {
            if(Object.HasStateAuthority)
            {
                print("RPC called with value: " + controllerID + Object.Id);
                RPC_ChangeOverrideContorller(controllerID, Object.Id.ToString());
            }
            else
            {
              //  print("RPC not called with value: " + controllerID + Object.Id);
            }

        }


        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_ChangeOverrideContorller(int index, string playerID, RpcInfo info = default)
        {
            string numericOnly = new string(playerID.Where(char.IsDigit).ToArray());
            string trimmedID = numericOnly.Length >= 5
                ? numericOnly.Substring(0, 4) + numericOnly[^1]
                : numericOnly; 
           // print("PID"+Object.Id);
            //Debug.Log("TID"+trimmedID); 

            if (Object.Id.ToString() == playerID)
            {
                PAnimator.runtimeAnimatorController = animatorOverrideController[index];
                //Debug.Log("RPC called with value: " + index + playerID);
            }
            
        }

        public void SetAnimatorLayer(int index, float weight)
        {
            PAnimator.SetLayerWeight(index, weight);
        }
    }
}
