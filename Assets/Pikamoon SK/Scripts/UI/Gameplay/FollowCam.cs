using UnityEngine;
using DG.Tweening;
public class FollowCam : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;
    [Space]
    public Transform followTarget;      // This stays between A and B
    public Vector3 Offset;
    public Transform lookAtTransform;   // This will look at followTarget
    [Space]
    public float smoothSpeed = 5f;      // Set >0 for smooth motion

    [Space]
    public Vector3 MoveToPos;
    public int duration;
    void Update()
    {
        if (targetA == null || targetB == null || followTarget == null)
            return;

        // --- Position followTarget between targetA and targetB ---
        Vector3 midpoint = (targetA.position + targetB.position) * 0.5f;
        followTarget.position = Vector3.Lerp(followTarget.position, midpoint + Offset, Time.deltaTime * smoothSpeed);

        // --- Make lookAtTransform face followTarget ---
        if (lookAtTransform != null)
        {
            Vector3 direction = followTarget.position - lookAtTransform.position;
            if (direction.sqrMagnitude > 0.0001f) // avoid zero-length vector
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                lookAtTransform.rotation = Quaternion.Slerp(
                    lookAtTransform.rotation,
                    targetRotation,
                    Time.deltaTime * smoothSpeed
                );
            }
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayCinematic();
        }

    }

    void PlayCinematic()
    {
        this.transform.DOMove(MoveToPos, duration).SetEase(Ease.Linear);
    }




}
