using Cinemachine;
using Fusion;
using Photon.Voice.Fusion;
using SickscoreGames.HUDNavigationSystem;
using System.Collections;
using UnityEngine;

public class EmojiSetUp : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (HasStateAuthority == false)
        {
          

        }
        else
        {
          
              

        }
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
   public void RPC_DisplayEmoji(string emojiName)
    {
        Debug.Log(emojiName);
        GetComponent<PlayerController>().canvasData.transform.Find("AllEmojies/"+ emojiName).gameObject.SetActive(true);
        StartCoroutine(DisableAllemojies());

    }
    IEnumerator DisableAllemojies()
    {
        yield return new WaitForSeconds(5f);
        foreach (Transform child in GetComponent<PlayerController>().canvasData.transform.Find("AllEmojies"))
        {
            // Check if the child GameObject is active (enabled)
            if (child.gameObject.activeSelf)
            {
                // Start coroutine to disable after delay
                child.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
  
}
