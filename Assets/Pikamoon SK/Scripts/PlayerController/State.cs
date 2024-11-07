using UnityEngine;

namespace Pikamoon.Controller
{
    public abstract class State : MonoBehaviour
    {
        [HideInInspector] public PlayerController Controller;
        [HideInInspector] public PlayerInput playerInput;

        public virtual void Initialize()
        {
            Controller = GetComponent<PlayerController>();
            playerInput = ReferencesHolder.Instance._playerInput;
        }

        public abstract void OnEnd();

        public abstract void OnStart();

        public abstract void OnUpdate();
    }

}