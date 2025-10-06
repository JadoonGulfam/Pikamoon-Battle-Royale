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

    public void CapturedSuccessfully(Transform _player)
    {
       
    }

    public bool onCapture(out CapturedInfo captureReturnInfo)
    {
        captureReturnInfo = capturedInfo;

        return isStunned;
    }

    public void onCaptureCancel()
    {
        
    }

    public void onCaptureStart()
    {
       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        capturedInfo = new CapturedInfo();

        capturedInfo.TimeToCapture = TimeToBeCaptured;
        capturedInfo.transform = this.transform;

    }

}
