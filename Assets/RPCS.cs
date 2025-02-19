using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class RPCS : NetworkBehaviour
{
   // public Health health;
    private string myId;

    private void Start()
    {
        if (HasStateAuthority)
        {
            myId = TrimFirstFourAndLastOne((this.transform.GetComponent<NetworkObject>().Id).ToString());
            print("My player ID: " + myId);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void DealDamageRpc(string targetPlayerId)
    {
        string targetId = TrimFirstFourAndLastOne(targetPlayerId);

        if (HasStateAuthority)
        {
            Debug.Log("RPC Called - Target ID: " + targetId + ", My ID: " + myId);
            
            if (myId == targetId)
            {
               // health.DealDamage(1);
            }

        }
       // health.UpdateHealthUI();
        
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
