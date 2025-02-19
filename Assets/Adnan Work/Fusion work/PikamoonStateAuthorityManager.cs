using UnityEngine;
using Fusion;
public class PikamoonStateAuthorityManager : NetworkBehaviour
{
    
    void Start()
    {
        //this.gameObject.GetComponent<NetworkObject>().ReleaseStateAuthority();
    }
    private void OnApplicationQuit()
    {
        
    }

}
