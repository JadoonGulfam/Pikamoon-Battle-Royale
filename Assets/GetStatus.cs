using Photon.Voice.Unity;
using Photon.Voice.Unity.UtilityScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetStatus : MonoBehaviour
{
    [Header("Photon Voice References")]
    public UnityVoiceClient voiceClient;
    public ConnectAndJoin ConnectAndJoin;
    public Recorder Recorder;

    [Header("UI References")]
    public TMP_Text Status_text;
    public Button Mic_Mute;
    public Button Mic_UnMute;
  

    public string RoomName = "Pikamoon";

    private void Start()
    {
        // Add listeners to UI buttons
      

        if (Mic_Mute != null)
            Mic_Mute.onClick.AddListener(MuteMic);

        if (Mic_UnMute != null)
            Mic_UnMute.onClick.AddListener(UnMuteMic);

        // Register callbacks
        voiceClient.Client.StateChanged += OnVoiceStateChanged;

        // Set initial UI state
        UpdateUI(false);

        Invoke("ConnectToVoiceServer",2f); //  delay start
    }

    private void OnDestroy()
    {
        // Unregister callbacks
        if (voiceClient != null && voiceClient.Client != null)
        {
            voiceClient.Client.StateChanged -= OnVoiceStateChanged;
        }
    }

    private void ConnectToVoiceServer()
    {
        ConnectAndJoin.RoomName = RoomName;
        ConnectAndJoin.ConnectNow();
    }

    private void OnVoiceStateChanged(Photon.Realtime.ClientState fromState, Photon.Realtime.ClientState toState)
    {
        if (toState == Photon.Realtime.ClientState.Joined)
        {
            UpdateUI(true);
        }
        else if (toState == Photon.Realtime.ClientState.Disconnected)
        {
            UpdateUI(false);
        }
    }

    private void UpdateUI(bool connected)
    {
        Status_text.text = connected ? "Connected" : "Disconnected";

        Mic_Mute.gameObject.SetActive(connected);
        Mic_UnMute.gameObject.SetActive(false);

        if (!connected && Recorder != null)
        {
            Recorder.TransmitEnabled = false;
        }
    }

    private void MuteMic()
    {
        if (Recorder != null)
        {
            Recorder.TransmitEnabled = false;
            Mic_Mute.gameObject.SetActive(false);
            Mic_UnMute.gameObject.SetActive(true);
        }
    }

    private void UnMuteMic()
    {
        if (Recorder != null)
        {
            Recorder.TransmitEnabled = true;
            Mic_Mute.gameObject.SetActive(true);
            Mic_UnMute.gameObject.SetActive(false);
        }
    }
}
