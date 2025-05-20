using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyesBlinking : MonoBehaviour
{
    public SkinnedMeshRenderer blendHolder;
    public List<EyeBlendShape> AllEyeBlendShapes = new List<EyeBlendShape>();

    public float blinkingRate;
    private bool isEyeClose = false;
    [Range(2f, 3f)]
    public float waitTime;
    [Range(0.1f, 0.2f)]
    public float blinkingSpeed;
    private bool isBlinking = true;

    public bool isCoroutineRunning = true;
    private float currentBlendWeight = 0f;

    [Serializable]
    public class EyeBlendShape
    {
        public int index;
        public float value;
    }

    private void Awake()
    {
        waitTime = UnityEngine.Random.Range(2f, 3f);            // Randomize the wait time
        blinkingSpeed = UnityEngine.Random.Range(0.1f, 0.2f);   // Randomize the blinking speed
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        _ = StartCoroutine(BlinkingStartRoutine());
    }

    public IEnumerator BlinkingStartRoutine()
    {
        if (isBlinking && AllEyeBlendShapes.Count != 0)
        {
            while (isBlinking)
            {
                // Close eyes
                yield return StartCoroutine(BlinkEyes(100f));

                // Wait briefly with eyes closed
                yield return new WaitForSeconds(blinkingSpeed);

                // Open eyes
                yield return StartCoroutine(BlinkEyes(0f));

                // Wait before next blink
                yield return new WaitForSeconds(waitTime);
            }
        }
        isCoroutineRunning = false;
    }

    private IEnumerator BlinkEyes(float targetBlendWeight)
    {
        while (!Mathf.Approximately(currentBlendWeight, targetBlendWeight))
        {
            // Smoothly adjust the blend weight towards the target
            currentBlendWeight = Mathf.MoveTowards(currentBlendWeight, targetBlendWeight, blinkingRate * Time.deltaTime * 100f);

            // Update each eye blend shape
            for (int i = 0; i < AllEyeBlendShapes.Count; i++)
            {
                blendHolder.SetBlendShapeWeight(AllEyeBlendShapes[i].index, currentBlendWeight);
            }

            yield return null; // Yield to allow frame update
        }
    }

    private void OnDisable()
    {
        isCoroutineRunning = false;
    }
}