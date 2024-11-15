using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyesBlinking : MonoBehaviour
{
    public SkinnedMeshRenderer blendHolder;
    public List<EyeBlendShape> AllEyeBlendShapes = new List<EyeBlendShape>();

    //[HideInInspector]
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
        _=StartCoroutine(BlinkingStartRoutine());
    }
    public IEnumerator BlinkingStartRoutine()
    {
        if (isBlinking && AllEyeBlendShapes.Count != 0)  
        {
            while (isEyeClose)
            {
                currentBlendWeight += Time.deltaTime * blinkingRate;
                if (currentBlendWeight >= 100f)
                {
                    currentBlendWeight = 100f;
                    isEyeClose = false;
                }
                for (int i = 0; i < AllEyeBlendShapes.Count; i++)
                {
                    blendHolder.SetBlendShapeWeight(AllEyeBlendShapes[i].index, currentBlendWeight);// Mathf.Lerp(blendHolder.GetBlendShapeWeight(AllEyeBlendShapes[i].index), 0, Time.deltaTime * blinkingRate));
                }            
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
                _=StartCoroutine(BlinkingStartRoutine());
        }
        isCoroutineRunning = false;
    }
    private void OnDisable()
    {
        isCoroutineRunning = false;
    }
}