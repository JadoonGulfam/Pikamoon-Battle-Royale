using UnityEngine;

public class CameraOrbitWithSineWave : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;            // The target object to look at and orbit around
    [SerializeField] private float orbitDistance = 10f;   // Distance from the camera to the target
    [SerializeField] private float cameraHeight = 5f;     // Base height for the camera (fixed Y position without sine wave)

    [Header("Orbit Settings")]
    [SerializeField] private float orbitDuration = 20f;   // Time (in seconds) for one complete orbit around the target
    [SerializeField] private float orbitSpeedMultiplier = 1f;   // Adjust this multiplier to slow down or speed up the orbit

    [Header("Sine Wave Settings")]
    [SerializeField] private float sineWaveAmplitude = 2f; // Amplitude of the sine wave
    [SerializeField] private float sineWaveFrequency = 1f; // Frequency of the sine wave (how many cycles per second)

    [Header("Smoothing Settings")]
    [SerializeField] private float positionLerpSpeed = 5f; // Speed of position interpolation for smoother movement
    [SerializeField] private float rotationLerpSpeed = 5f; // Speed of rotation interpolation for smoother rotation

    private float orbitSpeed;      // Calculated speed to complete orbit in specified time
    private float angle;           // Current angle of rotation
    private float time;            // Time variable for sine wave calculation

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned to CameraOrbitWithSineWave script.");
            return;
        }

        // Calculate the orbit speed based on the duration (degrees per second), then adjust by multiplier
        orbitSpeed = (360f / orbitDuration) * orbitSpeedMultiplier;
        time = 0f; // Initialize time
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Increment the angle over time, ensuring smooth movement
        angle += orbitSpeed * Time.deltaTime;
        if (angle > 360f)
        {
            angle -= 360f;
        }

        // Calculate the new position for the camera based on the current angle
        float posX = target.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * orbitDistance;
        float posZ = target.position.z + Mathf.Cos(angle * Mathf.Deg2Rad) * orbitDistance;

        // Calculate the sine wave motion
        float sineWaveOffset = Mathf.Sin(time * sineWaveFrequency) * sineWaveAmplitude;
        time += Time.deltaTime;

        // Update camera's position with the sine wave offset
        Vector3 targetPosition = new Vector3(posX, target.position.y + cameraHeight + sineWaveOffset, posZ);

        // Smoothly move the camera to the new position
        transform.position = Vector3.Lerp(transform.position, targetPosition, positionLerpSpeed * Time.deltaTime);

        // Smoothly rotate the camera to look at the target
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationLerpSpeed * Time.deltaTime);
    }
}
