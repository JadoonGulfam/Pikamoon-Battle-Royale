using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using TMPro;
using Pikamoon.Controller;
using UnityEngine.AI;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks

{
    public bool connectoOnAwaike = false;
    public static NetworkRunner runnerInstance;
    private string lobbyName = "Default";


    public GameObject[] wearables;
    public GameObject[] weaponsForEnv;
    public int selectedWearablesIndex = 2;

 //   public AnimationController animationController;
    public GameObject[] playerPrefab;
    public int ChrarcterIndex = 0;
    public Transform sessionListContentParent;
    public GameObject sessionListEntryPrefab;
    public Dictionary<string, GameObject> sessionlistUIDictionary = new Dictionary<string, GameObject>();
    public TMP_InputField pname;
    public static NetworkManager Instance; // Singleton instance
    bool isPikamoonAdd;
    public float pikamoonRadius = 10f;
    public int pikamoonCount;

    [SerializeField] private List<NetworkObject> pikamoonList = new List<NetworkObject>();


    public static event Action<NetworkRunner, PlayerRef> OnOtherPlayerJoined;


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



    //public void selectCharacter(int index)
    //{
    //    ChrarcterIndex=index;

    //} 
    //public void selectwearable(int index)
    //{
    //    selectedWearablesIndex=index;

    //}
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        NetworkObject wearableNetworkObject = runnerInstance.Spawn(wearables[2], playerNetworkObject.transform.position, Quaternion.identity);

    //        Debug.Log("E key was pressed!");
    //    }
    //}

    public void SpawnWeapon(int WeaponIndex)
    {
        NetworkObject wearableNetworkObject = runnerInstance.Spawn(wearables[WeaponIndex], playerNetworkObject.transform.position, Quaternion.identity);
    }

    public void spawnEnvWeapons(Vector3 position)
    {
        int randomIndex = UnityEngine.Random.Range(0, 2); // 0 or 1
        print("Random Index: " + randomIndex);
        NetworkObject pikamoonNetworkObject = runnerInstance.Spawn(
            weaponsForEnv[randomIndex],
            position,
            Quaternion.identity
        );
    }


    //public void RequestDespawn(NetworkId objectId)
    //{
    //    animationController.RequestToDespawn(objectId);
    //}



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
            LoadingManager.Instance.ActivateLoading("Circle_Loading", false);
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
        LoadingManager.Instance.ActivateLoading("Circle_Loading", false);
    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (player == runner.LocalPlayer)
        {
            SceneManager.LoadScene("SKController_Meadows");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        if (runner.SessionInfo.PlayerCount == 1)
        {
            isPikamoonAdd = false;
        }
        else
        {
            isPikamoonAdd = true;
        }
        OnOtherPlayerJoined?.Invoke(runner, player);
    }
    private void PopulatePikamoonOverNetwork(Vector3 playerPosition, int pikamoonCount, float spawnRadius)
    {
        for (int i = 0; i < pikamoonCount; i++)
        {
            Vector3 randomOffset;
            Vector3 pikamoonPosition;
            NavMeshHit hit;
            int maxAttempts = 10; // Avoid infinite loop
            int attempts = 0;

            // Select a random Pikamoon from the list
            NetworkObject randomPikamoon = pikamoonList[i];//UnityEngine.Random.Range(0, pikamoonList.Count)];

            do
            {
                randomOffset = new Vector3(
                    UnityEngine.Random.Range(-spawnRadius, spawnRadius),
                    0f,
                    UnityEngine.Random.Range(-spawnRadius, spawnRadius)
                );
                pikamoonPosition = playerPosition + randomOffset;
                attempts++;
            }
            while (!NavMesh.SamplePosition(pikamoonPosition, out hit, 5f, NavMesh.AllAreas) && attempts < maxAttempts);

            if (attempts < maxAttempts)
            {
                pikamoonPosition = hit.position;
                NetworkObject pikamoonNetworkObject = runnerInstance.Spawn(
                    randomPikamoon,
                    pikamoonPosition,
                    Quaternion.identity
                );

                spawnEnvWeapons(pikamoonPosition);
                Debug.Log($"Pikamoon {i + 1} spawned at position: {pikamoonPosition}");
            }
            else
            {
                Debug.LogError("Failed to find valid NavMesh position for Pikamoon spawn.");
            }
        }
    }

   

    NetworkObject playerNetworkObject;
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SKController_Meadows")
        {
            GameObject go = GameObject.FindGameObjectWithTag("Ref");
             playerNetworkObject = runnerInstance.Spawn(playerPrefab[ChrarcterIndex], Vector3.zero, Quaternion.identity);
            go.GetComponent<ReferencesHolder>().InstantiatePlayer(playerNetworkObject.gameObject);
           // NetworkObject wearableNetworkObject = runnerInstance.Spawn(wearables[selectedWearablesIndex], playerNetworkObject.transform.position, Quaternion.identity);

            if (!isPikamoonAdd)
            {
                PopulatePikamoonOverNetwork(playerNetworkObject.transform.position,pikamoonCount,pikamoonRadius);
                isPikamoonAdd = true; // Ensure Pikamoon is only added once
            }

            if (playerNetworkObject.HasInputAuthority)
            {
                print("Player has input authority");
            }
            print("Scene loaded successfully");
            LoadingManager.Instance.DeactivateAll();
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
    #region ________________netbehaviour methods____________________
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
        LoadingManager.Instance.DeactivateAll();
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
    #endregion
}