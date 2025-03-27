using System.Collections;
using UnityEngine;

public class FadeProperty : MonoBehaviour
{
    public Renderer objectRenderer; // Assign your object's renderer
    public Color emissionColor = Color.red; // Set your emission color
    public float fadeDuration = 1f; // Duration for fade-in/out


    private Material material;
    void Start()
    {
        // Get the material
        material = objectRenderer.material;

        // Enable emission property
        material.EnableKeyword("_EMISSIVE_COLOR_MAP");

        // Start the fade effect
        StartCoroutine(FadeEmission());
    }

    IEnumerator FadeEmission()
    {
        while (true) // Loop indefinitely for continuous effect
        {
            yield return StartCoroutine(FadeTo(100)); // Fade In
            yield return StartCoroutine(FadeTo(0)); // Fade Out
        }
    }

    IEnumerator FadeTo(float targetIntensity)
    {
        float elapsedTime = 0f;
        float startIntensity = material.GetColor("_EmissiveColor").maxColorComponent;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / fadeDuration);
            material.SetColor("_EmissiveColor", emissionColor * intensity);
            yield return null;
        }
    }
}
