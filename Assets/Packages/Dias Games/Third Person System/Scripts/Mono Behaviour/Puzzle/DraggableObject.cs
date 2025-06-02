using UnityEngine;

namespace DiasGames.Puzzle
{
    public class DraggableObject : MonoBehaviour
    {
        private Rigidbody _rigidbody = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public virtual bool Move(Vector3 velocity)
        {
            velocity.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = velocity;

            return true;
        }

        public void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.linearVelocity = Vector3.zero;
        }

        public virtual void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }
}
