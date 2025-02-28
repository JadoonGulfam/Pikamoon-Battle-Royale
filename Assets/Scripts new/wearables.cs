using UnityEngine;
using Fusion;

public class wearables : NetworkBehaviour
{

    public int ownId;
    void Start()
    {
        ownId = int.Parse(TrimFirstFourAndLastOne((this.transform.GetComponent<NetworkObject>().Id).ToString()));
        print("wearable id " + ownId);



              GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObject in playerObjects)
        {

            NetworkObject networkObject = playerObject.GetComponent<NetworkObject>();
            int playerId = int.Parse(TrimFirstFourAndLastOne(networkObject.Id.ToString()));
            print("player id " + playerId);
            if (networkObject != null)
            {

                if (ownId > playerId && ownId <= playerId + 100)
                {
                    this.transform.SetParent(playerObject.transform);

                }
            }
            else
            {
                Debug.LogWarning($"{playerObject.name} does not have a NetworkObject component.");
            }
        }
    }
    public static string TrimFirstFourAndLastOne(string input)
    {
        if (input.Length <= 5)
        {
            return string.Empty;
        }
        return input.Substring(4, input.Length - 5);
    }
}
