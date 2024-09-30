using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyesBlinking : MonoBehaviour
{
    public SkinnedMeshRenderer blendHolder;
    public List<EyeBlendShape> AllEyeBlendShapes = new List<EyeBlendShape>();

    [HideInInspector]
    public float blinkingRate;
    [HideInInspector]
    public bool isEyeClose = false;
    [Range(2f, 3f)]
    public float waitTime;
    [Range(0.1f, 0.2f)]
    public float blinkingSpeed;
    [HideInInspector]
    public bool isBlinking = true;

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
        for (int i = 0; i < blendHolder.sharedMesh.blendShapeCount; i++)   // Get all the blend shapes that are related to eyes
        {
            string blendShapeName = blendHolder.sharedMesh.GetBlendShapeName(i);
            if (blendShapeName.Contains("eye"))
            {
                AllEyeBlendShapes.Add(new EyeBlendShape { index = i });
            }
        }

        waitTime = UnityEngine.Random.Range(2f, 3f);            // Randomize the wait time
        blinkingSpeed = UnityEngine.Random.Range(0.1f, 0.2f);   // Randomize the blinking speed
    }
    public void StoreBlendShapeValues()
    {
        if (blendHolder != null)
        {
            isBlinking = false;
            blendHolder.SetBlendShapeWeight(8, 0);
            blendHolder.SetBlendShapeWeight(9, 0);

            for (int i = 0; i < AllEyeBlendShapes.Count; i++)
            {
                AllEyeBlendShapes[i].value = blendHolder.GetBlendShapeWeight(AllEyeBlendShapes[i].index);
            }
            isBlinking = true;
        }
    }

    public IEnumerator BlinkingStartRoutine()
    {
        if (isBlinking && AllEyeBlendShapes.Count != 0)  
        {
            while (isEyeClose)
            {
                for (int i = 0; i < AllEyeBlendShapes.Count && AllEyeBlendShapes[i].value != 8 && AllEyeBlendShapes[i].value != 9; i++)
                {
                    blendHolder.SetBlendShapeWeight(i, Mathf.Lerp(blendHolder.GetBlendShapeWeight(i), 0, Time.deltaTime * blinkingRate));
                }
                currentBlendWeight += Time.deltaTime * blinkingRate;
                if (currentBlendWeight >= 100f)
                {
                    currentBlendWeight = 100f;
                    isEyeClose = false;
                }

                blendHolder.SetBlendShapeWeight(8, currentBlendWeight);  // Set the blend shape weight for the left eye
                blendHolder.SetBlendShapeWeight(9, currentBlendWeight);  // Set the blend shape weight for the right eye
            }
            yield return new WaitForSeconds(blinkingSpeed);
            while (!isEyeClose)
            {
                currentBlendWeight -= Time.deltaTime * blinkingRate;
                if (currentBlendWeight <= 0f)
                {
                    currentBlendWeight = 0f;
                    isEyeClose = true;
                }
                for (int i = 0; i < AllEyeBlendShapes.Count; i++)
                {
                    blendHolder.SetBlendShapeWeight(AllEyeBlendShapes[i].index, currentBlendWeight);
                }
            }
            isCoroutineRunning = true;
            yield return new WaitForSeconds(waitTime);
            if (gameObject.activeInHierarchy)
                StartCoroutine(BlinkingStartRoutine());
        }
        isCoroutineRunning = false;
    }
    private void OnDisable()
    {
        isCoroutineRunning = false;
    }
}