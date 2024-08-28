using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class AvatarDefaultClothes
{
    public Texture2D DefaultSkin, DefaultEyes, DefaultLips;
    public GameObject DefaultPent, DefaultShoes, DefaultShirt, DefaultHair;
}
public class DefaultClothDatabase : MonoBehaviour
{
    public AvatarDefaultClothes maleAvatarDefaultCostume;
    public AvatarDefaultClothes femaleAvatarDefaultCostume;

}