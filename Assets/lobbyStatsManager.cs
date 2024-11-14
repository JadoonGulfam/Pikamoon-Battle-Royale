using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class lobbyStatsManager : MonoBehaviour
{


    public TMP_Dropdown maxPlayerLimit;
    public TMP_Dropdown sessionTime;
    public Toggle IsVoiceChat,IstextChat,IsLockRoom;
    public TMP_Dropdown ServerRegion;
    public TMP_InputField SessionName;
    public TMP_Dropdown TotalTeams;
    public Button StartSession;
    public GameObject Loading_Screen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
