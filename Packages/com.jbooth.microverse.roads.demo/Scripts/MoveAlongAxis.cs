using UnityEngine;

namespace JBooth.MicroVerseCore.Roads.Demo
{
 
    public class MoveAlongAxis : MonoBehaviour
    {
        [SerializeField]
        private Vector3 moveXYZ = Vector3.zero;

        void Update()
        {
            transform.Translate(moveXYZ * Time.deltaTime);
        }
    }
}