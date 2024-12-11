using UnityEngine;

public class SmoothRotateDoor : MonoBehaviour
{
    public float rotationDuration = 2f; // Duration to complete the rotation
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float timeElapsed;

    void Start()
    {
        // Initialize the start rotation and the target rotation (90 degrees in the Y-axis)
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(startRotation.eulerAngles + new Vector3(0f,480, 0f)); 
    }

    void Update()
    {
        // Increment time elapsed
        timeElapsed += Time.deltaTime;

        // Easing function for smoother, more realistic rotation (SmoothStep)
        float lerpFactor = Mathf.SmoothStep(0f, 1f, timeElapsed / rotationDuration);

        // Smoothly rotate the object from start to target rotation
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, lerpFactor);

        // Stop the rotation once the time has passed
        if (timeElapsed >= rotationDuration)
        {
            enabled = false; // Disable the script once the rotation is done
        }
    }
}
