using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyStatsManager : NetworkBehaviour
{
    public NetworkRunner runner;
    public TMP_Text maximum_User;
    public TMP_Text current_User;
    public TMP_Text CountDownTimer;
    public float countdowntimer_;
    public bool countCheck;
    public Button startSesson;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startSesson.interactable = false;
        startSesson.onClick.AddListener(EnableButton);
    }


    void EnableButton()
    {
       //Start Game
    }
    // Update is called once per frame
    void Update()
    {
        if (countCheck)
        {

            countdowntimer_ -= Time.deltaTime;

            CountDownTimer.text = countdowntimer_.ToString();
            if(countdowntimer_ == 0)
                startSesson.interactable = true;

        }
        if(runner)
        {
            maximum_User.text = runner.SessionInfo.MaxPlayers.ToString();

            current_User.text= runner.SessionInfo.PlayerCount.ToString();
            if (runner.SessionInfo.MaxPlayers == runner.SessionInfo.PlayerCount)
                countCheck = true;
        }
    }
}



