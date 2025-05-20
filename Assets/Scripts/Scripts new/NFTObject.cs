using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class NFTObject : MonoBehaviour, IInteractable
{
    [SerializeField] private NFTMetadata metadata;
    [SerializeField] private int nftID;
    [SerializeField] UIManager1 uiManager;
    private void Awake()
    {

        nftID = int.Parse(this.gameObject.name);
    }
    public void Interact()
    {
        INFTData nftData = metadata.GetNFTDataByID(nftID);
        if (nftData != null)
        {
            uiManager.DisplayMetadata(nftData);
        }
        else
        {
            Debug.Log("NFT with ID" +nftID+" not found.");
        }
    }

    //private void DisplayMetadata(INFTData nftData)
    //{
        
    //    Debug.Log("NFT Name: " + nftData.NFTName);
    //    Debug.Log("Description: " + nftData.Description);
    //    Debug.Log("ID: " + nftData.ID);
    //    Debug.Log("URI: " + nftData.URI);
    //    Debug.Log("OpenSea URL: " + nftData.OpenSeaURL);
    //    // Display the image and other metadata as needed
    //}
}
