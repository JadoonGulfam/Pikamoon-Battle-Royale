using UnityEngine;

namespace Pikamoon.Controller
{
    public abstract class State : MonoBehaviour
    {
        [HideInInspector] public PlayerController Controller;
        [HideInInspector] public PlayerInput playerInput;
        [HideInInspector] public AnimationController AC;

        public virtual void Initialize()
        {
            Controller  = GetComponent<PlayerController>();
            playerInput = ReferencesHolder.Instance._playerInput;
            AC = GetComponent<AnimationController>();
        }

        public abstract void OnStart();

        public abstract void OnUpdate();

        public abstract void OnEnd();

    }

}