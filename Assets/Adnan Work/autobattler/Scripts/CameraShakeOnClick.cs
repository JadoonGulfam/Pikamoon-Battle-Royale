using UnityEngine;

public class CameraShakeOnBombBlast : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 0.5f;    // Duration of the camera shake
    [SerializeField] private float shakeMagnitude = 0.5f;   // Intensity of the camera shake (bomb blast impact)
    [SerializeField] private float dampingSpeed = 3.0f;     // Speed at which the shake effect fades out
    [SerializeField] private float frequency = 30.0f;       // Frequency of the shake movement for a rapid effect

    private Vector3 initialPosition;   // The original position of the camera
    private float currentShakeTime = 0f;
    private float noiseSeedX;
    private float noiseSeedY;

    private void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.localPosition;

        // Generate random seeds for Perlin noise for X and Y axes
        noiseSeedX = Random.Range(0f, 100f);
        noiseSeedY = Random.Range(0f, 100f);
    }

    private void Update()
    {
        // Trigger shake (for testing purposes)
        if (Input.GetKeyDown(KeyCode.Space))  // Use spacebar to simulate bomb explosion
        {
            StartShake();
        }

        // Perform shaking effect if it's active
        if (currentShakeTime > 0)
        {
            // Get Perlin noise to create random and intense camera movements
            float shakeOffsetX = (Mathf.PerlinNoise(noiseSeedX, Time.time * frequency) - 0.5f) * shakeMagnitude;
            float shakeOffsetY = (Mathf.PerlinNoise(noiseSeedY, Time.time * frequency) - 0.5f) * shakeMagnitude;
            float shakeOffsetZ = (Mathf.PerlinNoise(noiseSeedX + noiseSeedY, Time.time * frequency) - 0.5f) * shakeMagnitude;

            // Apply the shake effect
            transform.localPosition = initialPosition + new Vector3(shakeOffsetX, shakeOffsetY, shakeOffsetZ);

            // Reduce the shake time rapidly for a bomb blast decay effect
            currentShakeTime -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // Reset camera position after shaking
            currentShakeTime = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * dampingSpeed);
        }
    }

    // Function to start the shake (simulate a bomb blast)
    public void StartShake()
    {
        currentShakeTime = shakeDuration;

        // Re-randomize seeds for different shake patterns with each explosion
        noiseSeedX = Random.Range(0f, 100f);
        noiseSeedY = Random.Range(0f, 100f);
    }
}
