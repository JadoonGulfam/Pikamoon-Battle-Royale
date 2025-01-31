using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using TMPro;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks

{
    public bool connectoOnAwaike = false;
    public static NetworkRunner runnerInstance;
    private string lobbyName = "Default";
    
    
    public GameObject[] wearables;
    public int selectedWearablesIndex = 0;


    public GameObject[] playerPrefab;
    public int ChrarcterIndex = 0;
    public Transform sessionListContentParent;
    public GameObject sessionListEntryPrefab;
    public Dictionary<string, GameObject> sessionlistUIDictionary = new Dictionary<string, GameObject>();
    public TMP_InputField pname;
    public static NetworkManager Instance; // Singleton instance
    // public string _playerName = "adnan";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make persistent
        }
        else
        {
            Destroy(gameObject);
        }

        runnerInstance = gameObject.AddComponent<NetworkRunner>();
    }



    public void selectCharacter(int index)
    {
        ChrarcterIndex=index;
        
    } 
    public void selectwearable(int index)
    {
        selectedWearablesIndex=index;
        
    }
    
    private void Start()
    {
        runnerInstance.JoinSessionLobby(SessionLobby.Shared, lobbyName);
    }
    public string GetPlayerName()
    {
        return string.IsNullOrWhiteSpace(pname.text) ? "Player_" + UnityEngine.Random.Range(1000, 9999) : pname.text;
    }
    
    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("ConnectedToServer");
    }

    public void CreateRandomSession()
    {
        int randomInt = UnityEngine.Random.Range(1000, 9999);
        string randomSessionName = "Room" + randomInt.ToString();
        try
        {
            runnerInstance.StartGame(new StartGameArgs()
            {
                SessionName = randomSessionName,
                GameMode = GameMode.Shared,
            });

            Debug.Log("Game started with session name: " + randomSessionName);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error starting game session: " + ex.Message);
        }
    }

    public void JoinSession(string sessionName)
    {
        print("33333" + sessionName);
        runnerInstance.StartGame(new StartGameArgs()
        {
            SessionName = sessionName,
            GameMode = GameMode.Shared,
        });
    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (player == runner.LocalPlayer)
        {
            SceneManager.LoadScene("Meadows_Demo");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Meadows_Demo")
        {
       
            NetworkObject playerNetworkObject = runnerInstance.Spawn(playerPrefab[ChrarcterIndex], Vector3.zero, Quaternion.identity);
            NetworkObject wearableNetworkObject = runnerInstance.Spawn(wearables[selectedWearablesIndex], Vector3.zero, Quaternion.identity);
     
            if (playerNetworkObject.HasInputAuthority)
            {
                print("111111111111");
       
            }
            print("22222222");
           
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }


    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        DeleteOldSessionsFromUI(sessionList);
        CompareLists(sessionList);
        Debug.Log("SessionListUpdated" + sessionList);
    }

    public void DeleteOldSessionsFromUI(List<SessionInfo> sessionList)
    {
        List<string> keysToRemove = new List<string>();

        foreach (var kvp in sessionlistUIDictionary)
        {
            string sessionKey = kvp.Key;
            bool isContained = false;

            foreach (SessionInfo sessionInfo in sessionList)
            {
                if (sessionInfo.Name == sessionKey)
                {
                    isContained = true;
                    break;
                }
            }

            if (!isContained)
            {
                keysToRemove.Add(sessionKey);
            }
        }

        foreach (string key in keysToRemove)
        {
            Destroy(sessionlistUIDictionary[key]);
            sessionlistUIDictionary.Remove(key);
        }
    }

    private void CompareLists(List<SessionInfo> sessionList)
    {
        foreach (SessionInfo session in sessionList)
        {
            if (sessionlistUIDictionary.ContainsKey(session.Name))
            {
                UpdateUi(session);
            }
            else
            {
                CreateEntryUi(session);
            }
        }
    }

    private void CreateEntryUi(SessionInfo session)
    {
        print("create Entry");
        GameObject newEntry = Instantiate(sessionListEntryPrefab, sessionListContentParent);
        SessionListEntry entryScript = newEntry.GetComponent<SessionListEntry>();
        sessionlistUIDictionary.Add(session.Name, newEntry);

        entryScript.roomName.text = session.Name;
        entryScript.PlayerCount.text = $"{session.PlayerCount}/{session.MaxPlayers}";
        entryScript.joinButton.interactable = session.IsOpen;

        newEntry.SetActive(session.IsVisible);
    }

    private void UpdateUi(SessionInfo session)
    {
        print("update session");
        if (sessionlistUIDictionary.TryGetValue(session.Name, out GameObject entry))
        {
            SessionListEntry entryScript = entry.GetComponent<SessionListEntry>();

            entryScript.roomName.text = session.Name;
            entryScript.PlayerCount.text = $"{session.PlayerCount}/{session.MaxPlayers}";
            entryScript.joinButton.interactable = session.IsOpen;

            entry.SetActive(session.IsVisible);
        }
    }

    // Other INetworkRunnerCallbacks methods go here

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("PlayerLeft");
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {

    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }
}