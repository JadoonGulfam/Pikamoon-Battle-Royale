using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager1 : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text id;
    [SerializeField] private new TMP_Text name;
    [SerializeField] private TMP_Text desciption;
    [SerializeField] private TMP_Text URI;
    [SerializeField] private TMP_Text OpenSeaURL;

    public void DisplayMetadata(INFTData nftData)
    {
        id.text = nftData.ID.ToString();
        name.text = nftData.NFTName.ToString();
        desciption.text = nftData.Description.ToString();
        URI.text = nftData.URI.ToString();
        OpenSeaURL.text = nftData.OpenSeaURL.ToString();
        
        
        Debug.Log("NFT Name: " + nftData.NFTName);
        Debug.Log("Description: " + nftData.Description);
        Debug.Log("ID: " + nftData.ID);
        Debug.Log("URI: " + nftData.URI);
        Debug.Log("OpenSea URL: " + nftData.OpenSeaURL);
        // Display the image and other metadata as needed
    }
}
