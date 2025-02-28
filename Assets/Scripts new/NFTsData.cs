using System;
using System.Collections.Generic;
using UnityEngine;

// Interface for NFT Data
public interface INFTData
{
    string NFTName { get; }
    string Description { get; }
    int ID { get; }
    Texture2D Image { get; }
    string URI { get; }
    string OpenSeaURL { get; }
}

// Concrete class implementing INFTData
[Serializable]
public class NFTData : INFTData
{
    [SerializeField] private string nftName;
    [SerializeField] private string description;
    [SerializeField] private int id;
    [SerializeField] private Texture2D image;
    [SerializeField] private string uri;
    [SerializeField] private string openSeaURL;

    public string NFTName => nftName;
    public string Description => description;
    public int ID => id;
    public Texture2D Image => image;
    public string URI => uri;
    public string OpenSeaURL => openSeaURL;
}

[CreateAssetMenu(fileName = "NFTMetadata", menuName = "ScriptableObjects/NFTMetadata", order = 1)]
public class NFTMetadata : ScriptableObject
{
    [SerializeField] private List<NFTData> nftList;

    public INFTData GetNFTDataByID(int id)
    {
        return nftList.Find(nft => nft.ID == id);
    }
}
