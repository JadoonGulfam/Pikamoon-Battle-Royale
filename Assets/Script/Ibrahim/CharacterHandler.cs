using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{ 
    [Serializable]
    public class AvatarData
    {
        public genderType avatar_Gender;
        public GameObject avatar_parent;
        public SkinnedMeshRenderer avatar_body;
        public Animator avatar_animator;
        public Texture DShirt_Texture, DPent_Texture, DShoe_Texture, DEye_texture, DFace_Texture, DSkin_Texture;
    }
}
[Serializable]
public class AvatarDefaultClothes
{
    public Texture2D DefaultEyes;
    public GameObject DefaultPent, DefaultShoes, DefaultShirt, DefaultHair;
    public Color DefaultSkinColor, DefaultLipsColor, DefaultEyebrowColor;
}
[Serializable]
public class CharacterData
{
    public string gender;
    public int faceShape;
    public int eyeShape;
    public int lipsShape;
    public int noseShape;
    public float armShape;
    public float legShape;
    public float torsoShape;
    public string hairPreset;
    public string shirtPreset;
    public string trouserPreset;
    public string shoespreset;
    public string eyeColor;
    public Color eyeBrowColor;
    public Color skinColor;
    public Color hairColor;
    public Color lipsColor;
    public string characterPreset;
    public CharacterData Clone()
    {
        return (CharacterData)this.MemberwiseClone();
    }
}
public enum bodyType { Face, Hair, Lips, Eyes, Nose, SkinColor, EyeColor, Shirt, Trouser, Shoes, Preset, Arms, Legs, Torso, EyebrowColor }
public enum genderType { male, female }
public enum SliderType
{
    Arms, Legs, Torso
}