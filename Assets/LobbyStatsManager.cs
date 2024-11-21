using Fusion;
using System.Collections;
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
        startSesson.onClick.AddListener(() => StartCoroutine(EnableButton()));

//        startSesson.onClick.AddListener(  { (StartCoroutine(EnableButton)) });
    }


    IEnumerator EnableButton()
    {
        //Start Game



GameManager.instance.environmentprefabs.transform.GetChild(1).gameObject.SetActive(false);

        yield return new WaitForSeconds(1);
        GameManager.instance.environmentprefabs.transform.GetChild(0).gameObject.SetActive(true);

        GameManager.instance.MyLocalPlayer.GetComponent<PlayerController>().SetPositionAsPerEnvironment(GameManager.instance.gamePlayTransform);
    }
    // Update is called once per frame
    void Update()
    {
        if (countCheck)
        {
            if (countdowntimer_ >= 0)
                countdowntimer_ -= Time.deltaTime;
            else
            if(countdowntimer_ <= 0)
                startSesson.interactable = true;
            CountDownTimer.text = countdowntimer_.ToString();

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



