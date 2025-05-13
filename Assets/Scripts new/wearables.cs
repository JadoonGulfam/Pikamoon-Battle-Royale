using UnityEngine;
using Fusion;
using Pikamoon.Controller;

public class wearables : NetworkBehaviour
{

    public int ownId;
    void Start()
    {
        ownId = int.Parse(TrimFirstFourAndLastOne((this.transform.GetComponent<NetworkObject>().Id).ToString()));
        print("wepon id " + ownId);



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
                   // playerObject.GetComponent<InventoryController>().ManualAssignAtStart(this.transform);
                    playerObject.GetComponent<InventoryController>().ManualAssignAtStart(this.transform);
                     
                    //this.transform.Translate(new Vector3(0,2.5f,0));
                    //this.transform.position = Vector3.zero;

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
