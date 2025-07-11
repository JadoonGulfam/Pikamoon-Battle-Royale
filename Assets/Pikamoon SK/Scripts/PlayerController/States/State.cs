using UnityEngine;

namespace Pikamoon.Controller
{
    public abstract class State : MonoBehaviour
    {
         public PlayerController Controller;
        [HideInInspector] public PlayerInput playerInput;
        [HideInInspector] public AnimationController AC;
        [HideInInspector] public SFXController SFX;

        public virtual void Initialize()
        {
            Controller = GetComponent<PlayerController>();
            playerInput = ReferencesHolder.Instance._playerInput;
            AC = GetComponent<AnimationController>();
            SFX = GetComponent<SFXController>();
        }

        public virtual void Initialize(Transform Root)
        {
            Controller = Root.GetComponent<PlayerController>();
            playerInput = ReferencesHolder.Instance._playerInput;
            AC  = Root.GetComponent<AnimationController>();
            SFX = Root.GetComponent<SFXController>();
        }

        public abstract StateType GetStateType();

        public abstract void OnStart();

        public abstract void OnUpdate();

        public abstract void OnEnd();

    }

}