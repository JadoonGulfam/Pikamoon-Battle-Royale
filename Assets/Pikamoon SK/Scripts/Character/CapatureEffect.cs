using Pikamoon.Controller;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class CapatureEffect : MonoBehaviour, ICapturable
{
    [SerializeField]
    private PikamoonAi pikamoonAi;

    [Header("Capture Info")]
    [SerializeField] float TimeToBeCaptured;

    CapturedInfo capturedInfo;
   
    public bool isReadyToBeCaptured
    {
        get
        {
            return pikamoonAi.Stunned;
        }
        set
        {
            pikamoonAi.Stunned = value;
        }
    }


    [SerializeField] VisualEffect capture_new;
    public Material[] material;
    private void Awake()
    {
        for (int i = 0; i < material.Length; i++)
        material[i].SetFloat("_Cutoff", 4);

    }
    private void Start()
    {
        pikamoonAi = GetComponent<PikamoonAi>();
        capturedInfo = new CapturedInfo();

        capturedInfo.TimeToCapture = TimeToBeCaptured;
        capturedInfo.transform = this.transform;

    }
    private void OnDisable()
    {
        for (int i = 0; i < material.Length; i++)
            material[i].SetFloat("_Cutoff", 4);
    }
    Coroutine temp;
    public void Capture()
    {
        temp = StartCoroutine(capturePikamoon(this.gameObject));
    }
    IEnumerator capturePikamoon(GameObject pikamoon)
    {
        float duration = TimeToBeCaptured;
        float startValue = 4;
        float endValue = 0f; // Target value
        float stepSize = 0.1f; // Reduce by 0.1 at a time
        float totalSteps = (startValue - endValue) / stepSize; // Total steps required
        float delay = duration / totalSteps; // Delay between each step
        float currentValue = startValue;
        capture_new.Play();
        while (currentValue > endValue)
        {
            currentValue -= stepSize;
            for (int i = 0; i < material.Length; i++)
                material[i].SetFloat("_Cutoff", currentValue);
            yield return new WaitForSeconds(delay);
        }
        Debug.Log("Reduction complete! Final Value: " + currentValue);
    }

    public void ResetValues() 
    {
        Debug.Log("yaha aya ha bhai");
        if (temp != null)
        {
            StopCoroutine(temp);
            temp = null;
        }
        float endValue = 4f;
        for (int i = 0; i < material.Length; i++)
            material[i].SetFloat("_Cutoff", endValue);
    }
    //public IEnumerator SetPikamoonMaterial()
    //{
    //    // pikamoonRoaming.DisableRoaming();
    //    float duration = 1;
    //    float startValue = 0f;
    //    float endValue = 4f;
    //    float stepSize = 0.1f;
    //    float totalSteps = (endValue - startValue) / stepSize; // Correct total steps calculation
    //    float delay = duration / totalSteps; // Delay per step
    //    float currentValue = startValue;
    //    while (currentValue < 4)
    //    {
    //        Debug.Log("Current Value: " + currentValue);
    //        for (int i = 0; i < material.Length; i++)
    //            material[i].SetFloat("_Cutoff", currentValue);
    //        currentValue = Mathf.Min(currentValue + stepSize, endValue); // Ensure it doesn't exceed endValue
    //        yield return new WaitForSeconds(delay);
    //    }
    //}


    public bool onCapture(out CapturedInfo captureReturnInfo)
    {
        captureReturnInfo = capturedInfo;
        
        return isReadyToBeCaptured && !pikamoonAi.isCaptured;  
    }
    public void CapturedSuccessfully(Transform _player)
    {
        pikamoonAi.CapturedByPlayer(_player);
        ResetValues();
    }

    public void onCaptureCancel()
    {
        ResetValues();
    }

    public void onCaptureStart()
    {
       // if(isReadyToBeCaptured && !pikamoonAi.isCaptured)
        Capture();
    }
}
