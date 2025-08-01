using UnityEngine;

public class FootIK : MonoBehaviour
{
    private Animator animator;
    [SerializeField] CharacterController CC;
    [SerializeField] float MinVelocityForIK = 1f;
    [SerializeField] float SpeedToIK_Change = 2f;
    [Space]
    // Adjust these values to fit your character
    [SerializeField] private float footIKWeight = 1.0f;
    [SerializeField] private float footHeightOffset = 0.13f;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(!CC)
            CC = transform.parent.GetComponent<CharacterController>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            if (!CC)
                return;


            if (CC.velocity.magnitude > MinVelocityForIK)
            {
                if(footIKWeight > 0)
                {
                    footIKWeight -= Time.deltaTime * SpeedToIK_Change;
                }
            }
            else
            {
                if (footIKWeight < 1)
                {
                    footIKWeight += Time.deltaTime * SpeedToIK_Change;
                }
            }

            Mathf.Clamp01(footIKWeight);
            // Right foot IK
            Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
            RaycastHit hit;

            if (Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit, 1.5f, groundLayer))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += footHeightOffset;
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, footIKWeight);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);

                Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, footIKWeight);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
            }

            // Left foot IK - repeat the same process as for the right foot
            Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

            if (Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit, 1.5f, groundLayer))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += footHeightOffset;
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footIKWeight);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);

                Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footIKWeight);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, footRotation);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
            }
        }
    }
}
