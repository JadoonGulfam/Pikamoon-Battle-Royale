using UnityEngine;
using Pikamoon.Controller;
public class TempPikamoonCapture : MonoBehaviour, ICapturable
{
    [SerializeField] float TimeToBeCaptured;
    [SerializeField] bool isStunned;

    CapturedInfo capturedInfo;
    public bool isReadyToBeCaptured { 
        get
        {
            return isStunned;   
        }
        set
        {
            isStunned = value;
        }
    }

    public bool isInRangeToCapture(out CapturedInfo captureReturnInfo)
    {
        captureReturnInfo = capturedInfo;

        return isStunned;
    }

    public void OnCapture()
    {
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        capturedInfo = new CapturedInfo();

        capturedInfo.TimeToCapture = TimeToBeCaptured;
        capturedInfo.transform = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
