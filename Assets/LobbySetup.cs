using Fusion;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.Unicode;

public class LobbySetup : MonoBehaviour
{
    public NetworkRunner runner;
    public GameObject canvasPanel;
    lobbyStatsManager statsManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        statsManager = GameObject.FindGameObjectWithTag("StatsCanvas").GetComponent<lobbyStatsManager>();
        statsManager.StartSession.onClick.AddListener(startSession);
       // StartCoroutine(GetStats());
    }
    void startSession()
    {
        StartCoroutine(GetStats());
    }
    

    IEnumerator  GetStats()
    {
        statsManager.Loading_Screen.SetActive(true);
       // runner.Despawn(runner.GetPlayerObject(runner.LocalPlayer));
         //  runner.Shutdown(false, ShutdownReason.Ok);
        runner.LoadScene("Environment");
        // runner.Disconnect(runner.LocalPlayer);
        yield return  new WaitForSeconds(3);
     /*   //runner = new NetworkRunner();
        GameManager.instance.ConnectToLobby("pikamoon11");
        yield return new WaitForSeconds(3);
        runner.JoinSessionLobby(SessionLobby.Shared);
        //    runner.LoadScene("Environment");
        // runner.Shutdown(true, ShutdownReason.Ok);
        print("Changing Room");
        // GameManager.instance.ReturnToLobby();
        //   runner.Despawn(runner.GetPlayerObject(runner.LocalPlayer));
       yield return new WaitForSeconds(3.0f);
        //  GameManager.instance.CreateSession1(statsManager.SessionName.text,statsManager.maxPlayerLimit.value,true);
        CreateSession();*/
      //  statsManager.Loading_Screen.SetActive(true);
    }
    public async void CreateSession()
    {


       
        int randomint = UnityEngine.Random.Range(1000, 9999);
        string randomSessionName = "PikaMoon-" + randomint.ToString();

        if (runner == null)
        {
            print("Going Thru");
            //   runner = gameObject.AddComponent<NetworkRunner>();
        }

        await runner.StartGame(new StartGameArgs()
        {
            Scene = SceneRef.FromIndex(3),
            GameMode = GameMode.Shared,
            SessionName = randomSessionName,
            PlayerCount = 4,
            //    Scene = SceneManager.GetActiveScene().buildIndex, //  SceneManager.GetActiveScene().buildIndex,
            //  SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),


        });
        

    }

}
