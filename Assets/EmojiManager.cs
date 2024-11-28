using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmojiManager : NetworkBehaviour
{
    
    public Canvas emojiCanvas;
    public GameObject emojiPrefab;
    private GameObject currentEmoji;
    public GameObject buttonPrefab;
    List<Button> emojiButtons = new List<Button>();
    private void Start()
    {
        if (Object.HasStateAuthority)
        {
            Transform temp = GameObject.FindGameObjectWithTag("Canvas").transform;
            // Get the emoji list from the GameManager
            List<GameObject> emojis = GameManager.instance.emojiList;
            // Loop through the emojis and create buttons
            for (int i = 0; i < emojis.Count; i++)
            {
                // Instantiate the button prefab
                GameObject button = Instantiate(buttonPrefab, temp.GetChild(2));

                // Set the emoji sprite on the button's Image
                Image buttonImage = button.GetComponent<Image>();
                // buttonImage.sprite = emojis[i].GetComponent<Image>().sprite;

                // Add a click listener to the button
                int emojiIndex = i; // Cache the index to avoid closure issues
                button.GetComponent<Button>().onClick.AddListener(delegate { RPC_Emoji(emojiIndex); });
            }
        }
        else
        {
            Debug.Log("I am client do nothing please");
        }
        //for (int i = 0; i < emojiButtons.Count; i++)
        //{
        //    var x = i;
        //    emojiButtons[x] = temp.transform.GetChild(2).GetChild(x).GetComponent<Button>();
        //    emojiButtons[x].onClick.AddListener(delegate { Spawn_Emojis(x); });
        //}
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_Emoji(int _index)
    {
       // The code inside here will run on the client which owns this object(has state and input authority).
           Debug.Log("Spawn Emoji");
        // myHealth = myHealth - 1;//  damage;
        Spawn_Emojis(_index);
    }


    public void Spawn_Emojis(int _index)
    {
        // Instantiate the emoji prefab if it doesn't exist
        if (currentEmoji == null)
        {
            currentEmoji = Instantiate(GameManager.instance.emojiList[_index]);
            currentEmoji.transform.SetParent(emojiCanvas.transform);
        }
        else 
        {
            Destroy(currentEmoji);

            currentEmoji = Instantiate(GameManager.instance.emojiList[_index]);
            currentEmoji.transform.SetParent(emojiCanvas.transform);
        }

        // Set the emoji sprite
        // var emojiImage = currentEmoji.GetComponentInChildren<Image>();
        //emojiImage.sprite = GameManager.instance.emojiList[_index];

        // Activate and position the emoji
        currentEmoji.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f,0.5f);
        currentEmoji.transform.localPosition = new Vector3(0, 1.5f, 0); // Adjust height above player
        // Schedule to hide the emoji after a duration
        CancelInvoke(nameof(HideEmoji));
        Invoke(nameof(HideEmoji), 5);
    }

    private void HideEmoji()
    {
        if (currentEmoji != null)
        {
            currentEmoji.SetActive(false);
            Destroy(currentEmoji);
        }
    }  
}
