using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class Emoji_Networked : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform emojiesparent;
    public Sprite[] myEmojies;
    public GameObject EmojiPrefab;
    void Start()
    {
        for(int i=0;i< myEmojies.Length;i++) // (var emoji in myEmojies)
        {

            EmojiPrefab.GetComponent<Image>().sprite =myEmojies[i];
           GameObject temp =    Instantiate(EmojiPrefab, emojiesparent);
            temp.name = myEmojies[i].name.ToString();// spriteRenderer.sprite.name
            //temp
            temp.AddComponent<Button>();
            string tmp = temp.name;
           temp.GetComponent<Button>().onClick.AddListener(()=>{GameManager.instance.myLocalPlayer.GetComponent<EmojiSetUp>().RPC_DisplayEmoji(tmp); });
        }

    }

    // Update is called once per frame
 
}
