using Unity.Cinemachine;

using UnityEngine;
using Fusion;
using StarterAssets;
using UnityEngine.UI;

public class GetPlayerCamera : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform playerCameraRoot;


    public int myHealth;

    private void Awake()
    {


    }

    [System.Obsolete]
    void Start()
    {

        if (Object.HasStateAuthority)
        {
            GameObject VirtualCamera = GameObject.Find("PlayerFollowCamera");
            VirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot;
            GetComponent<ThirdPersonController>().enabled = true;
        }
        else
        {
        //    Debug.LogError("i am on client only");
         //   GetComponent<NetworkTransform>().InterpolationTarget = gameObject.transform;
        }

    }

    [Networked]//(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedHealth { get; set; } = 100;
    [Networked]
    public bool chkbool { get; set; } = true;


    public int myHealthValue = 100;
    public bool myhealthBool = false;
    public Text Scoreboard;
    public void Update()
    {


        Scoreboard.text = NetworkedHealth.ToString();
        if (HasStateAuthority == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("R pressed");
            NetworkedHealth = NetworkedHealth - 1;
            //            chkbool = !chkbool;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            chkbool = !chkbool;

        }





    }



    /* private static void NetworkedHealthChanged(Changed<GetPlayerCamera> changed)
     {
         // Here you would add code to update the player's healthbar.
         Debug.Log($"Health changed to: {changed.Behaviour.NetworkedHealth}");


     }*/


}
//  [Rpc(RpcSources.All, RpcTargets.All)]
/*   public void DealDamageRpc()
   {
       Debug.Log("RPC called");
       // The code inside here will run on the client which owns this object (has state and input authority).
       myHealthValue = myHealthValue - 10;

       myhealthBool = !myhealthBool;*/

//}



