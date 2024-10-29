using UnityEngine;
using Photon.Voice.Unity;


public class VoiceDetectionIndicator : MonoBehaviour
{


    public GameObject voiceImageDetection;
    private Recorder recorder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(recorder == null)
            recorder=GameManager.instance.gameObject.transform.Find("Recorder").GetComponent<Recorder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (recorder.VoiceDetector.Detected)
        {
            Debug.Log("VoiceDetected");
            voiceImageDetection.SetActive(true);
        }
        else {
            Debug.Log("VoiceDetected Failed");
            voiceImageDetection.SetActive(false);
        }
    }
}
