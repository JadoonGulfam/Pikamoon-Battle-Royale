using UnityEngine;

public class CheastBones : MonoBehaviour
{

    [Header("Breast Bones")]
    public Transform leftBreastBone;  // Assign the left breast bone in the Inspector
    public Transform rightBreastBone; // Assign the right breast bone in the Inspector

    [Header("Spring Settings")]
    public float springForce = 50f;       // The strength of the spring
    public float springDamping = 5f;     // Damping to smooth the motion
    public float maxDistance = 0.1f;     // Maximum allowed distance from the target

    private GameObject leftAnchor;       // Anchor point for the left breast
    private GameObject rightAnchor;      // Anchor point for the right breast

    void Start()
    {
        // Create invisible anchor objects
        if (leftBreastBone != null)
        {
            leftAnchor = CreateAnchor("LeftAnchor", leftBreastBone);
            AddSpringJoint(leftBreastBone, leftAnchor);
        }

        if (rightBreastBone != null)
        {
            rightAnchor = CreateAnchor("RightAnchor", rightBreastBone);
            AddSpringJoint(rightBreastBone, rightAnchor);
        }
    }

    void Update()
    {
        // Update anchor positions to follow parent motion
        if (leftAnchor != null)
            leftAnchor.transform.localPosition = leftBreastBone.localPosition;

        if (rightAnchor != null)
            rightAnchor.transform.localPosition = rightBreastBone.localPosition;
    }

    private GameObject CreateAnchor(string name, Transform bone)
    {
        GameObject anchor = new GameObject(name);
        anchor.transform.parent = bone.parent; // Keep it relative to the same parent
        anchor.transform.localPosition = bone.localPosition; // Match the bone's initial position
        anchor.transform.localRotation = bone.localRotation;
        return anchor;
    }

    private void AddSpringJoint(Transform bone, GameObject anchor)
    {
        SpringJoint spring = bone.gameObject.AddComponent<SpringJoint>();
        spring.connectedBody = anchor.GetComponent<Rigidbody>();
        spring.spring = springForce;
        spring.damper = springDamping;
        spring.maxDistance = maxDistance;
    }
}
