using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion.Sockets;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;
public class GameManager : MonoBehaviour, INetworkRunnerCallbacks
{

    public static GameManager instance;
    // public bool connectOnAwake = false;
    public NetworkRunner runner;
    public GameObject PlayerPrefab;
    public string _playerName = null;

    public TMP_Text userInputField;
    [Header("Session list")]

    public Transform _canvasCharacterSelection;
    public Button createSessionButton;
    public Button reconnectSessionButton;
    public TMP_Text connectionStatus;
    public Transform sessionListContent;
    public GameObject sessionEntryPrefab;
    public List<SessionInfo> _session = new List<SessionInfo>();
    //
    SessionInfo _currentSession;
    //
    public GameObject _roomList;
    public Button createSessionBtn;
    //  public SceneAsset lobbyScene;
    //  public SceneAsset gamePlayScene;
    public GameObject PlayerPrefabForSinglePlayer;
    public int myCharacter;
    public GameObject loader;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        runner.ProvideInput = true;
        runner.AddCallbacks(this);
        createSessionBtn.onClick.AddListener(CreateSession);
    }
    public void StartAutoBattler()
    {

        SceneManager.LoadScene("AutoBattler");
        Destroy(gameObject);
    }

  

        public void ReturnToLobby()
    {
        runner.Despawn(runner.GetPlayerObject(runner.LocalPlayer));
        runner.Shutdown(true, ShutdownReason.Ok);

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

        print("Disconnected Room");

          //  SceneManager.LoadScene("Lobby");
        //    throw new NotImplementedException();
    }

  
    public void SetPlayerName()
    {

        myCharacter = 0;//UnityEngine.Random.Range(0,10);
        StartCoroutine(ConnectToLobby(userInputField.text));

    }

    public IEnumerator ConnectToLobby(string playerName)
    {
        print(playerName);
        yield return new WaitForSeconds(1f);
        _playerName = playerName;
        if (runner == null)
        {
           // runner.
         //   runner = gameObject.AddComponent<NetworkRunner>();
        }
        runner.JoinSessionLobby(SessionLobby.Shared);
    }

    void Update()
    {
        if (runner != null)
        {
            if (runner.IsCloudReady && !runner.IsConnectedToServer)
            {
                createSessionButton.interactable = true;
            }
            else
                createSessionButton.interactable = false;
        }

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        print("session list updated");
        _session.Clear();
        _session = sessionList;

    }
    public void RefreshSessionListUI()
    {
        //Create Session list UI so we dont create duplicates
        foreach (Transform child in sessionListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (SessionInfo session in _session)
        {
            if (session.IsVisible)
            {
                GameObject entry = GameObject.Instantiate(sessionEntryPrefab, sessionListContent);
                SessionEntryPrefab script = entry.GetComponent<SessionEntryPrefab>();
                script.sessionName.text = session.Name;
                script.playerCount.text = session.PlayerCount + "/" + session.MaxPlayers;

                if (session.IsOpen == false || session.PlayerCount >= session.MaxPlayers)
                {

                    script.joinButton.interactable = false;
                }
                else
                {
                    script.joinButton.interactable = true;
                }
            }
        }
    }

 
    public async void ConnectToSession(string sessionName)
    {
        _roomList.SetActive(false);
        if (runner == null)
        {
          //  runner = gameObject.AddComponent<NetworkRunner>();
        }
        runner.name = sessionName;

        await runner.StartGame(new StartGameArgs()
        {
            Scene = SceneRef.FromIndex(2),  //runner.LoadScene(SceneRef.FromIndex(1),LoadSceneMode.Additive),// SceneManager.LoadScene("GamePlay").,
            GameMode = GameMode.Shared,
            SessionName = sessionName,
        });
    }
    public async void CreateSession()
    {
        //_roomList.SetActive(false);
        /*        createSessionBtn.enabled = false;
                int randomint = UnityEngine.Random.Range(1000, 9999);
                string randomSessionName = "Room-" + randomint.ToString();

                if (runner == null)
                {
                    runner = gameObject.AddComponent<NetworkRunner>();
                }

                await runner.StartGame(new StartGameArgs()
                {
                    Scene = SceneRef.FromIndex(2),
                    GameMode = GameMode.Shared,
                    SessionName = randomSessionName,
                    PlayerCount = 4,

       
                });*/

        createSessionBtn.enabled = false;
        int randomint = UnityEngine.Random.Range(1000, 9999);
        string randomSessionName = "PikaMoon-" + randomint.ToString();

        if (runner == null)
        {
            print("Going Thru");
         //   runner = gameObject.AddComponent<NetworkRunner>();
        }

        await runner.StartGame(new StartGameArgs()
        {
            Scene = SceneRef.FromIndex(2),
            GameMode = GameMode.Shared,
            SessionName = randomSessionName,
            PlayerCount = 4,
        //    Scene = SceneManager.GetActiveScene().buildIndex, //  SceneManager.GetActiveScene().buildIndex,
          //  SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),


        });


    }
    public async void CreateSession1(string sessionName,int playercount,bool isVisible)
    {



        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        Debug.LogError("Session Already exist with same name ");
        await runner.StartGame(new StartGameArgs()
        {
            Scene = SceneRef.FromIndex(3),
            GameMode = GameMode.Shared,
            SessionName = "heasss",
            IsVisible= true,
            PlayerCount = 8,
        });
    }


    void OnDestroy()
    {
        // Unregister the callback to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
        createSessionBtn.onClick.RemoveAllListeners();
    }
    public void SinglePlayer()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        myCharacter = 0;
        SceneManager.LoadScene("Environment");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Environment")
        {
            // Instantiate the player when the "Environment" scene is loaded
            GetComponent<NetworkRunner>().enabled = false;
            GameObject singlePlayer = Instantiate(PlayerPrefabForSinglePlayer, PlayerPrefabForSinglePlayer.transform.position, Quaternion.identity);
           
        }
    }
    public GameObject myLocalPlayer;
    public void OnConnectedToServer(NetworkRunner runner)
    {
        print("connected to server");
      /*  print(runner.name);
        print(runner.SessionInfo.PlayerCount);
        print(runner.SessionInfo.Name);
        print(runner.SessionInfo.MaxPlayers);
        print(runner.SessionInfo.PlayerCount);
        print(runner.SessionInfo.Region);
*/

        //;
        //  myLocalPlayer = 
        connectionStatus.text = "Connected to Server";
        //    throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        connectionStatus.text = "NetWork connection Failed : reason: " + reason.ToString();
        // throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        //  throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        //    throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        Debug.Log(reason.ToString());
        connectionStatus.text = "Network Disconnected reason: " + reason.ToString();



        //  throw new NotImplementedException();
    }


    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        //   throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        //   throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        //  throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        // throw new NotImplementedException();
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        //  throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        print("Onplayer Joinned");
        if (player == runner.LocalPlayer)
        {
            //     SceneManager.LoadScene(gamePlayScene.name);
            //      NetworkObject playerNetworkObject= runner.Spawn(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity, player);
            NetworkObject playerNetworkObject = runner.Spawn(PlayerPrefab, PlayerPrefab.transform.position, Quaternion.identity, player);
            runner.SetPlayerObject(player, playerNetworkObject);
            DontDestroyOnLoad(playerNetworkObject.gameObject);
            myLocalPlayer = playerNetworkObject.gameObject;
            //print( player. .GetComponent<PlayerController>().myHealth);
        }
        //  throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        print("Player left guys");
        if (player == runner.LocalPlayer)
        {

        }

        //   throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        //   throw new NotImplementedException();
    }
    //public NetworkObject PlayerPrefab;
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        // throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
      //  _sceneLoaded = false; // Reset the flag before loading a new scene
       

        //  throw new NotImplementedException();
        Debug.Log("Scene loading started.");

       

        // Additional logic when scene loading begins
       
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("Scene loading finished.");
     
        //   throw new NotImplementedException();
    }





    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

        //    print("Player left guys");
        //  throw new NotImplementedException();
    }

    // Start is called before the first frame update


}
